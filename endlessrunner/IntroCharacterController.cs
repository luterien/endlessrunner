using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCharacterController : MonoBehaviour {

    public string[] actionTriggers;

    public float minWaitingTime = 5f;
    public float maxWaitingTime = 10f;

    float waitingTime;

    Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        SetWaitingTime();
    }
	
	// Update is called once per frame
	void Update () {
		
        if (waitingTime <= 0f) {
            PlayAnim();
            SetWaitingTime();
        } else {
            waitingTime -= Time.deltaTime;
        }

	}

    void SetWaitingTime() {
        waitingTime = Random.Range(minWaitingTime, maxWaitingTime);
    }

    void PlayAnim() {
        int index = Random.Range(0, actionTriggers.Length);
        animator.SetTrigger(actionTriggers[index]);
    }

}
