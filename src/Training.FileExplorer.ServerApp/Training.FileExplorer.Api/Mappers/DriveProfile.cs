using AutoMapper;
using Training.FileExplorer.Api.Models.Dtos;
using Training.FileExplorer.Application.FileStorage.Models.Storage;

namespace Training.FileExplorer.Api.Mappers;

public class DriveProfile : Profile
{
    public DriveProfile()
    {
        CreateMap<StorageDrive, StorageDriveDto>().ReverseMap();
    }
}