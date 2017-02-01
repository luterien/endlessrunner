﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuff : MonoBehaviour {

    public int pointsGiven = 10;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            collider.gameObject.SendMessage("AddPoints", pointsGiven);
            GameManager.Instance.PlaySound();
            if (gameObject) Destroy(gameObject);
        }
    }

}
