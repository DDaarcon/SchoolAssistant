using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class InjectableAttribute : Attribute
    {
        public ServiceLifetime Lifetime { get; }
        public Type[]? Contracts { get; }

        public InjectableAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            Lifetime = lifetime;
        }

        public InjectableAttribute(params Type[] contracts)
        {
            Lifetime = ServiceLifetime.Scoped;
            Contracts = contracts;
        }

        public InjectableAttribute(ServiceLifetime lifetime, params Type[] contracts)
        {
            Lifetime = ServiceLifetime.Scoped;
            Contracts = contracts;
        }
    }
}
