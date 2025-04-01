using OilCore.Models.Base;

namespace OilCore.Models;

public class Manufacturer : ManufacturerModel
{
    public Manufacturer() : base() { }

    public Manufacturer(string name, string website)
        : base(name, website) { }
}