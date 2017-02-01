using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {

    public int points;

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            // add points to player
            collider.gameObject.SendMessage("AddPoints", points);
            // destroy the item
            Destroy(gameObject);
        }
    }

}
