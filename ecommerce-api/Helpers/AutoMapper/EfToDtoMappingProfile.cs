using System;
using ecommerce_api.DTO;
using ecommerce_api.Models;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using ecommerce_api.Constants;
using CodeUtility;

namespace ecommerce_api.Helpers.AutoMapper
{
    public class EfToDtoMappingProfile : Profile
    {
        char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        public EfToDtoMappingProfile()
        {
           
        }

    }
}