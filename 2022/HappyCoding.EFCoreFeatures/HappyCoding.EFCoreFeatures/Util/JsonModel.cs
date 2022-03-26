namespace HappyCoding.EFCoreFeatures.Util;

public class JsonModel<T>
    where T : class
{
    private byte[]? _rawBytes;
    private T? _modelInstance;
    private bool _modelInstanceChanged;

    public T ModelInstance
    {
        get
        {
            if (_modelInstance != null) { return _modelInstance; }
            
            _modelInstance = JsonModelSerializer.DeserializeJsonModel<T>(_rawBytes!);
            return _modelInstance;
        }
        set
        {
            if (_modelInstance != value)
            {
                _rawBytes = null;
                _modelInstance = value;
                _modelInstanceChanged = true;
            }
        }
    }

    public byte[] RawBytes
    {
        get
        {
            if (_modelInstanceChanged)
            {
                _rawBytes = JsonModelSerializer.SerializeJsonModel(_modelInstance);
                _modelInstanceChanged = false;
                return _rawBytes;
            }
            else
            {
                if (_rawBytes != null) { return _rawBytes; }

                _rawBytes = JsonModelSerializer.SerializeJsonModel(_modelInstance);
                _modelInstanceChanged = false;
                return _rawBytes;
            }
        }
    }

    public bool HasChanges => _modelInstanceChanged;

    /// <summary>
    /// Creates a <see cref="JsonModel{T}"/> based on the given raw serialized and compressed data.
    /// </summary>
    /// <param name="rawBytes">The serialized and compressed model.</param>
    public JsonModel(byte[] rawBytes)
    {
        _rawBytes = rawBytes;
        _modelInstance = null;
        _modelInstanceChanged = false;
    }

    /// <summary>
    /// Creates a <see cref="JsonModel{T}"/> based on the model.
    /// </summary>
    /// <param name="modelInstance">The model.</param>
    public JsonModel(T modelInstance)
    {
        _rawBytes = null;
        _modelInstance = modelInstance;
        _modelInstanceChanged = false;
    }

    public void UpdateModel(Action<T> modelUpdater)
    {
        modelUpdater(this.ModelInstance);
        _modelInstanceChanged = true;
    }
}
