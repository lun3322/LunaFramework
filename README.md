# LunaFramework

## mygetԴ
https://www.myget.org/F/luna/api/v3/index.json
```
Install-Package Luna
Install-Package Luna.Castle.Nlog
```

## ����ʹ��
1. �����Ҳʹ��nlogд��־�Ļ�����ֱ������ Luna.Service.Nlog ��.������־��һ�����û��Զ����ص���Ŀ��
2. ��Main��������������
    ```
    using (var starter = Starter.Create<Runner>())
    {
    	starter.Container.AddFacility<LoggingFacility>(f => f.LogUsing<NLogFactory>().WithConfig("NLog.config"));
    
    	starter.Run();
    }
    ```
1. �������service����������
    ```
    public interface IDemoService : ILunaService
    {
    	string GetMessage();
    }
    
    public class DemoService : LunaServiceBase, IDemoService
    {
    	public string GetMessage()
    	{
    		Logger.Info("GetMessage");
    		return "����";
    	}
    }
    ```
    ע��ӿ�ʵ��ILunaService���ܱ��Զ�ע���IOC
3. �޸�Runner���run����
    ```
    public class Runner : LunaRunnerBase
    {
    	private readonly IDemoService _demoService;
    
    	public Runner(IDemoService demoService)
    	{
    		_demoService = demoService;
    	}
    
    	public override void Run()
    	{
    		var message = _demoService.GetMessage();
    		Logger.Info(message);
    		Logger.Info("ok");
    	}
    }
    ```

�����ϲ����nlog�Ļ�,���Բ鿴Castle.Windsor�ĵ��޸ĵ�3����AddFacility����

**��ĿԼ��: ��ĳ�������������ѭaaa.bb.c�ķ�ʽ**
```
 Demo.App        <- Ӧ�ó������
 Demo.Service    <- �����
 Demo.Entity     <- ʵ���
```