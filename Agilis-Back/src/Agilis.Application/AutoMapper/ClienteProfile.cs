using Agilis.Application.ViewModels.Clientes;
using Agilis.Core.Domain.Models.Entities;
using AutoMapper;

namespace Agilis.Application.AutoMapper
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteViewModel>()
                .ReverseMap();
        }
    }
}
