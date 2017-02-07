using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatform : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            PlatformManager.get.DestroyPlatform(transform.parent.gameObject);
        }
    }

}
