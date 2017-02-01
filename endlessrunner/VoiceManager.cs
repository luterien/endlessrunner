using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour {

    public AudioSource[] startVoices;
    public AudioSource[] shoutingVoices;
    public AudioSource[] restartVoices;
    public AudioSource[] randomVoices;
    public AudioSource[] jumpVoices;

    AudioSource[] voiceList = null;

    public enum VoiceTypes {
        Start,
        Shout,
        Restart,
        RandomLine,
        Jump
    }

    public static VoiceManager Instance = null;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public void Play(VoiceTypes voice) {

        switch (voice) {

            case VoiceTypes.Start:
                voiceList = startVoices;
                break;

            case VoiceTypes.Shout:
                voiceList = shoutingVoices;
                break;

            case VoiceTypes.Jump:
                voiceList = jumpVoices;
                break;

        }

        if (voiceList != null) {
            getRandomVoice(voiceList).Play();
        }

    }

    AudioSource getRandomVoice(AudioSource[] array) {
        int i = Random.Range(0, array.Length);
        return array[i];
    }

}
