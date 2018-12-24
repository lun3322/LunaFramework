namespace Luna.Dependency
{
    public interface ILunaConfiguration : ISingletonDependency
    {
        void Initialize();

        void Setup();
    }
}