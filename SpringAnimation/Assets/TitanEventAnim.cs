using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanEventAnim : MonoBehaviour
{
    public RangeAttack range;

    public void RangeAttackAnim()
    {
        range.Shoot();
    }

    public void SpawnProjectile()
    {
        range.SpawnProjectile();
    }
}
