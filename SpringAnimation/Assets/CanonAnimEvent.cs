using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonAnimEvent : MonoBehaviour
{
    public Canon canon;

    public void ShootCanon()
    {
        canon.Shoot();
    }
}
