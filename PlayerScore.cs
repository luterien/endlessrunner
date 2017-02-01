using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

    public int points = 0;
    public Text scoreText;

	// Use this for initialization
	void Start () {
        scoreText.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddPoints(int point) {
        points += point;
        scoreText.text = points.ToString();
    }

}
