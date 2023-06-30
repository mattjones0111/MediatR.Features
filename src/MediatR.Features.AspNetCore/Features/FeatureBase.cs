using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR.Features.Abstractions;
using MediatR.Features.Abstractions.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Utilities;

namespace MediatR.Features.AspNetCore.Features;

public abstract class FeatureBase
{
    public Type RequestType { get; }
    public string Method { get; }
    public string RoutePattern { get; }

    protected FeatureBase()
    {
    }

    protected FeatureBase(
        Type requestType,
        string method,
        string routePattern)
    {
        Ensure.IsNotNull(requestType, nameof(requestType));
        Ensure.IsNotNullOrEmpty(method, nameof(method));

        RequestType = requestType;
        Method = method;
        RoutePattern = routePattern;
    }

    public static FeatureBase CreateFromType(Type requestType)
    {
        Ensure.IsNotNull(requestType, nameof(requestType));

        if (!IsFeature(requestType))
        {
            return new NullFeature();
        }

        HttpEndpointAttribute attribute =
            requestType.GetCustomAttribute<HttpEndpointAttribute>();

        if (attribute == null)
        {
            // this should never happen because the existence of the attribute
            // is tested in IsFeature(), but here we are.
            return new NullFeature();
        }

        if (IsCommand(requestType))
        {
            return new CommandFeature(
                requestType,
                attribute.Method,
                attribute.RoutePattern);
        }

        if (IsQuery(requestType))
        {
            return new QueryFeature(
                requestType,
                attribute.RoutePattern);
        }

        return new NullFeature();
    }

    static bool IsQuery(Type type)
    {
        Type baseType = type.BaseType;

        while (baseType != null)
        {
            if (baseType.IsGenericType &&
                baseType.GetGenericTypeDefinition() == typeof(QueryBase<>))
            {
                return true;
            }

            baseType = baseType.BaseType;
        }

        return false;
    }

    static bool IsCommand(Type type)
    {
        Type baseType = type.BaseType;

        while (baseType != null)
        {
            if (baseType == typeof(CommandBase))
            {
                return true;
            }

            baseType = baseType.BaseType;
        }

        return false;
    }

    public abstract void Map(IEndpointRouteBuilder builder);

    public abstract Task Execute(HttpContext context);

    public static bool IsFeature(Type candidateType)
    {
        Ensure.IsNotNull(candidateType, nameof(candidateType));

        if (candidateType.IsAbstract || !candidateType.IsClass)
        {
            return false;
        }

        if (!candidateType
                .GetCustomAttributesData()
                .Any(x => x.AttributeType == typeof(HttpEndpointAttribute)))
        {
            return false;
        }

        return IsCommand(candidateType) || IsQuery(candidateType);
    }

    protected static void BindRouteParameters(
        RouteValueDictionary routeValues,
        object queryObject)
    {
        PropertyInfo[] properties = queryObject
            .GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var routeValue in routeValues)
        {
            PropertyInfo theProperty = properties
                .FirstOrDefault(x => x.Name.Equals(
                    routeValue.Key,
                    StringComparison.OrdinalIgnoreCase));

            if (theProperty == null)
            {
                continue;
            }

            if (theProperty.PropertyType == typeof(int))
            {
                bool parsed = int.TryParse(
                    (string)routeValue.Value,
                    out int value);

                if (parsed)
                {
                    theProperty.SetValue(queryObject, value);
                }
            }

            if (theProperty.PropertyType == typeof(string))
            {
                theProperty.SetValue(queryObject, routeValue.Value);
            }
        }
    }

    protected static void BindQueryParameters(IQueryCollection queryValues, object queryObject)
    {
        PropertyInfo[] properties = queryObject
            .GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var queryParam in queryValues)
        {
            PropertyInfo theProperty = properties
                .FirstOrDefault(x => x.Name.Equals(
                    queryParam.Key,
                    StringComparison.OrdinalIgnoreCase));

            if (theProperty == null)
            {
                continue;
            }

            if (theProperty.PropertyType == typeof(int))
            {
                if (!int.TryParse(queryParam.Value[0], out int value))
                {
                    continue;
                }

                theProperty.SetValue(queryObject, value);
            }

            if (theProperty.PropertyType == typeof(string))
            {
                theProperty.SetValue(queryObject, queryParam.Value);
            }
        }
    }
}
