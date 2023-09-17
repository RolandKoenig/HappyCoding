using Microsoft.AspNetCore.DataProtection;

namespace HappyCoding.ProtectDataWithDataProtection;

public class LogicClassContainingProtector
{
    private readonly IDataProtectionProvider _dataProtectionProvider;
    
    public LogicClassContainingProtector(IDataProtectionProvider dataProtectionProvider)
    {
        _dataProtectionProvider = dataProtectionProvider;
    }

    public string ProtectSomeData(string dataAsString, string purpose = "MyDummyPurpose")
    {
        var protector = _dataProtectionProvider.CreateProtector(purpose);
        return protector.Protect(dataAsString);
    }
    
    public string UnprotectSomeData(string protectedDataAsString, string purpose = "MyDummyPurpose")
    {
        var protector = _dataProtectionProvider.CreateProtector(purpose);
        return protector.Unprotect(protectedDataAsString);
    }
}