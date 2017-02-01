using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {

    static public PlatformManager get;

    public List<GameObject> platforms = new List<GameObject>();
    List<GameObject> pool = new List<GameObject>();

    public float platformSize = 267f;
    GameObject nextPlatformObject;

    Vector3 lastPlatformPosition;

    void Awake() {
        if (get == null) {
            get = this;
        } else if (get != this) {
            Destroy(get);
        }
    }
    
    void Start () {
        for (int i=0; i < platforms.Count; i++) { 
            GameObject platformObject = (GameObject) Instantiate(platforms[i], new Vector3(0f, 0f, platformSize * i), Quaternion.identity);
            lastPlatformPosition = platformObject.transform.position;
        }
    }
	
	void Update () {
		
	}

    public void DestroyPlatform(GameObject oldPlatformObject) {

        nextPlatformObject = GetNextPlatform();

        if (oldPlatformObject.tag == nextPlatformObject.tag) {

            // reuse platform
            Reuse(oldPlatformObject);

        } else {

            // try to get it from pool
            GameObject platform = GetPlatformFromPool(nextPlatformObject);
            
            if (platform == null) {
                // can not find, create new
                CreateNewPlatform(nextPlatformObject);
            } else {
                Reuse(platform);
            }

            // add oldplatform to pool
            pool.Add(oldPlatformObject);

        }

    }

    GameObject GetNextPlatform() {
        // just get it randomly for now
        return platforms[Random.Range(0, platforms.Count)];
    }

    GameObject GetPlatformFromPool(GameObject nextPlatformObject) {
        // try to get it from pool
        foreach (GameObject platformObject in pool) {
            if (platformObject.tag == nextPlatformObject.tag) {
                pool.Remove(platformObject);
                return platformObject;
            }
        }
        // none found
        return null;
    }

    void CreateNewPlatform(GameObject nextPlatformObject) {
        lastPlatformPosition.z += platformSize;
        Instantiate(nextPlatformObject, lastPlatformPosition, Quaternion.identity);
    }

    void Reuse(GameObject oldPlatform) {
        lastPlatformPosition.z += platformSize;
        oldPlatform.transform.position = lastPlatformPosition;
        oldPlatform.GetComponent<Platform>().ReInitialize();
    }

}
