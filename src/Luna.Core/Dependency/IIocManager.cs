using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Castle.Windsor;

namespace Luna.Dependency
{
    public interface IIocManager : IDisposable
    {
        /// <summary>
        /// Ioc容器
        /// </summary>
        IWindsorContainer IocContainer { get; }

        /// <summary>
        /// 注册一个 Assembly
        /// </summary>
        /// <param name="assembly"></param>
        void RegisterAssemblyByConvention(Assembly assembly);

        /// <summary>
        /// 获取容器中的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>();
    }
}
