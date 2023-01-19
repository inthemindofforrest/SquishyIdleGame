using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IService
{
    Task Initialize();
    void Update();
    void Injection(Dictionary<Type, IService> services);
}
