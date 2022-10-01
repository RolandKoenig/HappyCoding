using HappyCoding.AvaloniaAppWithDataGrid.Util;

namespace HappyCoding.AvaloniaAppWithDataGrid.Data;

public class TestDataRow : PropertyChangedBase
{
    private int _valueInt;
    private string _valueString = string.Empty;
    private bool _valueBool;
    
    public int ValueInt
    {
        get => _valueInt;
        set => this.SetField(ref _valueInt, value);
    }

    public string ValueString
    {
        get => _valueString;
        set => this.SetField(ref _valueString, value);
    }

    public bool ValueBool
    {
        get => _valueBool;
        set => this.SetField(ref _valueBool, value);
    }
}