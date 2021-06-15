using InspirActions.Core.Models;
using System.Collections.Generic;

namespace InspirActions.Core.Factories
{
    public static class StageFactory
    {
        public static List<Stage> Create() 
        {
            var start = new Stage 
            {

            };
            var middle = new Stage();
            var end = new Stage();

            return new List<Stage> { start, middle, end };
        }
    }
}
