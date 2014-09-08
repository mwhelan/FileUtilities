using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;

namespace FileUtilities.UnitTests
{
    public class DirectoryRenamerTests
    {
        private DirectoryOperation _sut;
        private MockFileSystem _fileSystem;

        public DirectoryRenamerTests()
        {
            _fileSystem = CreateFileSystem();
            _sut = new DirectoryOperation(new SeasonDirectoryQuery(_fileSystem), new RenameSeasonDirectoryAction());
        }

        public void ShouldRenameFolderIfSeasonLessThanTen()
        {
            _sut.Execute("C:\\TV");

            _fileSystem
                .GetFile("C:\\TV\\Friends\\Season 01\\Friends - S01E01 - The one where Monica gets a roommate.mp4")
                .Should().NotBeNull();
        }

        public void ShouldNotRenameFolderIfSeasonTenOrMore()
        {
            _sut.Execute("C:\\TV");

            _fileSystem
                .GetFile("C:\\TV\\Friends\\Season 10\\Friends - S10E10 - The One Where Chandler Gets Caught.mp4")
                .Should().NotBeNull();
        }


        private MockFileSystem CreateFileSystem()
        {
            var fileData = new MockFileData("");
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {"C:\\TV\\Friends\\Season 1\\Friends - S01E01 - The one where Monica gets a roommate.mp4", fileData},
                {"C:\\TV\\Friends\\Season 10\\Friends - S10E10 - The One Where Chandler Gets Caught.mp4", fileData},
                {"C:\\TV\\The Walking Dead\\Season 3\\The Walking Dead - S01E02 - Credit Where Credit's Due.mp4", fileData},
                {"C:\\TV\\Alphas\\Season 2\\Alphas - S02E05 - Gaslight.mp4", fileData},
            },
            "C:\\TV");

            return fileSystem;
        }
    }
}
