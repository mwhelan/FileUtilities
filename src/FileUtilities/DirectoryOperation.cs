namespace FileUtilities
{
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
}