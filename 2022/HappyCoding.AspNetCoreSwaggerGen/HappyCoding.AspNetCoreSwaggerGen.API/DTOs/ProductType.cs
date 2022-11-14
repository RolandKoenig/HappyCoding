using System.ComponentModel;
using System.Runtime.Serialization;

namespace HappyCoding.AspNetCoreSwaggerGen.API.DTOs;

/// <summary>
/// Dummy Enum
/// </summary>
public enum ProductType
{
    /// <summary>
    /// Hardware
    /// </summary>
    [EnumMember(Value = "HardwareV2")]
    Hardware = 0,
    
    /// <summary>
    /// Software
    /// </summary>
    [Description("Software")]
    Software = 1,
    
    /// <summary>
    /// HardAndSoftware
    /// </summary>
    [Description("HardAndSoftware")]
    HardAndSoftware = 2
}