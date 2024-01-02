using AutoMapper;
using Training.FileExplorer.Application.FileStorage.Brokers;
using Training.FileExplorer.Application.FileStorage.Models;
using Training.FileExplorer.Application.FileStorage.Models.Storage;

namespace Training.FileExplorer.Infrastructure.FileStorage.Brokers;

public class DriveBroker(IMapper mapper) : IDriveBroker
{
    public IEnumerable<StorageDrive> Get()
    {
        return DriveInfo
            .GetDrives()
            .Select(drive => mapper.Map<StorageDrive>(drive))
            .AsQueryable();
    }
}