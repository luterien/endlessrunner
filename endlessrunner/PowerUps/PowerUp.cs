using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "KasideRunnerPack/Power Up")]
public class PowerUp : ScriptableObject {

    public string powerUpName;
    public float duration;
    public Sprite icon;
    public GameObject prefab;
    public ParticleSystem particle;

    public enum TargetStat {
        HorizontalSpeed,
        VerticalSpeed,
    }
    public TargetStat targetStat;

    public float statMultiplier;

    public float counter;
    public bool enabled = false;

    public void Start() {
        counter = duration;
        enabled = true;
    }

    public void OnUpdate(float deltaTime) {
        if (counter <= 0f) {
            Disable();
        } else {
            counter -= deltaTime;
        }
    }

    private void Disable() {
        enabled = false;
    }

    public void ResetDuration() {
        counter = duration;
    }

}
