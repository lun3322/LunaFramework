using Luna.Dependency;

namespace Luna
{
    public class StarterOption
    {
        public IocManager IocManager { get; }

        public StarterOption()
        {
            IocManager = IocManager.Instance;
        }
    }
}
