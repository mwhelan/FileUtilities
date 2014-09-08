using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace FileUtilities
{
    public class SeasonDirectoryQuery : IDirectoryQuery
    {
        private readonly IFileSystem _fileSystem;

        public SeasonDirectoryQuery(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public DirectoryInfoBase[] GetDirectories(string rootPath)
        {
            var directories = _fileSystem.DirectoryInfo.FromDirectoryName(rootPath)
                .GetDirectories("*", SearchOption.AllDirectories);
            var seasonDirectories = directories
                .Where(directory => directory.Name.StartsWith("Season"))
                .ToArray();
            return seasonDirectories;
        }
    }
}