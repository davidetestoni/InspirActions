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

        public Session(SessionOptions options)
        {
            // Initialize the tasks when the session is created
            tasks = options.Pictures.Take(options.NumberOfTasks).Select(p => new SessionTask
            {
                Picture = p,
                Task = options.AvailableTasks.Where(t => options.CategoryOptions[t.Category].Active).Random()
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
