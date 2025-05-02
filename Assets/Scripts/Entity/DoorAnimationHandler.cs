using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimationHandler : MonoBehaviour
{
    private static readonly int IsLocked = Animator.StringToHash("Lock");
    private static readonly int IsOped = Animator.StringToHash("Open");

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Lock()
    {
        animator.SetBool(IsLocked, true);
    }

    public void UnLock()
    {
        animator.SetBool(IsLocked, false);
    }

    public void Open()
    {
        animator.SetBool(IsOped, true);
    }

}
