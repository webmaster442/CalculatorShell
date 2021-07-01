using System.Runtime.Serialization;
using System.ServiceModel;

namespace CalculatorShell.Web.MnbRate.Domain
{
    [DataContract(Name = "GetCurrenciesRequestBody", Namespace = "http://www.mnb.hu/webservices/")]
    internal class GetCurrenciesRequestBody : IExtensibleDataObject
    {
        public ExtensionDataObject? ExtensionData { get; set; }
    }

    [DataContract(Name = "GetCurrenciesResponseBody", Namespace = "http://www.mnb.hu/webservices/")]
    internal class GetCurrenciesResponseBody : IExtensibleDataObject
    {

        public ExtensionDataObject? ExtensionData { get; set; }

        [DataMember(EmitDefaultValue = false)]
        internal string? GetCurrenciesResult { get; set; }
    }

    [DataContract(Name = "GetCurrencyUnitsRequestBody", Namespace = "http://www.mnb.hu/webservices/")]
    internal class GetCurrencyUnitsRequestBody : IExtensibleDataObject
    {
        public ExtensionDataObject? ExtensionData { get; set; }

        [DataMember(EmitDefaultValue = false)]
        internal string? currencyNames { get; set; }
    }

    [DataContract(Name = "GetCurrencyUnitsResponseBody", Namespace = "http://www.mnb.hu/webservices/")]
    internal class GetCurrencyUnitsResponseBody : IExtensibleDataObject
    {
        public ExtensionDataObject? ExtensionData { get; set; }

        [DataMember(EmitDefaultValue = false)]
        internal string? GetCurrencyUnitsResult { get; set; }
    }

    [DataContract(Name = "GetCurrentExchangeRatesRequestBody", Namespace = "http://www.mnb.hu/webservices/")]
    internal class GetCurrentExchangeRatesRequestBody : IExtensibleDataObject
    {
        public ExtensionDataObject? ExtensionData { get; set; }
    }

    [DataContract(Name = "GetCurrentExchangeRatesResponseBody", Namespace = "http://www.mnb.hu/webservices/")]
    internal class GetCurrentExchangeRatesResponseBody : IExtensibleDataObject
    {
        public ExtensionDataObject? ExtensionData { get; set; }

        [DataMember(EmitDefaultValue = false)]
        internal string? GetCurrentExchangeRatesResult { get; set; }
    }

    [DataContract(Name = "GetDateIntervalRequestBody", Namespace = "http://www.mnb.hu/webservices/")]
    internal class GetDateIntervalRequestBody : IExtensibleDataObject
    {
        public ExtensionDataObject? ExtensionData { get; set; }
    }

    [DataContract(Name = "GetDateIntervalResponseBody", Namespace = "http://www.mnb.hu/webservices/")]
    internal class GetDateIntervalResponseBody : IExtensibleDataObject
    {
        public ExtensionDataObject? ExtensionData { get; set; }

        [DataMember(EmitDefaultValue = false)]
        internal string? GetDateIntervalResult { get; set; }
    }

    [DataContract(Name = "GetExchangeRatesRequestBody", Namespace = "http://www.mnb.hu/webservices/")]
    internal class GetExchangeRatesRequestBody : IExtensibleDataObject
    {

        public ExtensionDataObject? ExtensionData { get; set; }

        [DataMember(EmitDefaultValue = false)]
        internal string? startDate { get; set; }

        [DataMember(EmitDefaultValue = false, Order = 1)]
        internal string? endDate { get; set; }

        [DataMember(EmitDefaultValue = false, Order = 2)]
        internal string? currencyNames { get; set; }
    }

    [DataContract(Name = "GetExchangeRatesResponseBody", Namespace = "http://www.mnb.hu/webservices/")]
    internal class GetExchangeRatesResponseBody : IExtensibleDataObject
    {
        public ExtensionDataObject? ExtensionData { get; set; }

        [DataMember(EmitDefaultValue = false)]
        internal string? GetExchangeRatesResult { get; set; }
    }

    [DataContract(Name = "GetInfoRequestBody", Namespace = "http://www.mnb.hu/webservices/")]
    internal class GetInfoRequestBody : IExtensibleDataObject
    {
        public ExtensionDataObject? ExtensionData { get; set; }
    }

    [DataContract(Name = "GetInfoResponseBody", Namespace = "http://www.mnb.hu/webservices/")]
    internal class GetInfoResponseBody : IExtensibleDataObject
    {
        public ExtensionDataObject? ExtensionData { get; set; }

        [DataMember(EmitDefaultValue = false)]
        internal string? GetInfoResult { get; set; }
    }

    [MessageContract(IsWrapped = false)]
    internal class GetCurrenciesRequest
    {

        [MessageBodyMember(Namespace = "http://www.mnb.hu/webservices/", Order = 0)]
        public GetCurrenciesRequestBody? GetCurrencies;

        public GetCurrenciesRequest()
        {
        }

        public GetCurrenciesRequest(GetCurrenciesRequestBody GetCurrencies)
        {
            this.GetCurrencies = GetCurrencies;
        }
    }

    [MessageContract(IsWrapped = false)]
    internal class GetCurrenciesResponse
    {

        [MessageBodyMember(Name = "GetCurrenciesResponse", Namespace = "http://www.mnb.hu/webservices/", Order = 0)]
        public GetCurrenciesResponseBody? GetCurrenciesResponse1;

        public GetCurrenciesResponse()
        {
        }

        public GetCurrenciesResponse(GetCurrenciesResponseBody GetCurrenciesResponse1)
        {
            this.GetCurrenciesResponse1 = GetCurrenciesResponse1;
        }
    }

