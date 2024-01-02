using AutoMapper;
using FileExplorer.Application.FileStorage.Brokers;
using FileExplorer.Application.FileStorage.Models;

namespace FileExplorer.Infrastructure.FileStorage.Brokers;

public class DriveBroker(IMapper mapper) : IDriveBroker
{
    public IEnumerable<StorageDrive> Get()
    {
        return DriveInfo
            .GetDrives()
            .Select(driveInfo => mapper.Map<StorageDrive>(driveInfo));
    }
}