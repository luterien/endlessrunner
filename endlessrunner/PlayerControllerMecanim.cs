using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
     for mecanim-ready models
     
     */

public class PlayerControllerMecanim : Controller {

    Animator animator;

    protected override void Load() {
        animator = GetComponent<Animator>();
    }

    protected override void Animate() {
        if (state == PlayerState.Running) {
            animator.SetTrigger(run);
        } else if (state == PlayerState.Jumping) {
            animator.SetTrigger(jump);
        } else if (state == PlayerState.Idle) {
            animator.SetTrigger(idle);
        } else if (state == PlayerState.Falling) {
            animator.SetTrigger(fall);
        } else if (state == PlayerState.Dead) {
            animator.SetTrigger(die);
        } else if (state == PlayerState.SwitchLane) {
            animator.SetTrigger(run);
        }
    }


}
