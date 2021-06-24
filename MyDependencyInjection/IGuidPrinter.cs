namespace MyDependencyInjection
{
    public interface IGuidPrinter
    {
        public decimal Version { get; }
        void Print();
    }
}