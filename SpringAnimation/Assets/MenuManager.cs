using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public delegate void MenuAction();
    public static event MenuAction OnOpenPauseMenu;
    public static event MenuAction OnClosePauseMenu;
    
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
