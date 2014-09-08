using System.IO.Abstractions;

namespace FileUtilities
{
    public interface IDirectoryQuery
    {
        DirectoryInfoBase[] GetDirectories(string rootPath);
    }
}