using AutoMapper;
using Zaka.Core.API.Application.Dtos;
using Zaka.Core.API.Domain.Entities;

namespace Zaka.Core.API.Application.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CreateAccountDto, Account>();
            CreateMap<TopUpWalletDto, Wallet>();
        }
    }
}