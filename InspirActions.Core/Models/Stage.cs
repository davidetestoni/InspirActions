using InspirActions.Core.Enums;
using System.Collections.Generic;

namespace InspirActions.Core.Models
{
    /// <summary>
    /// A stage of a session.
    /// </summary>
    public class Stage
    {
        public StageName Name { get; set; }
        public List<string> Pictures { get; set; }
        public List<string> Tasks { get; set; }
    }
}
