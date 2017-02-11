using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {

    public AudioClip pickUpSound;

    public int points;

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            // play sound effect
            SoundManager.get.Play(pickUpSound);
            // add points to player
            collider.gameObject.SendMessage("AddPoints", points);
            collider.gameObject.SendMessage("DisplayPickUpEffect");
            // destroy the item
            Destroy(gameObject);
        }
    }

}
