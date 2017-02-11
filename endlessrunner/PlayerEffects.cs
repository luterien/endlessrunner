using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour {

    public ParticleSystem pickUpEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DisplayPickUpEffect() {
        pickUpEffect.Play();
    }

}
