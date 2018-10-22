using System.ComponentModel;
using Test;

[assembly: InitializationEvent(typeof(AssemblyInit), nameof(AssemblyInit.Init))]

namespace Test
{
    public static class AssemblyInit
    {
        public static void Init()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
        }
    }
}
