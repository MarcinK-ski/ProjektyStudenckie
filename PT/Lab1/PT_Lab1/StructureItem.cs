using System.Collections.ObjectModel;
using System.IO;

namespace PT_Lab1
{
    public abstract class StructureItem
    {
        public string Title { get; set; }
        public string Path { get; set; }
        public DirectoryItem Parent { get; set; }
        public FileAttributes Attributes { get; set; }
        public PermissionStatus PermissionToItem { get; set; }

        public StructureItem(string title, string path)
        {
            Title = title;
            Path = path;
        }

        public override string ToString()
        {
            return $"{Title}";
        }
    }
}
