using Application.DTOs.DocumentType;
using AutoMapper;
using Core.Entities;

//using Core.Entities;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.MappingProfiles;

public class DocumentTypeProfile : Profile
{
    public DocumentTypeProfile()
    {
        CreateMap<DocumentTypeCreateDto, DocumentType>();
        CreateMap<DocumentTypeUpdateDto, DocumentType>();
        CreateMap<DocumentType, DocumentTypeResponseDto>();
    }
}
