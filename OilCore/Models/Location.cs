using System.ComponentModel.DataAnnotations;
using OilCore.Enumerations;
using OilCore.Models.Base;

namespace OilCore.Models;

public class Location : BaseEntity
{
    public Location() { }

    protected Location(AddressType type)
        => (Type, LocationName, Street, City, County, State, ZipCode, Country) =
            (type, string.Empty, string.Empty, string.Empty, MichiganCounty.Unknown, UsState.Unknown, string.Empty, "US");

    protected Location(AddressType type, string city)
        => (Type, City, County, State, ZipCode) =
            (type, city, MichiganCounty.Isabella, UsState.Michigan, string.Empty);

    protected Location(string locationName, float longitude, float latitude)
        => (LocationName, Longitude, Latitude) = (locationName, longitude, latitude);

    protected Location(AddressType type, string locationName, string street, string city, MichiganCounty county, UsState state, string zipCode, string country)
        => (Type, LocationName, Street, City, County, State, ZipCode, Country) =
            (type, locationName, street, city, county, state, zipCode, country);

    public AddressType Type { get; set; } = AddressType.Unknown;

    [MaxLength(50)] public string LocationName { get; set; } = string.Empty;
    [MaxLength(100)] public string Description { get; set; } = string.Empty;
    [MaxLength(50)] public string Street { get; set; } = string.Empty;
    [MaxLength(50)] public string City { get; set; } = string.Empty;
    [MaxLength(2)] public UsState State { get; set; } = UsState.Michigan;
    [MaxLength(50)] public MichiganCounty County { get; set; } = MichiganCounty.Isabella;
    [MaxLength(10)] public string ZipCode { get; set; } = string.Empty;
    [MaxLength(10)] public string Country { get; set; } = "US";

    public float Latitude { get; set; } = 0.0f;
    public float Longitude { get; set; } = 0.0f;

    public override string ToString() =>
        $"{LocationName}, {Street}, {City}, {State} {ZipCode}, {Country}";

    public string ToShortString() => $"{Street}, {City}, {State} {ZipCode}";
    public string ToCityStateZip() => $"{City}, {State} {ZipCode}";
    public string ToCityState() => $"{City}, {State}";
    public string ToCoords() => Latitude == 0.0f && Longitude == 0.0f ? "0.0, 0.0" : $"{Latitude}, {Longitude}";
}