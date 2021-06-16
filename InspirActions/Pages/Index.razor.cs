using InspirActions.Core.Helpers;
using InspirActions.Core.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InspirActions.Pages
{
    public partial class Index : IDisposable
    {
        [Inject] private TaskRepository TaskRepo { get; set; }
        [Inject] private GreetingRepository GreetingRepo { get; set; }
        [Inject] private NameRepository NameRepo { get; set; }
        [Inject] private NavigationManager NavManager { get; set; }

        private string picsFolder = "";
        private int numberOfTasks = 20;
        private Dictionary<string, CategoryOptions> categoryOptions = new();
        private List<AvailableTask> availableTasks = new();
        private List<string> greetings = new();
        private List<string> names = new();
        private Session session;
        private string base64 = "";
        private string currentText = "";
        private DotNetObjectReference<Index> reference;

        protected override void OnInitialized()
        {
            availableTasks = TaskRepo.Load().ToList();
            greetings = GreetingRepo.Load().ToList();
            names = NameRepo.Load().ToList();

            categoryOptions = availableTasks.Select(t => t.Category)
                .Where(c => !string.IsNullOrEmpty(c)).Distinct()
                .ToDictionary(c => c, c => new CategoryOptions { Active = true });
        }

        private async Task StartSession()
        {
            if (string.IsNullOrWhiteSpace(picsFolder) || numberOfTasks < 1)
            {
                return;
            }

            var pictures = PicturesLoader.LoadFromFolder(picsFolder).ToList();
            var options = new SessionOptions
            {
                Pictures = pictures,
                AvailableTasks = availableTasks,
                Greetings = greetings,
                Names = names,
                NumberOfTasks = numberOfTasks,
                CategoryOptions = categoryOptions
            };

            session = new Session(options);

            await UpdateTask();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                // See warning about memory above in the article
                reference = DotNetObjectReference.Create(this);
                await js.InvokeVoidAsync("registerSession", reference);
            }
        }

        [JSInvokable]
        public async Task PreviousTask()
        {
            session.PreviousTask();
            await UpdateTask();
        }

        [JSInvokable]
        public async Task NextTask()
        {
            session.NextTask();
            await UpdateTask();
        }

        private void EditTasks()
            => NavManager.NavigateTo("/tasks");

        private async Task UpdateTask()
        {
            if (session.CurrentTask is not null)
            {
                base64 = Convert.ToBase64String(File.ReadAllBytes(session.CurrentTask.Picture));
                currentText = session.CurrentTask.Task.Description;
            }

            await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            reference?.Dispose();
        }
    }
}
