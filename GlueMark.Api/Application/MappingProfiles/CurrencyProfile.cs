using Application.DTOs.Currency;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles
{
    public class CurrencyProfile : Profile
    {
        public CurrencyProfile() {
            CreateMap<CurrencyCreateDto, Currency>();
            CreateMap<CurrencyUpdateDto, Currency>();
            CreateMap<Currency, CurrencyResponseDto>();
        }
    }
}
