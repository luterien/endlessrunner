using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnManager : MonoBehaviour {

    public GameObject[] blocks;
    public Transform spawnPoint;

    public GameManager game;
    
	void Start () {
        game = GameManager.Instance;
	}
	
	void Update () {
		
	}

    void SpawnNewBlock() {
        Instantiate(getBlock(), spawnPoint.position, gameObject.transform.rotation);
    }

    void OnTriggerEnter(Collider hit) {
        if (game.Active() && hit.gameObject.tag == "Player") {
            SpawnNewBlock();
        }
    }

    GameObject getBlock() {
        return blocks[Random.Range(0, blocks.Length - 1)];
    }

}
