using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singe : ClassMereEnemy
{
    private void Update()
    {
        pv++;

        TakeDamage(1);
    }

    public override void TakeDamage(float damage)
    {


        base.TakeDamage(damage);

        if(pv <= 0)
        {

        }
    }
    

}
