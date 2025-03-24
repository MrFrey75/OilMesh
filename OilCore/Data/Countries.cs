namespace OilCore.Data;

public static class Countries
{
    public static readonly Dictionary<string, string> CountryNames = new()
    {
        // North America
        { "US", "United States" },
        { "CA", "Canada" },
        { "MX", "Mexico" },

        // Central America & Caribbean
        { "BZ", "Belize" },
        { "CR", "Costa Rica" },
        { "SV", "El Salvador" },
        { "GT", "Guatemala" },
        { "HN", "Honduras" },
        { "NI", "Nicaragua" },
        { "PA", "Panama" },
        { "CU", "Cuba" },
        { "DO", "Dominican Republic" },
        { "HT", "Haiti" },
        { "JM", "Jamaica" },
        { "TT", "Trinidad and Tobago" },
        { "PR", "Puerto Rico" }, // U.S. Territory

        // South America
        { "AR", "Argentina" },
        { "BO", "Bolivia" },
        { "BR", "Brazil" },
        { "CL", "Chile" },
        { "CO", "Colombia" },
        { "EC", "Ecuador" },
        { "GY", "Guyana" },
        { "PE", "Peru" },
        { "PY", "Paraguay" },
        { "SR", "Suriname" },
        { "UY", "Uruguay" },
        { "VE", "Venezuela" },

        // Europe
        { "GB", "United Kingdom" },
        { "FR", "France" },
        { "DE", "Germany" },
        { "IT", "Italy" },
        { "ES", "Spain" },
        { "NL", "Netherlands" },
        { "BE", "Belgium" },
        { "CH", "Switzerland" },
        { "AT", "Austria" },
        { "SE", "Sweden" },
        { "NO", "Norway" },
        { "FI", "Finland" },
        { "DK", "Denmark" },
        { "PL", "Poland" },
        { "PT", "Portugal" },
        { "IE", "Ireland" },
        { "GR", "Greece" },
        { "RU", "Russia" },
        { "UA", "Ukraine" },

        // Asia
        { "CN", "China" },
        { "IN", "India" },
        { "JP", "Japan" },
        { "KR", "South Korea" },
        { "ID", "Indonesia" },
        { "TH", "Thailand" },
        { "VN", "Vietnam" },
        { "MY", "Malaysia" },
        { "SG", "Singapore" },
        { "PH", "Philippines" },

        // Middle East
        { "AE", "United Arab Emirates" },
        { "SA", "Saudi Arabia" },
        { "IR", "Iran" },
        { "IQ", "Iraq" },
        { "IL", "Israel" },
        { "TR", "Turkey" },

        // Africa
        { "EG", "Egypt" },
        { "ZA", "South Africa" },
        { "NG", "Nigeria" },
        { "KE", "Kenya" },
        { "GH", "Ghana" },
        { "DZ", "Algeria" },
        { "MA", "Morocco" },

        // Oceania
        { "AU", "Australia" },
        { "NZ", "New Zealand" },
        { "FJ", "Fiji" },
        { "PG", "Papua New Guinea" },

        // Others
        { "VA", "Vatican City" }
    };
}