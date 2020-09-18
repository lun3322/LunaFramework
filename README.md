# LunaFramework [![Build Status](https://travis-ci.com/lun3322/LunaFramework.svg?branch=master)](https://travis-ci.com/lun3322/LunaFramework)

## myget源
https://www.myget.org/F/luna/api/v3/index.json
```
Install-Package Luna
Install-Package Luna.Castle.Nlog
Install-Package Luna.Web
```

## 关于使用
1. 如果你也使用nlog写日志的话可以直接引用 Luna.Service.Nlog 包.关于日志的一个配置会自动加载到项目中
2. 在Main方法中新增代码
    ```
	using (var starter = LunaStarter.Create<Program>())
	{
		starter.IocManager.IocContainer.AddFacility<LoggingFacility>(m =>
			m.LogUsing<NLogFactory>().WithConfig("NLog.config"));

		starter.Initialize();
	}
    ```
1. 增加你的service像下面这样
    ```
    public interface IDemoService : ILunaService
    {
    	string GetMessage();
    }
    
    public class DemoService : LunaService, IDemoService
    {
    	public string GetMessage()
    	{
    		Logger.Info("GetMessage");
    		return "测试";
    	}
    }
    ```
    注意接口实现ILunaService才能被自动注册进IOC

如果不喜欢用nlog的话,可以查看Castle.Windsor文档修改第3步中AddFacility方法

## 关于web包使用
修改Startup代码
```
public IServiceProvider ConfigureServices(IServiceCollection services)
{
	...
	return services.AddLuna<Startup>();
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
	...
	// 在UseMvc之前调用
	app.UseLuna();
	...
}
```

**项目约定: 你的程序命名必须遵循aaa.bb.c的方式**
```
 Demo.App        <- 应用程序入口
 Demo.Service    <- 服务层
 Demo.Entity     <- 实体层
```