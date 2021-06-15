using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InspirActions.Core.Helpers
{
    public static class PicturesLoader
    {
        private static readonly string[] allowedExtensions = new[] { "jpg", "jpeg", "png" };

        public static IEnumerable<string> LoadFromFolder(string folder)
            => Directory.EnumerateFiles(folder)
            .Where(f => allowedExtensions
                .Any(ext => ext.Equals(GetExtension(f), StringComparison.OrdinalIgnoreCase)));

        private static string GetExtension(string fileName)
        {
            var ext = Path.GetExtension(fileName);
            return ext is null ? string.Empty : ext.Replace(".", "");
        }
    }
}
