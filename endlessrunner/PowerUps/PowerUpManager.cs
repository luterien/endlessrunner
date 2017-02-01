using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    public List<PowerUp> powerUps = new List<PowerUp>();

    static public PowerUpManager get;

    void Awake() {
        if (get == null) {
            get = this;
        } else if (get != this) {
            Destroy(get);
        }
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnRandomPowerUp(Vector3 spawnPosition) {

        PowerUp powerUp = powerUps[Random.Range(0, powerUps.Count)];

        Instantiate(powerUp.prefab, spawnPosition, powerUp.prefab.transform.rotation);

    }

    public PowerUp GetPowerUp() {
        return powerUps[Random.Range(0, powerUps.Count)];
    }

}
