# LunaFramework

## mygetԴ
https://www.myget.org/F/luna/api/v3/index.json
```
Install-Package Luna
Install-Package Luna.Castle.Nlog
Install-Package Luna.Web
```

## ����ʹ��
1. �����Ҳʹ��nlogд��־�Ļ�����ֱ������ Luna.Service.Nlog ��.������־��һ�����û��Զ����ص���Ŀ��
2. ��Main��������������
    ```
	using (var starter = LunaStarter.Create<Program>())
	{
		starter.IocManager.IocContainer.AddFacility<LoggingFacility>(m =>
			m.LogUsing<NLogFactory>().WithConfig("NLog.config"));

		starter.Initialize();
	}
    ```
1. �������service����������
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
    		return "����";
    	}
    }
    ```
    ע��ӿ�ʵ��ILunaService���ܱ��Զ�ע���IOC

�����ϲ����nlog�Ļ�,���Բ鿴Castle.Windsor�ĵ��޸ĵ�3����AddFacility����

## ����web��ʹ��
�޸�Startup����
```
public IServiceProvider ConfigureServices(IServiceCollection services)
{
	...
	return services.AddLuna<Startup>();
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
	...
	// ��UseMvc֮ǰ����
	app.UseLuna();
	...
}
```

**��ĿԼ��: ��ĳ�������������ѭaaa.bb.c�ķ�ʽ**
```
 Demo.App        <- Ӧ�ó������
 Demo.Service    <- �����
 Demo.Entity     <- ʵ���
```