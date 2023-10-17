using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterState
{
    public void OnEnter();

    public void OnUpdate();

    public void OnExit();
}

public class MyInterfaces : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
