using SchoolAssistant.DAL.Attributes;
using SchoolAssistant.DAL.Repositories;
using System.Reflection;

namespace SchoolAssistant.DAL.Help.Application
{
    internal class ConfigRecordOperationsHelper
    {
        private readonly Func<SADbContext> _getContext;
        private readonly AppConfigRepository _repo;

        public ConfigRecordOperationsHelper(
            Func<SADbContext> getContext,
            AppConfigRepository repo)
        {
            _getContext = getContext;
            _repo = repo;
        }

        private object CreateOperations(PropertyInfo property)
        {
            var attr = property.GetCustomAttribute<AppConfigKeyAttribute>()!;
            var genericType = property.PropertyType.GenericTypeArguments[0];

            if (typeof(int).IsEquivalentTo(genericType))
                return new ConfigRecordOperationsPrimitive<int>(
                    attr.Key, _getContext,
                    to => to is null ? null : int.Parse(to), from => from?.ToString());

            if (typeof(decimal).IsEquivalentTo(genericType))
                return new ConfigRecordOperationsPrimitive<decimal>(
                    attr.Key, _getContext,
                    to => to is null ? null : decimal.Parse(to), from => from?.ToString());

            if (typeof(string).IsEquivalentTo(genericType))
                return new ConfigRecordOperations<string>(
                    attr.Key, _getContext,
                    to => to, from => from);

            throw new ArgumentException($"Storing/fetching configuration of type {genericType.Name} has not been specified.");
        }


        public void SetUpProperties()
        {
            var properties = _repo.GetType().GetProperties()!;

            foreach (var property in properties)
            {
                if (IsPropertyValid(property))
                    property.SetValue(_repo, CreateOperations(property));
            }
        }

        private bool IsPropertyValid(PropertyInfo property)
        {
            return IsAssignableTo(property.PropertyType, typeof(ConfigRecordOperations<>))
                && property.GetCustomAttribute<AppConfigKeyAttribute>() is not null;
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
