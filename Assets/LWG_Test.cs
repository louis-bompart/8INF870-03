using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LWG_Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LocalWorldGenerator.Create((int)Random.value*250000);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
