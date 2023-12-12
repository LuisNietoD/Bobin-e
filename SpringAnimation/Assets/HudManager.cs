using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    public TextMeshProUGUI nutBoltCount;

    // Update is called once per frame
    void Update()
    {
        nutBoltCount.text = GameManager.instance.nutBolt.ToString();
    }
}
