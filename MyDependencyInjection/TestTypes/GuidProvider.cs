using System;

namespace MyDependencyInjection.TestTypes
{
    public class GuidProvider : IGuidProvider
    {
        private readonly Guid _guid;

        public GuidProvider()
        {
            _guid = Guid.NewGuid();
        }

        public Guid GetGuid()
        {
            return _guid;
        }
    }
}
