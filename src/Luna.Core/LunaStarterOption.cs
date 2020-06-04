using Luna.Dependency;

namespace Luna
{
    public class LunaStarterOption
    {
        public IocManager IocManager { get; set; }
        public bool UseDefaultLunaFilter { get; set; }

        public LunaStarterOption()
        {
            IocManager = IocManager.Instance;
        }
    }
}