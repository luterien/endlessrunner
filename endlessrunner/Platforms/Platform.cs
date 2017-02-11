using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    Transform lootDrawer;

	void Start () {
        lootDrawer = transform.Find("Loot");
        InitializeProps();
    }
	
	void Update () {
		
	}

    void InitializeProps() {
        LootManager.get.DoSpawn(lootDrawer.transform.position.z, lootDrawer, PlatformManager.get.platformSize);
    }

    public void ReInitialize() {
        // remove old props
        foreach (Transform child in lootDrawer) {
            Destroy(child.gameObject);
        }
        // add new ones
        InitializeProps();
    }

}
