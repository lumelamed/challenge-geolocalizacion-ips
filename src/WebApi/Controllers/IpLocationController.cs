namespace WebApi.Controllers
{
    using Application.DTOs.Responses;
    using Application.UseCases;
    using Domain.Abstractions;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Produces("application/json")]
    [Route("api/iplocation")]
    public class IpLocationController : ControllerBase
    {
        public IpLocationController(
            ILogger<IpLocationController> logger,
            GetCountryInfoUseCase getCountryInfoUseCase,
            GetStatisticsUseCase getStatisticsUseCase)
        {
            this.Logger = logger;
            this.GetCountryInfoUseCase = getCountryInfoUseCase;
            this.GetStatisticsUseCase = getStatisticsUseCase;
        }

        private ILogger<IpLocationController> Logger { get; }

        private GetCountryInfoUseCase GetCountryInfoUseCase { get; }

        private GetStatisticsUseCase GetStatisticsUseCase { get; }

        [HttpGet("{ip}")]
        [ProducesResponseType(typeof(ApiResponse<IpInfoResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetIpInfo([FromRoute] string ip, CancellationToken cancellationToken)
        {
            var result = await this.GetCountryInfoUseCase.ExecuteAsync(ip, cancellationToken);
            return this.Ok(ApiResponse<IpInfoResponseDTO>.Success(result));
        }

        [HttpGet("statistics")]
        [ProducesResponseType(typeof(ApiResponse<StatisticsResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStatistics(CancellationToken cancellationToken)
        {
            var stats = await this.GetStatisticsUseCase.ExecuteAsync(cancellationToken);
            return this.Ok(ApiResponse<StatisticsResponseDTO>.Success(stats));
        }
    }
}
