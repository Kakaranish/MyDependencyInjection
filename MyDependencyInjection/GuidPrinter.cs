using System;

namespace MyDependencyInjection
{
    public class GuidPrinter : IGuidPrinter
    {
        private readonly IGuidProvider _guidProvider;
        private readonly double _version;

        public GuidPrinter(IGuidProvider guidProvider)
        {
            _version = new Random().NextDouble();
            _guidProvider = guidProvider ?? throw new ArgumentNullException(nameof(guidProvider));
        }

        public decimal Version => (decimal) _version;

        public void Print()
        {
            Console.WriteLine($"Printer v{_version}: {_guidProvider.GetGuid()}");
        }
    }
}
