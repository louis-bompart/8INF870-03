using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LWG_Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int seed = Random.Range(0,200) * 100;//18800;//Random.Range(0,200) * 100;//1800;//1300;//Random.Range(0,200) * 100;//18000;//8700;// //14900
		Debug.Log (seed);
		LocalWorldGenerator.Create((int)seed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
