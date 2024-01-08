using AutoMapper;
using Training.FileExplorer.Api.Models.Dtos;
using Training.FileExplorer.Application.FileStorage.Models.Storage;

namespace Training.FileExplorer.Api.Mappers;

public class StorageProfile : Profile
{
    public StorageProfile()
    {
        CreateMap<IStorageEntry, IStorageDto>().ReverseMap();
    }
}