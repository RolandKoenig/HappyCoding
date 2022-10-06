using HappyCoding.AvaloniaAppWithDataGrid.Util;

namespace HappyCoding.AvaloniaAppWithDataGrid.Data;

public class TestDataRow : PropertyChangedBase
{
// "name": "Keegan Elliott",
// "postalZip": "941416",
// "address": "Ap #611-7891 Consequat Ave",
// "country": "Nigeria",
// "region": "Provence-Alpes-Côte d'Azur"


    private string _name = "";
    private string _postalZip = "";
    private string _address = "sfsadf";
    private string _country = "asdsaf";
    private string _region = "sadfsaf";

    public string Name
    {
        get => _name;
        set => this.SetField(ref _name, value);
    }

    public string PostalZip
    {
        get => _postalZip;
        set => this.SetField(ref _postalZip, value);
    }

    public string Address
    {
        get => _address;
        set => this.SetField(ref _address, value);
    }

    public string Country
    {
        get => _country;
        set => this.SetField(ref _country, value);
    }

    public string Region
    {
        get => _region;
        set => this.SetField(ref _region, value);
    }
}