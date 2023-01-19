using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UpgradeService : IService
{
    private Dictionary<string, IUpgrade> upgrades = new Dictionary<string, IUpgrade>();
    public async Task Initialize()
    {
        GetDataFromServer();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }

    public void Injection(Dictionary<Type, IService> services)
    {
        //Empty
    }

    public IUpgrade GetUpgradeModel(string id)
    {
        if (upgrades.ContainsKey(id))
        {
            return upgrades[id];
        }
        
        MindfulDebugger.DebugError("No upgrade with the ID: " + id,
            MindfulDebugger.DEBUGURGENCY.HIGH);
        return new UpgradeModel("", 0, 0, 0);
    }

    public IUpgrade AddToUpgradeModel(string id)
    {
        if (upgrades.ContainsKey(id))
        {
            upgrades[id].IncrementUpgrade();
            return upgrades[id];
        }
        
        MindfulDebugger.DebugError("No currency with the ID: " + id,
            MindfulDebugger.DEBUGURGENCY.HIGH);
        return new UpgradeModel("", 0, 0, 0);
    }
    
    private void GetDataFromServer()
    {
        MindfulDebugger.DebugMessage("Currency's are not being pulled from database.",
            MindfulDebugger.DEBUGURGENCY.HIGH);
        upgrades.Add("Basic", new UpgradeModel("Basic", 1, 1, 100));
    }
}
