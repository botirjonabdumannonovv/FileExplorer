﻿using Microsoft.Extensions.Options;
using Training.FileExplorer.Application.Common.Models.Filtering;
using Training.FileExplorer.Application.FileStorage.Brokers;
using Training.FileExplorer.Application.FileStorage.Models.Filtering;
using Training.FileExplorer.Application.FileStorage.Models.Settings;
using Training.FileExplorer.Application.FileStorage.Models.Storage;
using Training.FileExplorer.Application.FileStorage.Services;

namespace Training.FileExplorer.Infrastructure.FileStorage.Services;

public class FileService(
    IOptions<FileFilterSettings> fileFilterSettings,
    IOptions<FileStorageSettings> fileStorageSettings,
    IFileBroker fileBroker
    ) : IFileService
{
    private readonly FileFilterSettings _fileFilterSettings = fileFilterSettings.Value;
    private readonly FileStorageSettings _fileStorageSettings = fileStorageSettings.Value;
    public async ValueTask<IList<StorageFile>> GetFilesByPathAsync(IEnumerable<string> filesPath)
    {
        var files = await Task.Run(() => 
            { return filesPath.Select(filePath => 
                fileBroker.GetByPath(filePath)).ToList(); });

        return files;
    }

    public ValueTask<StorageFile> GetFileByPathAsync(string filePath) =>
        !string.IsNullOrWhiteSpace(filePath)
            ? new ValueTask<StorageFile>(fileBroker.GetByPath(filePath))
            : throw new ArgumentNullException(nameof(filePath));

    public IEnumerable<StorageFilesSummary> GetFilesSummary(IEnumerable<StorageFile> files)
    {
        var filesType = files.Select(file => (File: file, Type: GetFileType(file.Path)));
        return filesType
            .GroupBy(file => file.Type)
            .Select(filesGroup => new StorageFilesSummary
            {
                FileType = filesGroup.Key,
                DisplayName = _fileFilterSettings.FileExtensions.FirstOrDefault(extension => extension.FileType == filesGroup.Key)?.DisplayName ??
                              "Other files",
                Count = filesGroup.Count(),
                Size = filesGroup.Sum(file => file.File.Size),
                ImageUrl = _fileFilterSettings.FileExtensions.FirstOrDefault(extension => extension.FileType == filesGroup.Key)?.ImageUrl ??
                           _fileStorageSettings.FileImageUrl
            });
    }

    public StorageFileType GetFileType(string filePath)
    {
        var fileExtension = Path.GetExtension(filePath).TrimStart('.');
        var matchedFileType = _fileFilterSettings.FileExtensions.FirstOrDefault(extension => extension.Extensions.Contains(fileExtension));
        return matchedFileType?.FileType ?? StorageFileType.Other;
    }
}