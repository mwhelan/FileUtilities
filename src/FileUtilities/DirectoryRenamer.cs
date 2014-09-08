using System;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace FileUtilities
{
    public interface IDirectoryQuery
    {
        DirectoryInfoBase[] GetDirectories(string rootPath);
    }

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

    public class DirectoryOperation
    {
        private readonly IDirectoryQuery _directoryQuery;
        private readonly IDirectoryAction _directoryAction;

        public DirectoryOperation(IDirectoryQuery directoryQuery, IDirectoryAction directoryAction)
        {
            _directoryQuery = directoryQuery;
            _directoryAction = directoryAction;
        }

        public void Execute(string directoryPath)
        {
            foreach (var directory in _directoryQuery.GetDirectories(directoryPath))
            {
                _directoryAction.Modify(directory);
            }
        }
    }

    public interface IDirectoryAction
    {
        void Modify(DirectoryInfoBase directory);
    }

    public class RenameSeasonDirectoryAction : IDirectoryAction
    {
        public void Modify(DirectoryInfoBase directory)
        {
                var split = directory.Name.Split(' ');
                if (split.Length != 2 || split[0].ToLower() != "season")
                {
                    return;
                }

                int seasonNumber = Int32.Parse(split[1]);
            if (seasonNumber >= 0 && seasonNumber <= 9)
            {
                var parentFolder = directory.Parent.FullName;
                directory.MoveTo(string.Format("{0}Season {1}", parentFolder, seasonNumber.ToString("00")));
            }

        }
    }
}