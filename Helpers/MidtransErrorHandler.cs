namespace PaymentApi.Helpers
{
    using System.Text.Json;

    public static class MidtransErrorHandler
    {
        public static (string Message, int StatusCode, int MetaCode) Handle(Exception ex)
        {
            string rawMessage = ex.InnerException?.Message ?? ex.Message;

            int statusCode = 500;
            int metaCode = 50000;

            string message = rawMessage; // default isi string apa adanya

            if (rawMessage.Contains("BadRequest", StringComparison.OrdinalIgnoreCase) ||
                rawMessage.Contains("Conflict", StringComparison.OrdinalIgnoreCase))
            {
                statusCode = 400;
                metaCode = 40000;

                try
                {
                    int jsonStart = rawMessage.IndexOf('{');
                    if (jsonStart >= 0)
                    {
                        string jsonPart = rawMessage.Substring(jsonStart);
                        using var doc = JsonDocument.Parse(jsonPart);

                        if (doc.RootElement.TryGetProperty("error_messages", out var arr))
                        {
                            var messages = arr.EnumerateArray()
                                              .Select(e => e.GetString() ?? string.Empty)
                                              .ToArray();

                            message = string.Join("; ", messages); // gabungkan jadi 1 string
                        }
                    }
                }
                catch
                {
                    message = rawMessage;
                }
            }
            else if (rawMessage.Contains("timeout", StringComparison.OrdinalIgnoreCase))
            {
                statusCode = 504;
                metaCode = 50400;
            }

            return (message, statusCode, metaCode);
        }
    }
}
