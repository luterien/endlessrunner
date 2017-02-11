using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootSpawnData {

    public GameObject loot;

    // position of loot compared to the loot before this one
    public Vector3 position;

    public float rotationDifference = 0f;

}
