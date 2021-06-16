using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InspirActions.Core.Helpers
{
    public class NameRepository
    {
        private readonly string fileName;
        private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented
        };

        public NameRepository(string fileName = "names.json")
        {
            this.fileName = fileName;
        }

        public IEnumerable<string> Load()
            => File.Exists(fileName)
                ? JsonConvert.DeserializeObject<string[]>(File.ReadAllText(fileName), jsonSettings)
                : Array.Empty<string>();

        public void Save(IEnumerable<string> names)
            => File.WriteAllText(fileName, JsonConvert.SerializeObject(names
                .Where(n => !string.IsNullOrEmpty(n)), jsonSettings));
    }
}
