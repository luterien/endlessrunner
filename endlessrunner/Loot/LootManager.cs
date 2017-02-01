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

    public float powerUpSpawnChance = 0.05f;

    float currentZ;

    public float spawnStartDistance;
    public float spawnEndDistance;

    public Vector3 spawnPosition;

    List<float> lanes = new List<float>() { -5f, 0f, 5f };
    
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
        while (spawnPosition.z < zPoint + platformSize) {
            SpawnLootBlock(lootBox);
        }
    }

    private void SpawnLootBlock(Transform parent) {
        LootBlock lootBlock = GetLootBlock();
        spawnPosition.x = PickLane();
        foreach (LootSpawnData spawnObject in lootBlock.spawnData) {
            SpawnLoot(spawnObject, parent);
        }
        spawnPosition.z += distanceBetweenLootBlocks;
        spawnPosition.y = defaultY;
    }

    private LootBlock GetLootBlock() {
        // return a random block for now
        return lootBlocks[Random.Range(0, lootBlocks.Count)];
    }

    private void SpawnLoot(LootSpawnData spawnData, Transform parent) {
        // update current position
        spawnPosition += spawnData.position;
        // spawn object
        GameObject loot = (GameObject) Instantiate(
            spawnData.loot,
            spawnPosition, 
            spawnData.loot.transform.rotation
        );
        loot.transform.SetParent(parent);
        // check for power up spawn
        TryAndSpawnPowerUp();
    }

    private float PickLane() {
        return lanes[Random.Range(0, lanes.Count)];
    }

    // spawn a power up object alongside the loot block
    private void TryAndSpawnPowerUp() {
        if (Random.value < powerUpSpawnChance) {
            float x;
            if (spawnPosition.x == 0) {
                x = (Random.value > 0.5f ? -5 : 5);
            } else {
                x = (Random.value > 0.5f ? 0 : spawnPosition.x*-1);
            }
            PowerUpManager.get.SpawnRandomPowerUp(
                new Vector3(
                        x,
                        spawnPosition.y,
                        spawnPosition.z
                    )
            );
        }
    }

}
