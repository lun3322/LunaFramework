# LunaFramework [![Build Status](https://travis-ci.com/lun3322/LunaFramework.svg?branch=master)](https://travis-ci.com/lun3322/LunaFramework)

| 包 | 状态 |
| ------- | ------------ |
| [Luna](https://www.nuget.org/packages/Luna/)  | [![Luna](https://img.shields.io/nuget/v/Luna)](https://www.nuget.org/packages/Luna/) |
| [Luna.Web](https://www.nuget.org/packages/Luna.Web/) | [![Luna.Web](https://img.shields.io/nuget/v/Luna.Web)](https://www.nuget.org/packages/Luna.Web/) |
| [Luna.Dapper](https://www.nuget.org/packages/Luna.Dapper/) | [![Luna](https://img.shields.io/nuget/v/Luna.Dapper)](https://www.nuget.org/packages/Luna.Dapper/) |
| [Luna.MongoDb](https://www.nuget.org/packages/Luna.MongoDb/) | [![Luna](https://img.shields.io/nuget/v/Luna.MongoDb)](https://www.nuget.org/packages/Luna.MongoDb/) |
| [Luna.Redis.AspNetCore](https://www.nuget.org/packages/Luna.Redis.AspNetCore/) | [![Luna](https://img.shields.io/nuget/v/Luna.Redis.AspNetCore)](https://www.nuget.org/packages/Luna.Redis.AspNetCore/) |

## 关于使用
1. 在Main方法中新增代码
    ```
   LunaStarter.StartUp<LunaModule>()
    ```
1. service写法
    ```
    public interface IDemoService
    {
    	string GetMessage();
    }
    
    public class DemoService :IDemoService, ILunaService
    {
    	public string GetMessage()
    	{
    		Logger.Info("GetMessage");
    		return "测试";
    	}
    }
    ```
    注意接口实现ILunaService才能被自动注册进IOC


## 关于web包使用
修改Startup代码
```
public void ConfigureServices(IServiceCollection services)
{
    ...
    services.AddLuna<SampleWebModule>(opt => { opt.EnableLunaModelValid = false; });
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