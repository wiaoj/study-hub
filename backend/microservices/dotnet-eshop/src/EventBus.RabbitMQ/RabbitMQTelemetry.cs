﻿using OpenTelemetry.Context.Propagation;
using System.Diagnostics;

namespace EventBus.RabbitMQ;
public class RabbitMQTelemetry {
    public static String ActivitySourceName = "EventBusRabbitMQ";
    public ActivitySource ActivitySource { get; } = new(ActivitySourceName);
    public TextMapPropagator Propagator { get; } = Propagators.DefaultTextMapPropagator;
}