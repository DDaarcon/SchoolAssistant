using AppConfigurationEFCore.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AppConfigurationEFCore
{
    internal class Factory<TDbContext, TRecords>
        where TDbContext : DbContext
        where TRecords : class, new()
    {
        private static TDbContext? _context;

        private TRecords? _records;

        public Factory()
        {

        }

        private readonly Func<TDbContext> _getContext = () => _context ?? throw new ArgumentNullException("DbContext has not been provided");

        public AppConfiguration<TDbContext, TRecords> ConstructAppConfiguration(TDbContext context, IServiceScopeFactory scopeFactory)
        {

            return new AppConfiguration<TDbContext, TRecords>(_getContext(), scopeFactory, _records);
        }

        public void SetUpProperties()
        {
            var properties = _records.GetType().GetProperties()!;

            foreach (var property in properties)
            {
                if (IsPropertyValid(property))
                    property.SetValue(_records, CreateOperations(property));
            }
        }

        private object CreateOperations(PropertyInfo property)
        {
            var attr = property.GetCustomAttribute<RecordKeyAttribute>()!;
            var genericType = property.PropertyType.GenericTypeArguments[0];

            if (typeof(int).IsEquivalentTo(genericType))
                return new PrimitiveRecordHandler<int>(
                    attr.Key, _getContext,
                    to => to is null ? null : int.Parse(to), from => from?.ToString());

            if (typeof(decimal).IsEquivalentTo(genericType))
                return new PrimitiveRecordHandler<decimal>(
                    attr.Key, _getContext,
                    to => to is null ? null : decimal.Parse(to), from => from?.ToString());

            if (typeof(string).IsEquivalentTo(genericType))
                return new RecordHandler<string>(
                    attr.Key, _getContext,
                    to => to, from => from);

            throw new ArgumentException($"Storing/fetching configuration of type {genericType.Name} has not been specified.");
        }

        private bool IsPropertyValid(PropertyInfo property)
        {
            return IsAssignableTo(property.PropertyType, typeof(RecordHandler<>))
                && property.GetCustomAttribute<RecordKeyAttribute>() is not null;
        }

        private bool IsAssignableTo(Type givenType, Type genericType)
        {
            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            Type? baseType = givenType.BaseType;
            if (baseType == null) return false;

            return IsAssignableTo(baseType, genericType);
        }

    }
}
