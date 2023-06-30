using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities;

public static class Ensure
{
    public static void IsNotNull(object value, string paramName)
    {
        if (value == null)
        {
            throw new ArgumentNullException(paramName);
        }
    }

    public static void IsNotNullOrEmpty(string value, string paramName)
    {
        IsNotNull(value, paramName);

        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException(
                $"{nameof(paramName)} cannot be an empty string.");
        }
    }

    public static void IsOneOf<T>(
        T value,
        IEnumerable<T> allowedItems,
        string paramName,
        IEqualityComparer<T> comparer = null)
    {
        IsNotNull(allowedItems, nameof(allowedItems));

        comparer ??= EqualityComparer<T>.Default;

        if (!allowedItems.Contains(value, comparer))
        {
            throw new ArgumentException(
                $"{nameof(value)} is not one of the allowed values.",
                paramName);
        }
    }
}
