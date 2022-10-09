using HappyCoding.GenericCloning.Tests.Data;

namespace HappyCoding.GenericCloning.Tests;

public class GenericCloneTests
{
    [Fact]
    public void Clone_FlatDataClass()
    {
        // Arrange
        var dataClass = new FlatDataClass()
        {
            Property1 = "123",
            Property2 = "456",
            Property3 = 3523,
            Property4 = 23.4f,
            Property5 = null,
            Property6 = 23
        };
        
        // Act
        var clonedDataClass = GenericClone.CreateDeepClone(dataClass);
        
        // Assert
        Assert.False(ReferenceEquals(dataClass, clonedDataClass));
        Assert.Equal(dataClass.Property1, clonedDataClass.Property1);
        Assert.Equal(dataClass.Property2, clonedDataClass.Property2);
        Assert.Equal(dataClass.Property3, clonedDataClass.Property3);
        Assert.Equal(dataClass.Property4, clonedDataClass.Property4);
        Assert.Equal(dataClass.Property5, clonedDataClass.Property5);
        Assert.Equal(dataClass.Property6, clonedDataClass.Property6);
    }
    
    [Fact]
    public void Clone_FlatDataStruct()
    {
        // Arrange
        var dataClass = new FlatDataStruct()
        {
            Property1 = "123",
            Property2 = "456",
            Property3 = 3523,
            Property4 = 23.4f,
            Property5 = null,
            Property6 = 23
        };
        
        // Act
        var clonedDataClass = GenericClone.CreateDeepClone(dataClass);
        
        // Assert
        Assert.Equal(dataClass.Property1, clonedDataClass.Property1);
        Assert.Equal(dataClass.Property2, clonedDataClass.Property2);
        Assert.Equal(dataClass.Property3, clonedDataClass.Property3);
        Assert.Equal(dataClass.Property4, clonedDataClass.Property4);
        Assert.Equal(dataClass.Property5, clonedDataClass.Property5);
        Assert.Equal(dataClass.Property6, clonedDataClass.Property6);
    }
    
    [Fact]
    public void Clone_FlatDataReadOnlyStruct()
    {
        // Arrange
        var dataClass = new FlatDataReadOnlyStruct()
        {
            Property1 = "123",
            Property2 = "456",
            Property3 = 3523,
            Property4 = 23.4f,
            Property5 = null,
            Property6 = 23
        };
        
        // Act
        var clonedDataClass = GenericClone.CreateDeepClone(dataClass);
        
        // Assert
        Assert.Equal(dataClass.Property1, clonedDataClass.Property1);
        Assert.Equal(dataClass.Property2, clonedDataClass.Property2);
        Assert.Equal(dataClass.Property3, clonedDataClass.Property3);
        Assert.Equal(dataClass.Property4, clonedDataClass.Property4);
        Assert.Equal(dataClass.Property5, clonedDataClass.Property5);
        Assert.Equal(dataClass.Property6, clonedDataClass.Property6);
    }
    
    [Fact]
    public void Clone_FlatDataRecord()
    {
        // Arrange
        var dataClass = new FlatDataRecord()
        {
            Property1 = "123",
            Property2 = "456",
            Property3 = 3523,
            Property4 = 23.4f,
            Property5 = null,
            Property6 = 23
        };
        
        // Act
        var clonedDataClass = GenericClone.CreateDeepClone(dataClass);
        
        // Assert
        Assert.False(ReferenceEquals(dataClass, clonedDataClass));
        Assert.Equal(dataClass.Property1, clonedDataClass.Property1);
        Assert.Equal(dataClass.Property2, clonedDataClass.Property2);
        Assert.Equal(dataClass.Property3, clonedDataClass.Property3);
        Assert.Equal(dataClass.Property4, clonedDataClass.Property4);
        Assert.Equal(dataClass.Property5, clonedDataClass.Property5);
        Assert.Equal(dataClass.Property6, clonedDataClass.Property6);
    }
    
    [Fact]
    public void Clone_FlatDataRecordWithInits()
    {
        // Arrange
        var dataClass = new FlatDataRecordWithInits()
        {
            Property1 = "123",
            Property2 = "456",
            Property3 = 3523,
            Property4 = 23.4f,
            Property5 = null,
            Property6 = 23
        };
        
        // Act
        var clonedDataClass = GenericClone.CreateDeepClone(dataClass);
        
        // Assert
        Assert.False(ReferenceEquals(dataClass, clonedDataClass));
        Assert.Equal(dataClass.Property1, clonedDataClass.Property1);
        Assert.Equal(dataClass.Property2, clonedDataClass.Property2);
        Assert.Equal(dataClass.Property3, clonedDataClass.Property3);
        Assert.Equal(dataClass.Property4, clonedDataClass.Property4);
        Assert.Equal(dataClass.Property5, clonedDataClass.Property5);
        Assert.Equal(dataClass.Property6, clonedDataClass.Property6);
    }
    
