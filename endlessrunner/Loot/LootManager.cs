using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour {

    static public LootManager get;

    public List<LootBlock> lootBlocks = new List<LootBlock>();

    public float distanceBetweenLootBlocks;

    public float currentX = 0f;
    public float currentY = 0f;

    public float defaultX = 0f;
    public float defaultY = 0f;

    public float startingZ = 0f;

    public float powerUpSpawnChance = 0.05f;

    float currentZ;

    public float spawnStartDistance;
    public float spawnEndDistance;

    public Vector3 spawnPosition;

    List<float> lanes = new List<float>() { -4.2f, 0f, 4.2f };
    
    void Awake() {

        if (get == null) {
            get = this;
        } else if (get != this) {
            Destroy(get);
        }

    }

    void Start() {
        currentZ = spawnStartDistance;
        spawnPosition = new Vector3(defaultX, defaultY, currentZ);
    }
    
    /*
     
        spawn methods
     
    */

    // handles loot spawn for each platform
    public void DoSpawn(float zPoint, Transform lootBox, float platformSize) {
        // set spawn Z and reset points X and Y
        spawnPosition = new Vector3(defaultX, defaultY, zPoint + spawnStartDistance);
        // try to spawn blocks until we are out of bounds
        while (spawnPosition.z - startingZ < zPoint + platformSize - spawnEndDistance) {
            LootBlock lootBlock = GetLootBlock();
            if (lootBlock == null) return;
            SpawnLootBlock(lootBlock, lootBox);
        }
    }

    private void SpawnLootBlock(LootBlock lootBlock, Transform parent) {
        // set position
        spawnPosition.x = PickLane();
        spawnPosition.z += distanceBetweenLootBlocks;
        spawnPosition.y = defaultY;
        // spawn all objects
        foreach (LootSpawnData spawnObject in lootBlock.spawnData) {
            SpawnLoot(spawnObject, parent);
        }
    }

    private LootBlock GetLootBlock() {
        // return a random block for now
        try {
            return lootBlocks[Random.Range(0, lootBlocks.Count)];
        } catch (System.ArgumentOutOfRangeException) {
            Debug.LogError("LootBlock could not be found.");
        }
        return null;
    }

    private void SpawnLoot(LootSpawnData spawnData, Transform parent) {
        // update current position
        spawnPosition += spawnData.position;
        // spawn object
        GameObject loot = (GameObject) Instantiate(
            spawnData.loot,
            new Vector3(spawnPosition.x, spawnData.loot.transform.position.y, spawnPosition.z), 
            spawnData.loot.transform.rotation
        );
        loot.transform.SetParent(parent);
    }

    private float PickLane() {
        return lanes[Random.Range(0, lanes.Count)];
    }

}
