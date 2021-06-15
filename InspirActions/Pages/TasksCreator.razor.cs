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
            => tasks = TaskRepo.Load().ToList();

        private void AddNew()
            => tasks.Add(new AvailableTask());

        private void Save()
        {
            TaskRepo.Save(tasks);
            NavManager.NavigateTo("/");
        }
    }
}
