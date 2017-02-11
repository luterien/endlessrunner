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

        // if power up is already active, reset its duration
        if (powerUps.Contains(powerUp)) {

            powerUps.Find(power => power == powerUp).ResetDuration();
            Toast.get.Show(powerUp.powerUpName);

        } else {

            // check for other powerups with similar effect
            for (int i=0; i < powerUps.Count; i++) {
                if (powerUps[i].targetStat == powerUp.targetStat) {

                    // powerup with a stronger effect has been picked up
                    if (powerUps[i].statMultiplier < powerUp.statMultiplier) {

                        UnapplyPowerUp(powerUps[i]);
                        ApplyPowerUp(powerUp);

                        powerUps.RemoveAt(i);
                        powerUps.Add(powerUp);
                        Toast.get.Show(powerUp.powerUpName);
                        return;

                    
                    } else if (powerUps[i].statMultiplier > powerUp.statMultiplier) {
                        // a stronger power up is already active
                        return;
                    }
                }
            }

            // add to the list
            powerUps.Add(powerUp);
            // apply
            ApplyPowerUp(powerUp);
            // show text
            Toast.get.Show(powerUp.powerUpName);

        }

    }

    void RemovePowerUp(PowerUp powerUp) {
        powerUps.Remove(powerUp);
        UnapplyPowerUp(powerUp);
    }

    private void ApplyPowerUp(PowerUp powerUp) {
        // apply the effect to player
        playerStats.ApplyPowerUp(powerUp);
        // add icon to ui
        Visualize(powerUp);
    }

    private void UnapplyPowerUp(PowerUp powerUp) {
        // remove effect from player
        playerStats.UnapplyPowerUp(powerUp);
        // remove icon from ui
        DeVisualize(powerUp);
    }

    /*
     
        UI stuff
         
    */

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
