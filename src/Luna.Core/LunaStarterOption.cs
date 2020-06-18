using Luna.Dependency;

namespace Luna
{
    public class LunaStarterOption
    {
        public IocManager IocManager { get; set; }
        public bool EnableLunaFilters { get; set; }

        public LunaStarterOption()
        {
            IocManager = IocManager.Instance;
            EnableLunaFilters = true;
        }
    }
}