using System.Text;

namespace GoPay.Extensions
{
    public static class GPClientExceptionExtensions
    {
        public static string GetErrors(this GPClientException exception)
        {
            var sb = new StringBuilder();

            if (exception.Error != null)
            {
                foreach (var error in exception.Error.ErrorMessages)
                {
                    sb.Append($"ErrorName: {error.ErrorName}; ErrorDescription: {error.Description}");
                }
            }

            return sb.ToString();
        }
    }
}
