using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour {

    public float timer = 2f;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void DoSelfDestroy() {
        if (gameObject) Destroy(gameObject);
    }

    void OnCollisionExit(Collision collision) {
        if (KasideGameManager.Instance.Active() && collision.gameObject.tag == "Player") {
            Invoke("DoSelfDestroy", timer);
        }
    }

}
