﻿using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace EventBus.Abstractions;
public class EventBusSubscriptionInfo {
    public Dictionary<String, Type> EventTypes { get; } = [];

    public JsonSerializerOptions JsonSerializerOptions { get; } = new(DefaultSerializerOptions);

    internal static readonly JsonSerializerOptions DefaultSerializerOptions = new() {
        TypeInfoResolver = JsonSerializer.IsReflectionEnabledByDefault ? CreateDefaultTypeResolver() : JsonTypeInfoResolver.Combine()
    };

    private static IJsonTypeInfoResolver CreateDefaultTypeResolver() {
        return new DefaultJsonTypeInfoResolver();
    }
}