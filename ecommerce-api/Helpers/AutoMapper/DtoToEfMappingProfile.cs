using ecommerce_api.DTO;
using ecommerce_api.Models;
using AutoMapper;
using System;
using System.Linq;
using CodeUtility;

namespace ecommerce_api.Helpers.AutoMapper
{
    public class DtoToEfMappingProfile : Profile
    {
        public DtoToEfMappingProfile()
        {
            var ct = DateTime.Now;


            CreateMap<UserForDetailDto, User>();
       
            

            //CreateMap<AuditTypeDto, MES_Audit_Type_M>();
        }
    }
}