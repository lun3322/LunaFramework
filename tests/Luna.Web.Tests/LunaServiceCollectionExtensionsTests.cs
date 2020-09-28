using System;
using FluentAssertions;
using Luna.Web.Mvc.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Luna.Web.Tests
{
    public class LunaServiceCollectionExtensions
    {
        private readonly IServiceProvider _provider;
        private readonly IWebHostBuilder _webHostBuilder;

        public LunaServiceCollectionExtensions()
        {
            var builder = new WebHostBuilder();
            builder.ConfigureServices(service => { service.AddLuna<LunaWebTestsModule>(); });
            _webHostBuilder = builder.Configure(app => { });
            var webHost = builder.Build();
            _provider = webHost.Services;
        }

        [Fact]
        public void AddLunaShouldContainLunaModelVerificationFilter()
        {
            var filter = _provider.GetService<ModelVerificationFilterAttribute>();

            filter.Should().NotBeNull();
        }

        [Fact]
        public void AddLunaShouldContainLunaExceptionFilter()
        {
            var filter = _provider.GetService<LunaExceptionFilterAttribute>();

            filter.Should().NotBeNull();
        }
    }
}