using InspirActions.Core.Enums;

namespace InspirActions.Core.Models
{
    public class AvailableTask
    {
        public string ShortName { get; set; }
        public StageName Stage { get; set; }
        public string Category { get; set; }
        public int Duration { get; set; }
        public int Repetitions { get; set; }

        // In the description you can write stuff like {RAND:1:10} or {PLAYER} and they will be replaced
        // when creating the SessionTask
        public string Description { get; set; }
        public int Difficulty { get; set; }
    }
}
