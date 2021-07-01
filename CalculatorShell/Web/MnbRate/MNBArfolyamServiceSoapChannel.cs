using System.ServiceModel;

namespace CalculatorShell.Web.MnbRate
{
#pragma warning disable S101 // Types should be named in PascalCase
    internal interface MNBArfolyamServiceSoapChannel : IMNBArfolyamServiceSoap, IClientChannel
#pragma warning restore S101 // Types should be named in PascalCase
    {
    }
}
