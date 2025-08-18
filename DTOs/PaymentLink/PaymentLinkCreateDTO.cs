using System.ComponentModel.DataAnnotations;

namespace PaymentApi.DTOs.PaymentLink;

public class PaymentLinkCreateDTO
{
    [Required]
    public Guid CredentialId { get; set; }

    [Required]
    public string OrderId { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "The ChargeAmount field is required.")]
    public decimal ChargeAmount { get; set; }

    [Required]
    [Url]
    public string CallBackUrl { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

}