using Tesonet.ServerProxy.Dto;
using Tesonet.ServerProxy.Exceptions;

namespace Tesonet.ServerProxy.Extensions
{
    public static class ServerDtoExtensions
    {
        private static readonly char SplitSymbol = '#';

        public static string ServerCountryName(this ServerDto dto)
        {
            var splits = dto.Name.Split(SplitSymbol);
            if (splits.Length != 2)
            {
                throw new RetrieveServerCountryNameException();
            }

            return splits[0];
        }

        public static int ServerNumber(this ServerDto dto)
        {
            var splits = dto.Name.Split(SplitSymbol);
            if (splits.Length != 2)
            {
                throw new RetrieveServerNumberException();
            }

            if (!int.TryParse(splits[1], out int number))
            {
                throw new RetrieveServerNumberException();
            }

            return number;
        }

        public static double ServerDistance(this ServerDto dto)
        {
            if (!int.TryParse(dto.Distance, out int distance))
            {
                throw new RetrieveServerDistanceException();
            }

            return distance;
        }
    }
}