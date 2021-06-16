using System.Collections.Generic;

namespace InspirActions.Core.Models
{
    public class SessionOptions
    {
        public List<string> Greetings { get; set; }
        public List<string> Names { get; set; }
        public List<string> Pictures { get; set; }
        public List<AvailableTask> AvailableTasks { get; set; }
        public Dictionary<string, CategoryOptions> CategoryOptions { get; set; }
        public int NumberOfTasks { get; set; } = 20;
    }
}
