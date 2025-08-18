using System;

namespace PaymentApi.Models;

public class MidtransResponseLog : BaseEntity
{
    public long Id { get; set; }
    public long MidtransRequestLogId { get; set; }
    public MidtransRequestLog? MidtransRequestLog { get; set; }
    public string? Header { get; set; }
    public string Body { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}
