using CalculatorShell.Web.MnbRate.Domain;
using System.ServiceModel;
using System.Threading.Tasks;

namespace CalculatorShell.Web.MnbRate
{
    [ServiceContract(Namespace = "http://www.mnb.hu/webservices/", ConfigurationName = "MNBArfolyamServiceSoap")]
#pragma warning disable S101 // Types should be named in PascalCase
    internal interface IMNBArfolyamServiceSoap
#pragma warning restore S101 // Types should be named in PascalCase
    {
        [OperationContract(Action = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrencies", ReplyAction = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrenciesResponse")]
        [FaultContract(typeof(string), Action = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrenciesStringFault", Name = "string", Namespace = "http://schemas.microsoft.com/2003/10/Serialization/")]
        GetCurrenciesResponse GetCurrencies(GetCurrenciesRequest request);

        [OperationContract(Action = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrencies", ReplyAction = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrenciesResponse")]
        Task<GetCurrenciesResponse> GetCurrenciesAsync(GetCurrenciesRequest request);

        [OperationContract(Action = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrencyUnits", ReplyAction = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrencyUnitsResponse")]
        [FaultContract(typeof(string), Action = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrencyUnitsStringFault", Name = "string", Namespace = "http://schemas.microsoft.com/2003/10/Serialization/")]
        GetCurrencyUnitsResponse GetCurrencyUnits(GetCurrencyUnitsRequest request);

        [OperationContract(Action = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrencyUnits", ReplyAction = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrencyUnitsResponse")]
        Task<GetCurrencyUnitsResponse> GetCurrencyUnitsAsync(GetCurrencyUnitsRequest request);

        [OperationContract(Action = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrentExchangeRates", ReplyAction = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrentExchangeRatesRespo" +
            "nse")]
        [FaultContract(typeof(string), Action = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrentExchangeRatesStrin" +
            "gFault", Name = "string", Namespace = "http://schemas.microsoft.com/2003/10/Serialization/")]
        GetCurrentExchangeRatesResponse GetCurrentExchangeRates(GetCurrentExchangeRatesRequest request);

        [OperationContract(Action = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrentExchangeRates", ReplyAction = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetCurrentExchangeRatesRespo" +
            "nse")]
        Task<GetCurrentExchangeRatesResponse> GetCurrentExchangeRatesAsync(GetCurrentExchangeRatesRequest request);

        [OperationContract(Action = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetDateInterval", ReplyAction = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetDateIntervalResponse")]
        [FaultContract(typeof(string), Action = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetDateIntervalStringFault", Name = "string", Namespace = "http://schemas.microsoft.com/2003/10/Serialization/")]
        GetDateIntervalResponse GetDateInterval(GetDateIntervalRequest request);

        [OperationContract(Action = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetDateInterval", ReplyAction = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetDateIntervalResponse")]
        Task<GetDateIntervalResponse> GetDateIntervalAsync(GetDateIntervalRequest request);

        [OperationContract(Action = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetExchangeRates", ReplyAction = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetExchangeRatesResponse")]
        [FaultContract(typeof(string), Action = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetExchangeRatesStringFault", Name = "string", Namespace = "http://schemas.microsoft.com/2003/10/Serialization/")]
        GetExchangeRatesResponse GetExchangeRates(GetExchangeRatesRequest request);

        [OperationContract(Action = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetExchangeRates", ReplyAction = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetExchangeRatesResponse")]
        Task<GetExchangeRatesResponse> GetExchangeRatesAsync(GetExchangeRatesRequest request);

        [OperationContract(Action = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetInfo", ReplyAction = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetInfoResponse")]
        [FaultContract(typeof(string), Action = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetInfoStringFault", Name = "string", Namespace = "http://schemas.microsoft.com/2003/10/Serialization/")]
        GetInfoResponse GetInfo(GetInfoRequest request);

        [OperationContract(Action = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetInfo", ReplyAction = "http://www.mnb.hu/webservices/MNBArfolyamServiceSoap/GetInfoResponse")]
        Task<GetInfoResponse> GetInfoAsync(GetInfoRequest request);
    }
}
