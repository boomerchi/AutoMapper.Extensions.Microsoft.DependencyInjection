﻿using Microsoft.Extensions.DependencyInjection;

namespace AutoMapper.Extensions.Microsoft.DependencyInjection.Tests
{
    using System;
    using Shouldly;
    using Xunit;

    public class TypeResolutionTests
    {
        private readonly IServiceProvider _provider;

        public TypeResolutionTests()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddAutoMapper(typeof(Source));
            _provider = services.BuildServiceProvider();
        }

        [Fact]
        public void ShouldResolveConfiguration()
        {
            _provider.GetService<IConfigurationProvider>().ShouldNotBeNull();
        }

        [Fact]
        public void ShouldConfigureProfiles()
        {
            _provider.GetService<IConfigurationProvider>().GetAllTypeMaps().Length.ShouldBe(2);
        }

        [Fact]
        public void ShouldResolveMapper()
        {
            _provider.GetService<IMapper>().ShouldNotBeNull();
        }

        [Fact]
        public void ShouldResolveMappingAction()
        {
            _provider.GetService<FooMappingAction>().ShouldNotBeNull();
        }

        [Fact]
        public void ShouldResolveValueResolver()
        {
            _provider.GetService<FooValueResolver>().ShouldNotBeNull();
        }

        [Fact]
        public void ShouldResolveMemberValueResolver()
        {
            _provider.GetService<FooMemberValueResolver>().ShouldNotBeNull();
        }

        [Fact]
        public void ShouldResolveTypeConverter()
        {
            _provider.GetService<FooTypeConverter>().ShouldNotBeNull();
        }
    }

    public class TypeResolutionTests_ForStaticConfig
    {
        private readonly IServiceProvider _provider;

        public TypeResolutionTests_ForStaticConfig()
        {
            ServiceCollectionExtensions.UseStaticRegistration = true;
            IServiceCollection services = new ServiceCollection();
            services.AddAutoMapper(typeof(Source));
            _provider = services.BuildServiceProvider();
        }

        [Fact]
        public void ShouldInitializeStatically()
        {
            _provider.GetService<IConfigurationProvider>().ShouldBeSameAs(Mapper.Configuration);
        }
    }
}