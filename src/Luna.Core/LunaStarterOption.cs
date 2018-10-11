using Luna.Dependency;

namespace Luna
{
    public class LunaStarterOption
    {
        public IocManager IocManager { get; }

        public LunaStarterOption()
        {
            IocManager = IocManager.Instance;
        }
    }
}
