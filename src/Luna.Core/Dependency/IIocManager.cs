using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Reflection;

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

        /// <summary>
        /// 获取所有实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T[] ResolveAll<T>();

        /// <summary>
        /// 判断是否已注册进容器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool IsRegistered<T>();

        void RegisterTypeTransient(Type serviceType, Type implementedType);
        void RegisterTypeSingleton(Type serviceType, Type implementedType);

        HashSet<Assembly> AllAssembly { get; }
        HashSet<Type> AllTypes { get; }
    }
}