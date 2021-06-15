using InspirActions.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace InspirActions.Core.Helpers
{
    public class TaskRepository
    {
        private readonly string fileName;
        private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented
        };

        public TaskRepository(string fileName = "tasks.json")
        {
            this.fileName = fileName;
        }

        public IEnumerable<AvailableTask> Load()
            => File.Exists(fileName)
                ? JsonConvert.DeserializeObject<AvailableTask[]>(File.ReadAllText(fileName), jsonSettings)
                : Array.Empty<AvailableTask>();

        public void Save(IEnumerable<AvailableTask> tasks)
            => File.WriteAllText(fileName, JsonConvert.SerializeObject(tasks, jsonSettings));
    }
}
