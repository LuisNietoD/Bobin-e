using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringAnimEvent : MonoBehaviour
{
    public MeshMorphing meshMorph;
    public float weight = 0.01f;
    public AudioSource audioBoing;
    public Animator anim;

    private void Update()
    {
        meshMorph.m_MorphTargets[0].Weight = weight;
    }

    public void PlayBoing()
    {
        if(anim.GetBool("isMoving"))
            audioBoing.Play();
    }
}
