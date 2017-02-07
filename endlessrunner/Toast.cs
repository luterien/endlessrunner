using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour {

    static public Toast get;
    
    public Text textUI;

    bool displaying = false;

    public float displayDuration = 1f;
    float counter;

    void Awake() {
        if (get == null) {
            get = this;
        } else if (get != this) {
            Destroy(get);
        }
    }

    public void Show(string msg) {
        textUI.gameObject.SetActive(true);
        textUI.text = msg;
        displaying = true;
        counter = displayDuration;
    }

    void Update() {
        if (displaying) {
            if (counter <= 0) {
                HideText();
            } else {
                counter -= Time.deltaTime;
            }
        }
    }

    void HideText() {
        textUI.gameObject.SetActive(false);
        displaying = false;
    }

}
