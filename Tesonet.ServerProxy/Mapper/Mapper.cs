using Tesonet.Domain.Domain;
using Tesonet.ServerProxy.Dto;
using Tesonet.ServerProxy.Extensions;

namespace Tesonet.ServerProxy.Mapper
{
    public static class Mapper
    {
        //todo: Use Automapper
        public static Server MapToServer(this ServerDto dto)
        {
            return new Server
            {
                Number = dto.ServerNumber(),
                Country = dto.ServerCountryName(),
                Distance = dto.ServerDistance()
            };
        }
    }
}