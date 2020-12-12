using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBAppear : MonoBehaviour
{
    [SerializeField] Animator gba;
    [SerializeField] AnimationClip anim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gba.Play(anim.name);
        }
    }
}
