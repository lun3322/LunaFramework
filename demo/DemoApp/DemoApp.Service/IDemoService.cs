using System;
using System.Collections.Generic;
using System.Text;
using Luna.Application;

namespace DemoApp.Service
{
    public interface IDemoService : ILunaService
    {
        string GetMessage();
    }
}
