using Training.FileExplorer.Application.FileStorage.Models.Storage;

namespace Training.FileExplorer.Api.Models.Dtos;

public interface IStorageDto
{
    string Path { get; set; }
    
    StorageEntryType EntryType { get; set; }
}