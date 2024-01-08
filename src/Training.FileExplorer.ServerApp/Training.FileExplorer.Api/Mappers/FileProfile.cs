using AutoMapper;
using Training.FileExplorer.Api.Models.Dtos;
using Training.FileExplorer.Application.FileStorage.Models.Storage;

namespace Training.FileExplorer.Api.Mappers;

public class FileProfile : Profile
{
    public FileProfile()
    {
        CreateMap<StorageFile, StorageFileDto>().ReverseMap();
    }
}