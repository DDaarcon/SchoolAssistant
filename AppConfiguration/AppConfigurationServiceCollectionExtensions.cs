﻿using AppConfigurationEFCore.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections;

namespace AppConfigurationEFCore.Setup
{
    public static class AppConfigurationServiceCollectionExtensions
    {
        /// <summary>
        /// Register <see cref="AppConfiguration{TDbContext, TRecords}"/> factory.
        /// </summary>
        /// <remarks>
        /// 
        /// <typeparamref name="TRecords"/> type must have properties of type <see cref="RecordHandler{T}"/> (for records that come from reference types)
        /// <br />
        /// or <see cref="PrimitiveRecordHandler{T}"/> (for records that come from primitive types, like <c>int</c>, <c>decimal</c>).
        /// <br />
        /// Each property must have attribute <see cref="RecordKeyAttribute"/> with key of configuration record.
        /// <br />
        /// Example:
        /// <code>
        /// public class AppConfigRecords 
        /// {
        ///     [RecordKey("name")]
        ///     public RecordHandler&lt;string&gt; ApplicationName { get; private set; } = null!;
        ///     [RecordKey("maxItemsPerPage")]
        ///     public PrimitiveRecordHandler&lt;int&gt; MaxItemsPerPage { get; private set; } = null!;
        /// }
        /// </code>
        /// <br />
        /// <b>IMPORTANT</b>
        /// <br />
        /// By default only handlers for types <c>string</c>, <c>int</c> and <c>decimal</c> are registered. To register your own use <paramref name="customRecordTypesAction"/>.
        /// </remarks>
        /// <typeparam name="TDbContext"> EF Core DbContext used in your application </typeparam>
        /// <typeparam name="TRecords">
        /// Type with defined records that you'd like to have in your AppConfiguration table. See remarks for an example.
        /// </typeparam>
        /// <param name="customRecordTypesAction">
        /// Use this action to configure your own type handler. Method <c>Add</c> registers reference type converter, <c>AddPrimitive</c> registers primitive type converter.
        /// </param>
        public static IServiceCollection AddAppConfiguration<TDbContext, TRecords>(this IServiceCollection services, Action<CustomRecordTypeOptions>? customRecordTypesAction = null)
            where TDbContext : DbContext
            where TRecords : class, new()
        {
            return AddAppConfiguration(services, typeof(TDbContext), typeof(TRecords), customRecordTypesAction);
        }

        /// <summary>
        /// Register <see cref="AppConfiguration{TDbContext, TRecords}"/> factory.
        /// </summary>
        /// <remarks>
        /// 
        /// <c>TRecords</c> type must have properties of type <see cref="RecordHandler{T}"/> (for records that come from reference types)
        /// <br />
        /// or <see cref="PrimitiveRecordHandler{T}"/> (for records that come from primitive types, like <c>int</c>, <c>decimal</c>).
        /// <br />
        /// Each property must have attribute <see cref="RecordKeyAttribute"/> with key of configuration record.
        /// <br />
        /// Example:
        /// <code>
        /// public class AppConfigRecords 
        /// {
        ///     [RecordKey("name")]
        ///     public RecordHandler&lt;string&gt; ApplicationName { get; private set; } = null!;
        ///     [RecordKey("maxItemsPerPage")]
        ///     public PrimitiveRecordHandler&lt;int&gt; MaxItemsPerPage { get; private set; } = null!;
        /// }
        /// </code>
        /// <br />
        /// <b>IMPORTANT</b>
        /// <br />
        /// By default only handlers for types <c>string</c>, <c>int</c> and <c>decimal</c> are registered. To register your own use <paramref name="customRecordTypesAction"/>.
        /// </remarks>
        /// <param name="dbContextType"> EF Core DbContext used in your application </param>
        /// <param name="configurationRecordsType">
        /// Type with defined records that you'd like to have in your AppConfiguration table. See remarks for an example.
        /// </param>
        /// <param name="customRecordTypesAction">
        /// Use this action to configure your own type handler. Method <c>Add</c> registers reference type converter, <c>AddPrimitive</c> registers primitive type converter.
        /// </param>
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, Type dbContextType, Type configurationRecordsType, Action<CustomRecordTypeOptions>? customRecordTypesAction = null)
        {
            Type factoryType = typeof(Factory<,>).MakeGenericType(dbContextType, configurationRecordsType);
            services.TryAddSingleton(factoryType);




            return services;
        }
    }

    public class CustomRecordTypeOptions
    {
        /// <summary>
        /// Register reference type handler.
        /// </summary>
        /// <typeparam name="T">Reference type</typeparam>
        /// <param name="toTypeConverter">Function converting <c>string</c> value to <typeparamref name="T"/>.</param>
        /// <param name="fromTypeConverter">Function converting <typeparamref name="T"/> to <c>string</c>. By default <c>ToString()</c> method will be used.</param>
        public void Add<T>(Func<string?, T?> toTypeConverter, Func<T?, string?>? fromTypeConverter = null)
        {
            var type = typeof(T);
            _info.Add(new HandlerInfo<T>(type, toTypeConverter, fromTypeConverter ?? (x => x?.ToString())));
        }
        /// <summary>
        /// Register primitive type handler.
        /// </summary>
        /// <typeparam name="T">Primitive type</typeparam>
        /// <param name="toTypeConverter">Function converting <c>string</c> value to <typeparamref name="T"/>.</param>
        /// <param name="fromTypeConverter">Function converting <typeparamref name="T"/> to <c>string</c>. By default <c>ToString()</c> method will be used.</param>
        public void AddPrimitive<T>(Func<string?, T?> toTypeConverter, Func<T?, string?>? fromTypeConverter = null)
            where T : struct
        {
            var type = typeof(T);
            _primitiveInfo.Add(new PrimitiveHandlerInfo<T>(type, toTypeConverter, fromTypeConverter ?? (x => x?.ToString())));
        }

        private readonly ArrayList _info = new ArrayList();
        private record HandlerInfo<T>(
            Type ForType,
            Func<string?, T?> ToTypeConverter,
            Func<T?, string?> FromTypeConverter);

        private readonly ArrayList _primitiveInfo = new ArrayList();
        private record PrimitiveHandlerInfo<T>(
            Type ForType,
            Func<string?, T?> ToTypeConverter,
            Func<T?, string?> FromTypeConverter)
            where T : struct;
    }
}