using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LookAtIK : MonoBehaviour
{
    protected Animator animator;

    public bool ikAktive = false;
    public Transform lookObj = null;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            if (ikAktive)
            {

                if (lookObj != null)
                {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }
                
            }
            else
            {
                animator.SetLookAtWeight(0);
            }
        }
    }
}
