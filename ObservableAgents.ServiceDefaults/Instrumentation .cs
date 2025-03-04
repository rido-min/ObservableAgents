using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace ObservableAgents.ServiceDefaults
{
    public class Instrumentation : IDisposable
    {
        public const string ActivitySourceName = "bot-traffic";
        public const string ActivitySourceVersion = "1.0.0";

        internal const string MeterName = "OTelBot";
        public readonly Meter Meter;

        public Instrumentation()
        {
            ActivitySource = new ActivitySource(ActivitySourceName, ActivitySourceVersion);
            Meter = new Meter(MeterName, ActivitySourceVersion);
        }

        public ActivitySource ActivitySource { get; }

        public void Dispose()
        {
            ActivitySource.Dispose();
        }
    }
}
