﻿using System;
using System.Collections.Generic;
using System.Text;
using Castle.Core.Logging;
using Luna.Dependency;
using Microsoft.AspNetCore.Mvc;

namespace Luna.Web.Mvc
{
    public abstract class LunaController : Controller, ITransientDependency
    {
        public ILogger Logger { get; set; }
    }
}