using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{

    public float rotationSpeed = 2;
    void Update()
    {
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y + rotationSpeed * Time.deltaTime, 0f);
    }
}
