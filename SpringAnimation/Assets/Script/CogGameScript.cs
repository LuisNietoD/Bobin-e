using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogGameScript : MonoBehaviour
{
    public Camera cam;
    public GameObject toHide;

    public void HideAll()
    {
        if(toHide!=null)
            toHide.SetActive(false);
    }

    public void ShowAll()
    {
        if(toHide!=null)
            toHide.SetActive(true);
    }
}
