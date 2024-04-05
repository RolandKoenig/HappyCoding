using System.Diagnostics.Metrics;

public static class Program
{
    public static void Main()
    {
        var counterTask = ProduceMetricsAsync(CancellationToken.None);
        
        var listener = new MeterListener();
        listener.InstrumentPublished += (instrument, meterListener) =>
        {
            if (instrument.Meter.Name is "MyMetrics")
            {
                meterListener.EnableMeasurementEvents(instrument);
            }
        };
        listener.SetMeasurementEventCallback<int>((instrument, measurement, tags, state) =>
        {
            Console.WriteLine($"{instrument.Name} recorded measurement {measurement}");
        });
        listener.Start();
        
        Console.ReadLine();
    }

    private static async Task ProduceMetricsAsync(CancellationToken cancellationToken)
    {
        var meter = new Meter("MyMetrics", "1.0.0");
        var counter1 = meter.CreateCounter<int>(
            name: "counter1",
            unit: "#",
            description: "The number of hats sold in our store");

        var randomizer = new Random(Environment.TickCount);
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(randomizer.Next(500, 1500));
            
            counter1.Add(randomizer.Next(5, 15));
        }
    }
}