using Training.FileExplorer.Application.Common.Models.Filtering;
using Training.FileExplorer.Application.Common.Querying.Extensions;
using Training.FileExplorer.Application.FileStorage.Brokers;
using Training.FileExplorer.Application.FileStorage.Models.Storage;
using Training.FileExplorer.Application.FileStorage.Services;

namespace Training.FileExplorer.Infrastructure.FileStorage.Services;

public class DirectoryService(IDirectoryBroker directoryBroker) : IDirectoryService
{
    public IEnumerable<string> GetDirectoriesPath(
        string directoryPath, FilterPagination paginationOptions) =>
        directoryBroker.GetFilesPath(directoryPath).ApplyPagination(paginationOptions);

    public IEnumerable<string> GetFilesPath(
        string directoryPath, FilterPagination paginationOptions) =>
        directoryBroker.GetFilesPath(directoryPath).ApplyPagination(paginationOptions);

    public ValueTask<StorageDirectory?> GetByPathAsync(string directoryPath)
    {
        if (string.IsNullOrWhiteSpace(directoryPath))
            throw new ArgumentNullException(nameof(directoryPath));

        return new ValueTask<StorageDirectory?>(directoryBroker.GetByPathAsync(directoryPath));
    }

    public async ValueTask<IList<StorageDirectory>> GetDirectoriesAsync(
        string directoryPath, FilterPagination paginationOptions)
    {
        if (string.IsNullOrWhiteSpace(directoryPath))
            throw new ArgumentNullException(nameof(directoryPath));

        var directories = await Task.Run(() => 
            directoryBroker.GetDirectories(directoryPath)
                .ApplyPagination(paginationOptions).ToList());

        return directories;
    }
}