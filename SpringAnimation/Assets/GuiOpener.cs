using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiOpener : MonoBehaviour
{
    public delegate void MenuAction();
    public static event MenuAction OnEnableMenu;

    public GameObject inventory;
    
    public static void OpenPauseMenu()
    {
        OnEnableMenu?.Invoke();
    }
    
    private void OnEnable()
    {
        OnEnableMenu += EnableInventory;
        MenuManager.OnClosePauseMenu += DisableInventory;
    }
    
    private void OnDisable()
    {
        OnEnableMenu -= EnableInventory;
        MenuManager.OnClosePauseMenu -= DisableInventory;
    }
    
    
    

    public void EnableInventory()
    {
        inventory.SetActive(true);
    }
    
    public void DisableInventory()
    {
        inventory.SetActive(false);

    }
}
