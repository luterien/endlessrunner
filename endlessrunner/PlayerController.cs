using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
     for legacy models
     
     */

public class PlayerController : Controller {

    Animation anim;

    protected override void Load() {
        anim = GetComponent<Animation>();
    }

    protected override void Animate() {
        if (state == PlayerState.Running) {
            anim.Play(run);
        } else if (state == PlayerState.Jumping) {
            anim.Play(jump);
        } else if (state == PlayerState.Idle) {
            anim.Play(idle);
        } else if (state == PlayerState.Falling) {
            anim.Play(fall);
        } else if (state == PlayerState.Dead) {
            anim.Play(die);
        }
    }
}
