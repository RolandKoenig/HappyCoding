using System;

namespace HappyCoding.TemperatureViewer.Model;

public record TemperatureMeasurement(DateTimeOffset TimeStamp, double TemperatureInDegrees);