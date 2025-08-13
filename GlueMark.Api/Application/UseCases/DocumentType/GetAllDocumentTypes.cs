using Application.DTOs.DocumentType;
using AutoMapper;
using Core.Interfaces;

namespace Application.UseCases.DocumentType;

public class GetAllDocumentTypes
{
    private readonly IDocumentTypeRepository _repository;
    private readonly IMapper _mapper;

    public GetAllDocumentTypes(IDocumentTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<DocumentTypeResponseDto>> ExecuteAsync(int businessId)
    {
        var entities = await _repository.GetAllAsync(businessId);
        return _mapper.Map<List<DocumentTypeResponseDto>>(entities);
    }
}
