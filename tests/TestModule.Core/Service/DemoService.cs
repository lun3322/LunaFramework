using Luna.Application;

namespace TestModule.Core.Service
{
    public class DemoService : IDemoService, ILunaService
    {
        public int GetCode()
        {
            return 0;
        }
    }
}