using Luna.Dependency;

namespace Luna
{
    public abstract class LunaTestBase<T>
            where T : class
    {
        public LunaStarter LunaStarter { get; private set; }
        public IIocManager IocManager { get; set; }

        protected LunaTestBase()
        {
            LunaStarter = LunaStarter.Create<T>(opt => opt.IocManager = new IocManager());
            LunaStarter.Initialize();
            IocManager = LunaStarter.IocManager;
        }
    }
}
