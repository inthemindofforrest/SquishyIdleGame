using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngineInternal;
using Random = UnityEngine.Random;

public static class ServiceController
{
    private static Dictionary<Type, IService> services = new Dictionary<Type, IService>();
    private static int initializedServiceCount;
    private static bool finishedInitializing;
    
    
    public static async Task InitializeServiceController()
    {
        services.Add(typeof(CurrencyService), new CurrencyService());
        services.Add(typeof(StatisticsService), new StatisticsService());
        services.Add(typeof(QuestService), new QuestService());
        
        await InitializeServices();
        InjectServices();
    }

    public static void UpdateServices()
    {
        foreach (var service in services)
        {
            service.Value.Update();
        }
    }    
    public static async Task<IService> GetService<T>() where T : IService
    {
        await WaitForInitialized();
        
        if (services.ContainsKey(typeof(T)))
        {
            return services[typeof(T)];
        }

        return null;
    }

    public static async Task WaitForInitialized()
    {
        while (!finishedInitializing)
        {
            await Task.Delay(100);
        }
    }

    public static float GetInitializedPercent()
    {
        return (float)initializedServiceCount / services.Count;
    }
    
    private static async Task InitializeServices()
    {
        foreach(var service in services)
        {
            Task.Run(async () =>
            {
                await service.Value.Initialize();
                initializedServiceCount++;

                if (initializedServiceCount >= services.Count)
                {
                    finishedInitializing = true;
                }
            });
        }
        await WaitForInitialized();
    }
    
    private static void InjectServices()
    {
        foreach (var service in services)
        {
            service.Value.Injection(services);
        }
    }
}
