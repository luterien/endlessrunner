using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGroupManager : MonoBehaviour {

    static public LootGroupManager get;

    public List<GameObject> lootGroups = new List<GameObject>();

    List<GameObject> pool = new List<GameObject>();

    public float distanceBetweenLootGroups;

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
}
