using System;
using System.IO.Abstractions;

namespace FileUtilities
{
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