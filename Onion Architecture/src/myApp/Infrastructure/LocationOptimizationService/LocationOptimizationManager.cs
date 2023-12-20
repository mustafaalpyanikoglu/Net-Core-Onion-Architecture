using Application.Features.LocationSolvers.Dtos;
using Application.Services.LocationSolverService;
using Infrastructure.Utilities.Helpers;
using Infrastructure.Utilities.HttpClientGenericRepository;
using static Infrastructure.Constants.WebServiceUrlConstants;

namespace Infrastructure.LocationOptimizationService
{
    public class LocationOptimizationManager : ILocationOptimizationService
    {
        private readonly ILocationSolverService _locationSolverService;
        private readonly IHttpClientRepository _httpClientRepository;

        public LocationOptimizationManager(ILocationSolverService locationSolverService, IHttpClientRepository httpClientRepository)
        {
            _locationSolverService = locationSolverService;
            _httpClientRepository = httpClientRepository;
        }

        public async Task<LocationSolverResult> LocationSolver()
        {
            LocationOptimizationRequestDto locationOptimizationRequestDto = await _locationSolverService.SimaulatedAnnealingQuickSortSolver();

            Body body = new Body
            {
                LocationSolver = new LocationSolver
                {
                    LocationOptimizationRequestDto = locationOptimizationRequestDto
                }
            };
            string soapRequestPayload = SoapXmlProcessor.SerializeToXml(body);   

            string responseContent = await _httpClientRepository.SendSoapRequest(ACTION_URL + LOCATION_SERVICE_URL, HttpMethod.Post, soapRequestPayload, null);
            
            LocationSolverResult response = SoapXmlProcessor.DeserializeSoapResponse(responseContent);

            return response;
        }
    }
}
