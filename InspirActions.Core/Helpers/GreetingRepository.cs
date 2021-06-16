using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InspirActions.Core.Helpers
{
    public class GreetingRepository
    {
        private readonly string fileName;
        private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented
        };

        public GreetingRepository(string fileName = "greetings.json")
        {
            this.fileName = fileName;
        }

        public IEnumerable<string> Load()
            => File.Exists(fileName)
                ? JsonConvert.DeserializeObject<string[]>(File.ReadAllText(fileName), jsonSettings)
                : Array.Empty<string>();

        public void Save(IEnumerable<string> greetings)
            => File.WriteAllText(fileName, JsonConvert.SerializeObject(greetings
                .Where(g => !string.IsNullOrEmpty(g)), jsonSettings));
    }
}
