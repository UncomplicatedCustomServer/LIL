using System.IO;
using System.Linq;

namespace LIL.Features.Files
{
    internal abstract class BaseFile(string path)
    {
        public abstract string Extension { get; }

        public string Name { get; } = path.Split('/').Last().Split('.').First();

        public string FileName => $"{Name}.{Extension}";

        public string Content { get; } = File.ReadAllText(path);
    }
}