    [MessageContract(IsWrapped = false)]
    internal class GetCurrencyUnitsRequest
    {

        [MessageBodyMember(Namespace = "http://www.mnb.hu/webservices/", Order = 0)]
        public GetCurrencyUnitsRequestBody? GetCurrencyUnits;

        public GetCurrencyUnitsRequest()
        {
        }

        public GetCurrencyUnitsRequest(GetCurrencyUnitsRequestBody GetCurrencyUnits)
        {
            this.GetCurrencyUnits = GetCurrencyUnits;
        }
    }

    [MessageContract(IsWrapped = false)]
    internal class GetCurrencyUnitsResponse
    {
        [MessageBodyMember(Name = "GetCurrencyUnitsResponse", Namespace = "http://www.mnb.hu/webservices/", Order = 0)]
        public GetCurrencyUnitsResponseBody? GetCurrencyUnitsResponse1;

        public GetCurrencyUnitsResponse()
        {
        }

        public GetCurrencyUnitsResponse(GetCurrencyUnitsResponseBody GetCurrencyUnitsResponse1)
        {
            this.GetCurrencyUnitsResponse1 = GetCurrencyUnitsResponse1;
        }
    }

    [MessageContract(IsWrapped = false)]
    internal class GetCurrentExchangeRatesRequest
    {

        [MessageBodyMember(Namespace = "http://www.mnb.hu/webservices/", Order = 0)]
        public GetCurrentExchangeRatesRequestBody? GetCurrentExchangeRates;

        public GetCurrentExchangeRatesRequest()
        {
        }

        public GetCurrentExchangeRatesRequest(GetCurrentExchangeRatesRequestBody GetCurrentExchangeRates)
        {
            this.GetCurrentExchangeRates = GetCurrentExchangeRates;
        }
    }

    [MessageContract(IsWrapped = false)]
    internal class GetCurrentExchangeRatesResponse
    {

        [MessageBodyMember(Name = "GetCurrentExchangeRatesResponse", Namespace = "http://www.mnb.hu/webservices/", Order = 0)]
        public GetCurrentExchangeRatesResponseBody? GetCurrentExchangeRatesResponse1;

        public GetCurrentExchangeRatesResponse()
        {
        }

        public GetCurrentExchangeRatesResponse(GetCurrentExchangeRatesResponseBody GetCurrentExchangeRatesResponse1)
        {
            this.GetCurrentExchangeRatesResponse1 = GetCurrentExchangeRatesResponse1;
        }
    }

    [MessageContract(IsWrapped = false)]
    internal class GetDateIntervalRequest
    {

        [MessageBodyMember(Namespace = "http://www.mnb.hu/webservices/", Order = 0)]
        public GetDateIntervalRequestBody? GetDateInterval;

        public GetDateIntervalRequest()
        {
        }

        public GetDateIntervalRequest(GetDateIntervalRequestBody GetDateInterval)
        {
            this.GetDateInterval = GetDateInterval;
        }
    }

    [MessageContract(IsWrapped = false)]
    internal class GetDateIntervalResponse
    {

        [MessageBodyMember(Name = "GetDateIntervalResponse", Namespace = "http://www.mnb.hu/webservices/", Order = 0)]
        public GetDateIntervalResponseBody? GetDateIntervalResponse1;

        public GetDateIntervalResponse()
        {
        }

        public GetDateIntervalResponse(GetDateIntervalResponseBody GetDateIntervalResponse1)
        {
            this.GetDateIntervalResponse1 = GetDateIntervalResponse1;
        }
    }

    [MessageContract(IsWrapped = false)]
    internal class GetExchangeRatesRequest
    {

        [MessageBodyMember(Namespace = "http://www.mnb.hu/webservices/", Order = 0)]
        public GetExchangeRatesRequestBody? GetExchangeRates;

        public GetExchangeRatesRequest()
        {
        }

        public GetExchangeRatesRequest(GetExchangeRatesRequestBody GetExchangeRates)
        {
            this.GetExchangeRates = GetExchangeRates;
        }
    }

    [MessageContract(IsWrapped = false)]
    internal class GetExchangeRatesResponse
    {

        [MessageBodyMember(Name = "GetExchangeRatesResponse", Namespace = "http://www.mnb.hu/webservices/", Order = 0)]
        public GetExchangeRatesResponseBody? GetExchangeRatesResponse1;

        public GetExchangeRatesResponse()
        {
        }

        public GetExchangeRatesResponse(GetExchangeRatesResponseBody GetExchangeRatesResponse1)
        {
            this.GetExchangeRatesResponse1 = GetExchangeRatesResponse1;
        }
    }

    [MessageContract(IsWrapped = false)]
    internal class GetInfoRequest
    {

        [MessageBodyMember(Namespace = "http://www.mnb.hu/webservices/", Order = 0)]
        public GetInfoRequestBody? GetInfo;

        public GetInfoRequest()
        {
        }

        public GetInfoRequest(GetInfoRequestBody GetInfo)
        {
            this.GetInfo = GetInfo;
        }
    }

    [MessageContract(IsWrapped = false)]
    internal class GetInfoResponse
    {

        [MessageBodyMember(Name = "GetInfoResponse", Namespace = "http://www.mnb.hu/webservices/", Order = 0)]
        public GetInfoResponseBody? GetInfoResponse1;

        public GetInfoResponse()
        {
        }

        public GetInfoResponse(GetInfoResponseBody GetInfoResponse1)
        {
            this.GetInfoResponse1 = GetInfoResponse1;
        }
    }
}