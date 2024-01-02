using Training.FileExplorer.Application.Common.Models.Filtering;
using Training.FileExplorer.Application.FileStorage.Models.Filtering;
using Training.FileExplorer.Application.FileStorage.Models.Storage;
using Training.FileExplorer.Application.FileStorage.Services;

namespace Training.FileExplorer.Infrastructure.FileStorage.Services;

public class FileProcessingService(
    IDirectoryService directoryService,
    IFileService fileService
    ) : IFileProcessingService
{
    public async ValueTask<StorageFileFilterDataModel> GetFilterDataModelAsync(string directoryPath)
    {
        var pagination = new FilterPagination
        {
            PageSize = int.MaxValue,
            PageToken = 1
        };

        var filesPath = directoryService.GetFilesPath(directoryPath, pagination);
        var files = await fileService.GetFilesByPathAsync(filesPath);
        
        var filesSummary = fileService.GetFilesSummary(files);
        var filterDataModel = new StorageFileFilterDataModel
        {
            FilterData = filesSummary.ToList()
        };
        
        return filterDataModel;
    }

    public async ValueTask<IList<StorageFile>> GetByFilterAsync(StorageFileFilterModel filterModel)
    {
        var filteredFilesPath = directoryService
            .GetFilesPath(filterModel.DirectoryPath, filterModel)
            .Where(filePath => filterModel.FilesType.Contains(fileService.GetFileType(filePath)));

        var files = await fileService.GetFilesByPathAsync(filteredFilesPath);

        return files;
    }
}