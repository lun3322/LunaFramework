using Luna.Application;

namespace DemoApp.Service
{
    public interface IDemoService : ILunaService
    {
        string GetMessage();
    }
}