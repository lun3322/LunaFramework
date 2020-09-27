using System;
using System.Linq;
using FluentAssertions;
using Luna.Web.Mvc.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Luna.Web.Tests
{
    public class LunaServiceCollectionExtensions
    {
        private IServiceProvider _provider;
        private IWebHost _webHost;
        private IWebHostBuilder _webHostBuilder;

        public LunaServiceCollectionExtensions()
        {
            var builder = new WebHostBuilder();
            builder.ConfigureServices(service => { service.AddLuna<LunaWebTestsModule>(); });
            _webHostBuilder = builder.Configure(app => { });
            _webHost = builder.Build();
            _provider = _webHost.Services;
        }

        [Fact]
        public void AddLunaShouldContainLunaModelVerificationFilter()
        {
            var filter = _provider.GetService<ModelVerificationFilter>();

            filter.Should().NotBeNull();
        }

        [Fact]
        public void AddLunaShouldContainLunaExceptionFilter()
        {
            var filter = _provider.GetService<LunaExceptionFilter>();

            filter.Should().NotBeNull();
        }
    }
}