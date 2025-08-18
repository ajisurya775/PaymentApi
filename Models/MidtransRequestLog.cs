using System;

namespace PaymentApi.Models;

public class MidtransRequestLog : BaseEntity
{
    public long Id { get; set; }
    public string? Header { get; set; }
    public string Body { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}

public static class TypeEnum
{
    public const string CreateCharge = "create-charge";
    public const string MidtransCallbackCharge = "midtrans-callback-charge";
    public const string PlatformCallbackCharge = "platform-callback-charge";
}

