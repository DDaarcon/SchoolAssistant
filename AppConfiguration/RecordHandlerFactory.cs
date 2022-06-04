using AppConfigurationEFCore.Configuration;
using Microsoft.EntityFrameworkCore;

namespace AppConfigurationEFCore.Setup
{
    internal interface IRecordHandlerFactory
    {
        RecordHandler<T>? Get<T>(string key, Func<DbContext> getContext);
        PrimitiveRecordHandler<T>? GetPrimitive<T>(string key, Func<DbContext> getContext) where T : struct;
    }
    internal class RecordHandlerFactory : IRecordHandlerFactory
    {
        private readonly object[] _handlersInfo;
        private readonly object[] _primitiveHandlersInfo;

        private Type _type = null!;
        private string _key = null!;
        private Func<DbContext> _getContext = null!;

        public RecordHandlerFactory(
            object[] info,
            object[] primitiveInfo)
        {
            _handlersInfo = info;
            _primitiveHandlersInfo = primitiveInfo;
        }

        public RecordHandler<T>? Get<T>(string key, Func<DbContext> getContext)
        {
            if (!AssignHandlerParamsAndValidateNulls(typeof(T), key, getContext))
                return null;

            if (TryGetDefaultHandler(out var handlerObj2))
                return handlerObj2 as RecordHandler<T>;

            if (TryGetUserDefinedHandler<T>(out var handler))
                return handler;

            return null;
        }

        public PrimitiveRecordHandler<T>? GetPrimitive<T>(string key, Func<DbContext> getContext)
            where T : struct
        {
            if (!AssignHandlerParamsAndValidateNulls(typeof(T), key, getContext))
                return null;

            if (TryGetDefaultPrimitiveHandler(out var handlerObj2))
                return handlerObj2 as PrimitiveRecordHandler<T>;

            if (TryGetUserDefinedPrimitiveHandler<T>(out var handler))
                return handler;

            return null;
        }

        private bool AssignHandlerParamsAndValidateNulls(Type type, string key, Func<DbContext> getContext)
        {
            _type = type; _key = key; _getContext = getContext;
            if (_key is null) return false;
            if (_getContext is null) return false;
            return true;
        }

        private bool TryGetDefaultHandler(out object? handlerObj)
        {
            handlerObj = null;
            if (_type is null) return false;

            if (typeof(string).IsEquivalentTo(_type))
            {
                handlerObj = new RecordHandler<string>(
                    _key, _getContext,
                    to => to, from => from);
                return true;
            }

            return false;
        }

        private bool TryGetUserDefinedHandler<T>(out RecordHandler<T>? handlerObj)
        {
            handlerObj = null;
            var info = _handlersInfo.FirstOrDefault(x => x is HandlerInfo<T> h && h.ForType == _type) as HandlerInfo<T>;

            if (info is null) return false;

            handlerObj = new RecordHandler<T>(
                _key, _getContext,
                info.ToTypeConverter, info.FromTypeConverter);

            return true;
        }

        private bool TryGetDefaultPrimitiveHandler(out object? handlerObj)
        {
            handlerObj = null;
            if (_type is null) return false;

            if (typeof(int).IsEquivalentTo(_type))
            {
                handlerObj = new PrimitiveRecordHandler<int>(
                    _key, _getContext,
                    to => to is null ? null : int.Parse(to), from => from?.ToString());
                return true;
            }
            if (typeof(decimal).IsEquivalentTo(_type))
            {
                handlerObj = new PrimitiveRecordHandler<decimal>(
                    _key, _getContext,
                    to => to is null ? null : decimal.Parse(to), from => from?.ToString());
                return true;
            }

            return false;
        }

        private bool TryGetUserDefinedPrimitiveHandler<T>(out PrimitiveRecordHandler<T>? handlerObj)
            where T : struct
        {
            handlerObj = null;
            var info = _primitiveHandlersInfo.FirstOrDefault(x => x is PrimitiveHandlerInfo<T> h && h.ForType == _type) as PrimitiveHandlerInfo<T>;

            if (info is null) return false;

            handlerObj = new PrimitiveRecordHandler<T>(
                _key, _getContext,
                info.ToTypeConverter, info.FromTypeConverter);

            return true;
        }

    }
}
