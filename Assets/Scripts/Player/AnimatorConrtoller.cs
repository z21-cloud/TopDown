using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorConrtoller : MonoBehaviour
{
    private Animator animator;
    private Movement move;
    private void Start()
    {
        animator = GetComponent<Animator>();
        move = GetComponent<Movement>();
    }

    private void Update()
    {
        if (move.MovementVector != Vector3.zero)
        {
            animator.SetBool("isRunning", true);
        }
        else
            animator.SetBool("isRunning", false);
    }
}
