using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShader : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Camera.main.SetReplacementShader(Shader.Find("Hidden/VacuumShaders/Curved World/VertexLit/Cutout"), "CurvedWorld__TransparentCutout");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
