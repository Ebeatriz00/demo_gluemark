using Application.DTOs.Currency;
using AutoMapper;
using Core.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Currency
{
    public class GetAllCurrency
    {
        private readonly ICurrencyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllCurrency(ICurrencyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<CurrencyResponseDto>> ExecuteAsync(int businessId)
        {
            var entities = await _repository.GetAllAsync(businessId);
            return _mapper.Map<List<CurrencyResponseDto>>(entities);
        }
    }
}
