using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpAsLoot : MonoBehaviour {

    public PowerUp powerUp;

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            // add points to player
            collider.gameObject.SendMessage("AddPowerUp", powerUp);
            // destroy the item
            Destroy(gameObject);
        }
    }

}
