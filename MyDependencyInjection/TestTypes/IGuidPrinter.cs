namespace MyDependencyInjection.TestTypes
{
    public interface IGuidPrinter
    {
        public decimal Version { get; }
        void Print();
    }
}