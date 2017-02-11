using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    static public SoundManager get;

    AudioSource audioSource;
    
	void Awake () {
		if (get == null) {
            get = this;
        } else if (get != this) {
            Destroy(get);
        }
	}

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }
	
	public void Play(AudioClip clip) {
        audioSource.clip = clip;
        audioSource.Play();
    }

}
