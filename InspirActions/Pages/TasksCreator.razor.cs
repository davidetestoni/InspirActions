using InspirActions.Core.Helpers;
using InspirActions.Core.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace InspirActions.Pages
{
    public partial class TasksCreator
    {
        [Inject] private TaskRepository TaskRepo { get; set; }
        [Inject] private NavigationManager NavManager { get; set; }

        private List<AvailableTask> tasks = new();

        protected override void OnInitialized()
        {
            tasks = TaskRepo.Load().ToList();
            StateHasChanged();
        }

        private void AddNew()
            => tasks.Add(new AvailableTask());

        private void MoveUp(AvailableTask task)
        {
            var index = tasks.IndexOf(task);

            if (index == 0)
            {
                return;
            }

            tasks[index] = tasks[index - 1];
            tasks[index - 1] = task;
        }

        private void MoveDown(AvailableTask task)
        {
            var index = tasks.IndexOf(task);

            if (index == tasks.Count)
            {
                return;
            }

            tasks[index] = tasks[index + 1];
            tasks[index + 1] = task;
        }

        private void Save()
        {
            TaskRepo.Save(tasks);
            NavManager.NavigateTo("/");
        }
    }
}
