using System;
using System.Text.Json.Serialization;
using HappyCoding.AvaloniaAppWithDataGrid.Util;

namespace HappyCoding.AvaloniaAppWithDataGrid.Data;

public class TestDataRow : PropertyChangedBase
{
    private string _name = "";
    private string _postalZip = "";
    private string _address = "sfsadf";
    private string _country = "asdsaf";
    private string _region = "sadfsaf";
    private bool _status = false;
    private DateTime _birthDate = DateTime.MinValue;

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

    public bool Status
    {
        get => _status;
        set => this.SetField(ref _status, value);
    }

    [JsonConverter(typeof(JsonDateParser))]
    public DateTime BirthDate
    {
        get => _birthDate;
        set => this.SetField(ref _birthDate, value);
    }
}