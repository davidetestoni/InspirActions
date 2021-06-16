using InspirActions.Core.Extensions;
using InspirActions.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InspirActions.Core.Factories
{
    public class SessionTaskFactory
    {
        private readonly string[] greetings;
        private readonly string[] names;
        private readonly AvailableTask[] availableTasks;
        private readonly Dictionary<string, CategoryOptions> categoryOptions;
        private static readonly Random rng = new();

        public SessionTaskFactory(IEnumerable<string> greetings, IEnumerable<string> names,
            IEnumerable<AvailableTask> availableTasks, Dictionary<string, CategoryOptions> categoryOptions)
        {
            this.greetings = greetings.ToArray();
            this.names = names.ToArray();
            this.availableTasks = availableTasks.ToArray();
            this.categoryOptions = categoryOptions;
        }

        public SessionTask Generate(string picture)
        {
            var greeting = greetings.Random();
            var name = names.Random();
            var task = availableTasks.Random(t => categoryOptions[t.Category].Active);
            return new SessionTask
            {
                Picture = picture,
                Task = new AvailableTask
                {
                    ShortName = task.ShortName,
                    Category = task.Category,
                    Description = $"{greeting} {name}, {ReplaceVariables(task.Description)}",
                    Difficulty = task.Difficulty,
                    Duration = task.Duration,
                    Repetitions = task.Repetitions,
                    Stage = task.Stage
                }
            };
        }

        private static string ReplaceVariables(string input)
        {
            var replaced = false;

            do
            {
                var match = Regex.Match(input, @"\[RAND:([\d]+):([\d]+)\]");

                if (match.Success)
                {
                    var min = int.Parse(match.Groups[1].Value);
                    var max = int.Parse(match.Groups[2].Value);
                    var random = rng.Next(min, max + 1);
                    input = input.Replace(match.Groups[0].Value, random.ToString());
                    replaced = true;
                }
                else
                {
                    replaced = false;
                }
            }
            while (replaced);

            return input;
        }
    }
}
