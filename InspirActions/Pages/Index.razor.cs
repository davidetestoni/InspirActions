using InspirActions.Core.Helpers;
using InspirActions.Core.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.IO;
using System.Threading.Tasks;

namespace InspirActions.Pages
{
    public partial class Index : IDisposable
    {
        [Inject] private TaskRepository TaskRepo { get; set; }
        [Inject] private NavigationManager NavManager { get; set; }

        private string picsFolder = "";
        private int numberOfTasks = 20;
        private Session session;
        private string base64 = "";
        private string currentText = "";
        private DotNetObjectReference<Index> reference;

        private async Task StartSession()
        {
            var pictures = PicturesLoader.LoadFromFolder(picsFolder);
            session = new Session(pictures, TaskRepo.Load(), numberOfTasks);

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
