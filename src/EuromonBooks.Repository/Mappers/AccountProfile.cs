using AutoMapper;
using EuromonBooks.Abstractions.Models;
using EuromonBooks.Database.Abstractions.Models;
using EuromonBooks.Database.Abstractions.ProcedureParamaters;
using EuromonBooks.Database.Abstractions.Queries;
using EuromonBooks.Domain.Abstractions.Models.Account;

namespace EuromonBooks.Repository.Mappers
{
    public sealed class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<UserModel, UserParams>()
              .ForMember(x => x.Password, opt => opt.Ignore());

            CreateMap<RoleQuery, Role>();

            CreateMap<UsersQuery, UserWithDates>();
        }
    }
}