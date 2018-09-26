using Luna.Dependency;

namespace Luna
{
    public interface IRunner : ISingletonDependency
    {
        void Run();
        void Stop();
        void Init();
    }
}
