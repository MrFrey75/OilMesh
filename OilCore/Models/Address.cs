using System.ComponentModel.DataAnnotations;

namespace OilCore.Models;

public class Address : Location
{
    [MaxLength(100)]
    public string AttentionTo { get; set; } = string.Empty;

    public bool IsPrimary { get; set; } = false;

    public bool IsMailingAddress { get; set; } = false;

    public bool IsBillingAddress { get; set; } = false;

    public string GetFullLabel() =>
        $"{(string.IsNullOrWhiteSpace(AttentionTo) ? "" : AttentionTo + "\n")}{ToString()}";

    public string ToFormattedBlock() =>
        $"{LocationName}\n{Street}\n{City}, {State} {ZipCode}\n{Country}";
}