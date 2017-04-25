using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LWG_Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int seed = 1300;//Random.Range(0,200) * 100;//18000;//8700;//Random.Range(0,200) * 100; //48 jolie,8700, pb : 180
		Debug.Log (seed);
		LocalWorldGenerator.Create((int)seed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
