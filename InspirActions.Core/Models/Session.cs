using InspirActions.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace InspirActions.Core.Models
{
    public class Session
    {
        private readonly List<SessionTask> tasks;
        private int index = 0;

        public bool Finished { get; private set; } = false;
        public SessionTask CurrentTask => tasks[index];

        public Session(IEnumerable<string> pictures, IEnumerable<AvailableTask> availableTasks, int numberOfTasks)
        {
            // Initialize the tasks when the session is created
            tasks = pictures.Take(numberOfTasks).Select(p => new SessionTask
            {
                Picture = p,
                Task = availableTasks.Random()
            }).ToList();
        }

        public void NextTask()
        {
            if (index < tasks.Count - 1)
            {
                index++;
            }
            else
            {
                Finished = true;
            }
        }

        public void PreviousTask()
        {
            if (index > 0)
            {
                index--;
            }
        }
    }
}
