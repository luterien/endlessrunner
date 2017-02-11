using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffSpawnManager : MonoBehaviour {

    public GameObject[] objects;
    public GameObject[] obstacles;

    public float minVerticalDistance;
    public float maxVerticalDistance;

    public float minHorizontalDistance;
    public float maxHorizontalDistance;

    public float yPos;

    int objectCount;
    int obstacleCount;

    public int minStuffToCreate = 2;
    public int maxStuffToCreate = 5;

    public Transform spawnPoint;

    bool spawnStuffBlock;

    public float distanceBetweenEachStuff = 5f;

    KasideGameManager game;

    // Use this for initialization
    void Start () {
        objectCount = objects.Length;
        obstacleCount = obstacles.Length;
        spawnStuffBlock = true;
        game = KasideGameManager.Instance;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnStuff() {

        if (spawnStuffBlock) {
            SpawnStuffBlock();
        } else {
            SpawnRandomly();
        }

    }

    void OnTriggerEnter(Collider hit) {
        if (game.Active() && hit.gameObject.tag == "Player") {
            SpawnStuff();
        }
    }

    void SpawnRandomly() {

        int stuffToCreate = Random.Range(minStuffToCreate, maxStuffToCreate);

        for (int i = 0; i < stuffToCreate; i++) {

            int index = Random.Range(0, objectCount);

            float xPos = Random.Range(-3f, 3f);
            float zPos = Random.Range(minHorizontalDistance, maxHorizontalDistance);

            Instantiate(
                    objects[index],
                    new Vector3(spawnPoint.position.x + xPos, yPos, spawnPoint.position.z + zPos),
                    gameObject.transform.rotation
                );

        }
    }

    void SpawnStuffBlock() {

        int stuffToCreate = Random.Range(minStuffToCreate, maxStuffToCreate);
        float zPos = Random.Range(minHorizontalDistance, maxHorizontalDistance);
        float xPos = Random.Range(-3f, 3f);
        int obstacleIndex = Random.Range(0, stuffToCreate);

        int index;
        
        for (int i = 0; i < stuffToCreate; i++) {

            if (i == obstacleIndex) {

                index = Random.Range(0, obstacleCount);

                Instantiate(
                        obstacles[index],
                        new Vector3(spawnPoint.position.x + distanceBetweenEachStuff * i, yPos, spawnPoint.position.z),
                        gameObject.transform.rotation
                    );


            } else { 

                index = Random.Range(0, objectCount);

                Instantiate(
                        objects[index],
                        new Vector3(spawnPoint.position.x + distanceBetweenEachStuff * i, yPos, spawnPoint.position.z),
                        gameObject.transform.rotation
                    );

            }

        }

    }
}
