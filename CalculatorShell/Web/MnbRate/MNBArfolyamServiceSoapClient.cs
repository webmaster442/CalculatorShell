using CalculatorShell.Web.MnbRate.Domain;
using System.ServiceModel;
using System.Threading.Tasks;

namespace CalculatorShell.Web.MnbRate
{
#pragma warning disable S101 // Types should be named in PascalCase
    internal class MNBArfolyamServiceSoapClient : ClientBase<IMNBArfolyamServiceSoap>, IMNBArfolyamServiceSoap
#pragma warning restore S101 // Types should be named in PascalCase
    {
        public MNBArfolyamServiceSoapClient()
        {
        }

        public MNBArfolyamServiceSoapClient(string endpointConfigurationName) :
                base(endpointConfigurationName)
        {
        }

        public MNBArfolyamServiceSoapClient(string endpointConfigurationName, string remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public MNBArfolyamServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public MNBArfolyamServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        public GetCurrenciesResponse GetCurrencies(GetCurrenciesRequest request)
        {
            return Channel.GetCurrencies(request);
        }

        public Task<GetCurrenciesResponse> GetCurrenciesAsync(GetCurrenciesRequest request)
        {
            return Channel.GetCurrenciesAsync(request);
        }

        public GetCurrencyUnitsResponse GetCurrencyUnits(GetCurrencyUnitsRequest request)
        {
            return Channel.GetCurrencyUnits(request);
        }

        public Task<GetCurrencyUnitsResponse> GetCurrencyUnitsAsync(GetCurrencyUnitsRequest request)
        {
            return Channel.GetCurrencyUnitsAsync(request);
        }

        public GetCurrentExchangeRatesResponse GetCurrentExchangeRates(GetCurrentExchangeRatesRequest request)
        {
            return Channel.GetCurrentExchangeRates(request);
        }

        public Task<GetCurrentExchangeRatesResponse> GetCurrentExchangeRatesAsync(GetCurrentExchangeRatesRequest request)
        {
            return Channel.GetCurrentExchangeRatesAsync(request);
        }

        public GetDateIntervalResponse GetDateInterval(GetDateIntervalRequest request)
        {
            return Channel.GetDateInterval(request);
        }

        public Task<GetDateIntervalResponse> GetDateIntervalAsync(GetDateIntervalRequest request)
        {
            return Channel.GetDateIntervalAsync(request);
        }

        public GetExchangeRatesResponse GetExchangeRates(GetExchangeRatesRequest request)
        {
            return Channel.GetExchangeRates(request);
        }

        public Task<GetExchangeRatesResponse> GetExchangeRatesAsync(GetExchangeRatesRequest request)
        {
            return Channel.GetExchangeRatesAsync(request);
        }

        public GetInfoResponse GetInfo(GetInfoRequest request)
        {
            return Channel.GetInfo(request);
        }

        public Task<GetInfoResponse> GetInfoAsync(GetInfoRequest request)
        {
            return Channel.GetInfoAsync(request);
        }
    }
}