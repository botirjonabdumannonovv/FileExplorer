using Training.FileExplorer.Application.FileStorage.Brokers;
using Training.FileExplorer.Application.FileStorage.Models.Storage;
using Training.FileExplorer.Application.FileStorage.Services;

namespace Training.FileExplorer.Infrastructure.FileStorage.Services;

public class DriveService(IDriveBroker driveBroker) : IDriveService
{
    public ValueTask<IList<StorageDrive>> GetAsync()
    {
        var drives = driveBroker.Get().ToList();

        return new ValueTask<IList<StorageDrive>>(drives);
    }
}