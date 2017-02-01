using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "KasideRunnerPack/LootBlock")]
public class LootBlock : ScriptableObject {

    [SerializeField]
    public List<LootSpawnData> spawnData = new List<LootSpawnData>();

}
