using Newtonsoft.Json;
using System.Text;

namespace GoPay.Extensions
{
    public static class GPClientExceptionExtensions
    {
        public static string GetErrors(this GPClientException exception)
        {
            if (exception.Error != null)
            {
                return JsonConvert.SerializeObject(exception.Error.ErrorMessages);
            }
            return null;
        }
    }
}
