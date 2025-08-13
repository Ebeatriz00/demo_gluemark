using Application.DTOs.DocumentType;
using Application.Exceptions;
using AutoMapper;
using Core.Interfaces;
using FluentValidation;
using SharedKernel;
using SharedKernel.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AppValidationException = Application.Exceptions.ValidationException;

namespace Application.UseCases.DocumentType
{
    public class CreateDocumentType
    {
        private readonly IDocumentTypeRepository _repository;
        private readonly IValidator<DocumentTypeCreateDto> _validator;
        private readonly IMapper _mapper;


        public CreateDocumentType(IDocumentTypeRepository repository, IValidator<DocumentTypeCreateDto> validator, IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<GlobalResponse> ExecuteAsync(DocumentTypeCreateDto dto)
        {
            var validation = await _validator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                var errores = validation.Errors
                            .Select(e => new GlobalErrorDetail(e.ErrorCode, e.ErrorMessage))
                            .ToList();

                 throw new AppValidationException(errores);


            }

            var yaExiste = await _repository.ExistsAsync(dto.Description, dto.CodeSunat, dto.BusinessId);
            if (yaExiste)
                throw new DuplicateEntryException("El tipo de documento ya existe para este negocio.");


            var entity = _mapper.Map<Core.Entities.DocumentType>(dto);

            await _repository.AddAsync(entity);

            return new GlobalResponse
            {
                Status = 1,
                Message = "Tipo de documento creado exitosamente.",
                
            };
        }
    }
}
