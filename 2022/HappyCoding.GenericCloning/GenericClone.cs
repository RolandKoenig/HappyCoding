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
    public static T? CloneObject<T>(T? source)
    {
        return (T?)CloneObjectInternal(source, source, new Dictionary<object, object>());
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

        // Special case value-types; they are passed by value so we have our
        // own copy right here.
        if (entityType.IsValueType)
        {
            return entity;
        }

        // Clone strings (special case)
        if (entityType == typeof(string))
        {
            return ((string) entity).Clone();
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
                object clone = new WeakReference(weakReference.Target);
                refValues[entity] = clone;
                return clone;
            }
            else
            {
                object clone = new WeakReference(new object());
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
        var result = Activator.CreateInstance(entityType);

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
                // Check if this field is to be ignored during clone process
                if (actField.GetCustomAttribute<IgnoreGenericCloneAttribute>() != null)
                {
                    continue;
                }

                //Handling logic for fields which should get a reference to the original object
                if (actField.GetCustomAttribute<AssignOriginalParentObjectAfterCloneAttribute>() != null)
                {
                    try
                    {
                        actField.SetValue(result, entity);
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException(
                            "Unable to set original object to field " + actField.Name + ": " + ex.Message, ex);
                    }
                    continue;
                }

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
}
