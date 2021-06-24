using System;

namespace MyDependencyInjection
{
    public class GuidPrinter : IGuidPrinter
    {
        private readonly IGuidProvider _guidProvider;

        public GuidPrinter(IGuidProvider guidProvider)
        {
            _guidProvider = guidProvider ?? throw new ArgumentNullException(nameof(guidProvider));
        }

        public void Print()
        {
            Console.WriteLine(_guidProvider.GetGuid());
        }
    }
}
