﻿using Application.DTOs.Business;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMap<BusinessCreateDto, Business>();
        }
    }
}
