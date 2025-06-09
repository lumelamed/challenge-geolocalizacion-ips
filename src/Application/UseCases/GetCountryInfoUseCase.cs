namespace Application.UseCases
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Application.DTOs.Responses;
    using Application.Exceptions;
    using Application.Interfaces;
    using Domain.Abstractions;
    using Domain.Entities;

    public class GetCountryInfoUseCase
    {
        public GetCountryInfoUseCase(
            ILocationService ipLocationService,
            IUnitOfWork unitOfWork)
        {
            this.IpLocationService = ipLocationService;
            this.UnitOfWork = unitOfWork;
        }

        private ILocationService IpLocationService { get; }

        private IUnitOfWork UnitOfWork { get; }

        public async Task<IpInfoResponseDTO> ExecuteAsync(string ip, CancellationToken cancellationToken)
        {
            if (!IsValidIp(ip))
            {
                throw new ArgumentException("IP no válida");
            }

            var countryCode = await this.IpLocationService.GetCountryCodeByIP(ip) ?? throw new CountryNotFoundException(ip);

            var country = await this.UnitOfWork.CountryRepository.GetByCodeAsync(countryCode, cancellationToken);

            if (country is null)
            {
                var countryInfo = await this.IpLocationService.GetCountryInfoByCode(ip);

                country = new Country
                {
                    Name = countryInfo.CountryName,
                    Currency = countryInfo.Currency,
                    DistanceToBuenosAiresInKm = countryInfo.DistanceToBuenosAiresKm,
                    ISOCode = countryInfo.ISOCode,
                    Latitude = countryInfo.Latitude,
                    Longitude = countryInfo.Longitude,
                    Languages = countryInfo.Languages is not null && countryInfo.Languages.Count != 0 ? string.Join(",", countryInfo.Languages) : null,
                    Timezones = countryInfo.Timezones is not null && countryInfo.Timezones.Count != 0 ? string.Join(",", countryInfo.Timezones) : null,
                };

                await this.UnitOfWork.CountryRepository.AddAsync(country, cancellationToken);
            }

            var query = new IpInfo
            {
                IP = ip,
                Country = country,
                RequestDate = DateTime.UtcNow,
            };

            await this.UnitOfWork.IpInfoRepository.AddAsync(query, cancellationToken);
            await this.UnitOfWork.SaveAsync(cancellationToken);

            var rate = !string.IsNullOrWhiteSpace(country.Currency)
                ? await this.IpLocationService.GetDollarExchangeRateByCurrencyCode(country.Currency)
                : null;

            return new IpInfoResponseDTO
            {
                IP = ip,
                CountryName = country.Name,
                ISOCode = country.ISOCode,
                Currency = country.Currency,
                CurrentTimes = country.Timezones is null ? [] : [.. country.Timezones.Split(",").Select(x => TimeOnly.FromDateTime(TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(x))))],
                Languages = country.Languages?.Split(",")?.ToList() ?? [],
                Latitude = country.Latitude,
                Longitude = country.Longitude,
                DistanceToBuenosAiresKm = country.DistanceToBuenosAiresInKm,
                ExchangeRateToUSD = rate,
            };
        }

        private static bool IsValidIp(string ip)
        {
            return IPAddress.TryParse(ip, out _);
        }
    }
}
