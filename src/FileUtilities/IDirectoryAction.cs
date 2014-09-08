using System.IO.Abstractions;

namespace FileUtilities
{
    public interface IDirectoryAction
    {
        void Modify(DirectoryInfoBase directory);
    }
}