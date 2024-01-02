using Training.FileExplorer.Application.FileStorage.Models.Filtering;
using Training.FileExplorer.Application.FileStorage.Models.Storage;
using Training.FileExplorer.Application.FileStorage.Services;

namespace Training.FileExplorer.Infrastructure.FileStorage.Services;

public class DirectoryProcessingService(
    IFileService fileService,
    IDirectoryService directoryService
    ) : IDirectoryProcessingService
{
    public async ValueTask<List<IStorageEntry>> GetEntriesAsync(string directoryPath, StorageDirectoryEntryFilterModel filterModel)
    {
        var storageItems = new List<IStorageEntry>();

        if (filterModel.IncludeDirectories)
            storageItems.AddRange(await directoryService.GetDirectoriesAsync(directoryPath, filterModel));

        if (filterModel.IncludeFiles)
            storageItems.AddRange(await fileService.GetFilesByPathAsync(directoryService.GetFilesPath(directoryPath, filterModel)));

        return storageItems;
    }
}