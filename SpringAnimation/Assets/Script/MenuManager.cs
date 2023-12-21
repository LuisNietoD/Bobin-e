using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static event Action OnOpenPauseMenu;
    public static event Action OnClosePauseMenu;
    
    public static void OpenPauseMenu()
    {
        OnOpenPauseMenu?.Invoke();
    }

    public static void ClosePauseMenu()
    {
        OnClosePauseMenu?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !GameManager.isPlayerLock)
        {
            OpenPauseMenu();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if(Input.GetKeyDown(KeyCode.P))
        {
            ClosePauseMenu();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
