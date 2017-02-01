using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpDrawer : MonoBehaviour {
    
    // active power ups
    public List<PowerUp> powerUps = new List<PowerUp>();

    // visualization
    public GameObject powerUpIconPrefab;
    public GameObject powerUpIconBox;

    Stats playerStats;

    // Use this for initialization
    void Start () {
        playerStats = GetComponent<Stats>();
    }
	
	void Update () {
        // loop through all powerups and update them
        for (int i = 0; i < powerUps.Count; i++) {
            powerUps[i].OnUpdate(Time.deltaTime);
            if (!powerUps[i].enabled) {
                RemovePowerUp(powerUps[i]);
            }
        }
	}
    
    public void AddPowerUp(PowerUp powerUp) {

        // enable the power up first
        powerUp.Start();

        // add it to the list
        if (powerUps.Contains(powerUp)) {
            powerUps.Find(power => power == powerUp).ResetDuration();
        } else {
            powerUps.Add(powerUp);
            playerStats.ApplyPowerUp(powerUp);
            Visualize(powerUp);
        }

    }

    void RemovePowerUp(PowerUp powerUp) {

        // remove from the list
        powerUps.Remove(powerUp);

        // remove powerup effect
        playerStats.UnapplyPowerUp(powerUp);

        DeVisualize(powerUp);

    }

    void Visualize(PowerUp powerUp) {
        GameObject iconObject = (GameObject) Instantiate(powerUpIconPrefab, powerUpIconBox.transform);
        iconObject.GetComponent<Image>().sprite = powerUp.icon;
        iconObject.name = powerUp.powerUpName;
    }

    void DeVisualize(PowerUp powerUp) {
        foreach (Transform iconObject in powerUpIconBox.transform) {
            if (iconObject.gameObject.name == powerUp.powerUpName) {
                Destroy(iconObject.gameObject);
                return;
            }
        }
    }

}
