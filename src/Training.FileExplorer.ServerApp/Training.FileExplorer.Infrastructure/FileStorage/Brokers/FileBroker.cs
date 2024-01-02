using AutoMapper;
using Training.FileExplorer.Application.FileStorage.Brokers;
using Training.FileExplorer.Application.FileStorage.Models.Storage;

namespace Training.FileExplorer.Infrastructure.FileStorage.Brokers;

public class FileBroker(IMapper mapper) : IFileBroker
{
    public StorageFile GetByPath(string filePath)
    {
        return mapper.Map<StorageFile>(new FileInfo(filePath));
    }
}