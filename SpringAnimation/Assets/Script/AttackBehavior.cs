
using UnityEngine;


public interface AttackBehavior
{
    
    public string Name { get; set; }
    public abstract void Action();

    public abstract void StartMethods(GameObject slot);

    public abstract void UpdateMethods();
}