    [Fact]
    public void Clone_FlatDataRecordWithConstructorArgs()
    {
        // Arrange
        var dataClass = new FlatDataRecordWithConstructorArgs("123", "456");
        
        // Act
        var clonedDataClass = GenericClone.CreateDeepClone(dataClass);
        
        // Assert
        Assert.False(ReferenceEquals(dataClass, clonedDataClass));
        Assert.Equal(dataClass.Property1, clonedDataClass.Property1);
        Assert.Equal(dataClass.Property2, clonedDataClass.Property2);
    }
    
    [Fact]
    public void Clone_FlatDataRecordWithMixedProperties()
    {
        // Arrange
        var dataClass = new FlatDataRecordWithMixedProperties("123", "456")
        {
            Property3 = 3523,
            Property4 = 23.4f,
            Property5 = null,
            Property6 = 23
        };
        
        // Act
        var clonedDataClass = GenericClone.CreateDeepClone(dataClass);
        
        // Assert
        Assert.False(ReferenceEquals(dataClass, clonedDataClass));
        Assert.Equal(dataClass.Property1, clonedDataClass.Property1);
        Assert.Equal(dataClass.Property2, clonedDataClass.Property2);
        Assert.Equal(dataClass.Property3, clonedDataClass.Property3);
        Assert.Equal(dataClass.Property4, clonedDataClass.Property4);
        Assert.Equal(dataClass.Property5, clonedDataClass.Property5);
        Assert.Equal(dataClass.Property6, clonedDataClass.Property6);
    }

    [Fact]
    public void Clone_ComplexDataRecordWithChildClass()
    {
        // Arrange
        var dataClass = new ComplexDataRecordWithChildClass("123", new FlatDataClass()
        {
            Property1 = "123",
            Property2 = "456",
            Property3 = 3523,
            Property4 = 23.4f,
            Property5 = null,
            Property6 = 23
        });
        
        // Act
        var clonedDataClass = GenericClone.CreateDeepClone(dataClass);
        
        // Assert
        Assert.False(ReferenceEquals(dataClass, clonedDataClass));
        Assert.Equal(dataClass.Property1, clonedDataClass.Property1);
        Assert.False(ReferenceEquals(dataClass.ChildObject1, clonedDataClass.ChildObject1));
        Assert.Equal(dataClass.ChildObject1.Property1, clonedDataClass.ChildObject1.Property1);
        Assert.Equal(dataClass.ChildObject1.Property2, clonedDataClass.ChildObject1.Property2);
        Assert.Equal(dataClass.ChildObject1.Property3, clonedDataClass.ChildObject1.Property3);
        Assert.Equal(dataClass.ChildObject1.Property4, clonedDataClass.ChildObject1.Property4);
        Assert.Equal(dataClass.ChildObject1.Property5, clonedDataClass.ChildObject1.Property5);
        Assert.Equal(dataClass.ChildObject1.Property6, clonedDataClass.ChildObject1.Property6);
    }
    
    [Fact]
    public void Clone_ComplexDataRecordWithChildRecord()
    {
        // Arrange
        var dataClass = new ComplexDataRecordWithChildRecord("123", new FlatDataRecord()
        {
            Property1 = "123",
            Property2 = "456",
            Property3 = 3523,
            Property4 = 23.4f,
            Property5 = null,
            Property6 = 23
        });
        
        // Act
        var clonedDataClass = GenericClone.CreateDeepClone(dataClass);
        
        // Assert
        Assert.False(ReferenceEquals(dataClass, clonedDataClass));
        Assert.Equal(dataClass.Property1, clonedDataClass.Property1);
        Assert.False(ReferenceEquals(dataClass.ChildObject1, clonedDataClass.ChildObject1));
        Assert.Equal(dataClass.ChildObject1, clonedDataClass.ChildObject1);
        Assert.Equal(dataClass.ChildObject1.Property1, clonedDataClass.ChildObject1.Property1);
        Assert.Equal(dataClass.ChildObject1.Property2, clonedDataClass.ChildObject1.Property2);
        Assert.Equal(dataClass.ChildObject1.Property3, clonedDataClass.ChildObject1.Property3);
        Assert.Equal(dataClass.ChildObject1.Property4, clonedDataClass.ChildObject1.Property4);
        Assert.Equal(dataClass.ChildObject1.Property5, clonedDataClass.ChildObject1.Property5);
        Assert.Equal(dataClass.ChildObject1.Property6, clonedDataClass.ChildObject1.Property6);
    }
}