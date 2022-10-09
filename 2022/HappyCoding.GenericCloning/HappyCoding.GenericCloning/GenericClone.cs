using System.Collections;
using System.Reflection;

namespace HappyCoding.GenericCloning;

public static class GenericClone
{
    /// <summary>
    /// Clones an object by doing a full deep copy of every field and property.
    /// </summary>
    /// <param name="source">Object to clone</param>
    /// <returns>Cloned copy</returns>
    public static T CreateDeepClone<T>(T source)
    {
        var result = CloneObjectInternal(source, source, new Dictionary<object, object>());
        if (result == null)
        {
            throw new NotSupportedException("Unable to clone the given object!");
        }
        return (T)result;
    }

    /// <summary>
    /// Internal method to clone an object
    /// </summary>
    private static object? CloneObjectInternal(object? entity, object? initiator, Dictionary<object, object> refValues)
    {
        // Null? No work
        if (entity == null)
        {
            return null;
        }

        var entityType = entity.GetType();
        
        // Handle primitive types (int, float, double, etc.)
        if (entityType.IsPrimitive)
        {
            return entity;
        }

        // Clone strings (special case)
        if (entityType == typeof(string))
        {
            return entity;
        }

        // See if we've seen this object already.  If so, return the clone.
        if (refValues.ContainsKey(entity))
        {
            return refValues[entity];
        }

        // Clone weak references
        if (entity is WeakReference weakReference)
        {
            if (weakReference.IsAlive)
            {
                var clone = new WeakReference(weakReference.Target);
                refValues[entity] = clone;
                return clone;
            }
            else
            {
                var clone = new WeakReference(new object());
                refValues[entity] = clone;
                return clone;
            }
        }

        // Use the ICloneable implementation of the object if possible
        if (entity is ICloneable cloneableInterface)
        {
            var clone = cloneableInterface.Clone();
            refValues[entity] = clone;
            return clone;
        }

        // If the element is an array, then copy it.
        if (entityType.IsArray)
        {
            var copy = (Array) ((Array) entity).Clone();
            if (copy.Rank > 1)
            {
                for (var rank = 0; rank < copy.Rank; rank++)
                {
                    for (var loop = copy.GetLowerBound(rank); loop <= copy.GetUpperBound(rank); loop++)
                    {
                        copy.SetValue(CloneObjectInternal(copy.GetValue(rank, loop), initiator, refValues), rank, loop);
                    }
                }
            }
            else
            {
                for (var loop = copy.GetLowerBound(0); loop <= copy.GetUpperBound(0); loop++)
                {
                    var value = copy.GetValue(loop);
                    copy.SetValue(CloneObjectInternal(value, initiator, refValues), loop);
                }
            }
            refValues[entity] = copy;
            return copy;
        }

        // Dictionary type
        if (entity is IDictionary dictionary)
        {
            var clone = (IDictionary) Activator.CreateInstance(entityType);
            foreach (var key in dictionary.Keys)
            {
                var keyCopy = CloneObjectInternal(key, initiator, refValues);
                var valCopy = CloneObjectInternal(dictionary[key], initiator, refValues);
                clone.Add(keyCopy!, valCopy);
            }
            refValues[dictionary] = clone;
            return clone;
        }

        // IList type
        if (entity is IList list)
        {
            var clone = (IList) Activator.CreateInstance(entityType);
            foreach (var value in list)
            {
                var valCopy = CloneObjectInternal(value, initiator, refValues);
                clone.Add(valCopy);
            }
            refValues[list] = clone;
            return clone;
        }

        // No obvious way to copy the object - do a field-by-field copy
        var result = CreateInstance(entity, entityType);

        // Save off the reference
        refValues[entity] = result;

        // Walk through all the fields - this will capture auto-properties as well.
        var actType = entityType;
        var alreadyScanned = new Dictionary<FieldInfo, object?>();
        while (actType != null)
        {
            var fields = actType.GetFields(
                BindingFlags.Instance | BindingFlags.FlattenHierarchy |
                BindingFlags.NonPublic | BindingFlags.Public);
            foreach (var actField in fields)
            {
                // Handle current field
                if (!alreadyScanned.ContainsKey(actField))
                {
                    actField.SetValue(result, CloneObjectInternal(actField.GetValue(entity), initiator, refValues));
                    alreadyScanned.Add(actField, null);
                }
            }
            actType = actType.BaseType;
        }

        return result;
    }

    /// <summary>
    /// Creates a new instances from the given type.
    /// </summary>
    /// <param name="entity">The entity to be cloned.</param>
    /// <param name="entityType">The type of the entity.</param>
    private static object? CreateInstance(object entity, Type entityType)
    {
        // Support for Clone method on records (they may have no parameterless constructor)
        var cloneMethod = entityType.GetMethod("<Clone>$", BindingFlags.Instance | BindingFlags.Public);
        if (cloneMethod != null)
        {
            return cloneMethod.Invoke(entity, null);
        }
        
        // Last method.. Create an instance using the parameterless constructor of the entityType
        return Activator.CreateInstance(entityType);
    }
}
