using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject playerPrefab = Resources.Load<GameObject>("WorldGenerators/FPSController");
		GameObject player = GameObject.Instantiate (playerPrefab);
		player.tag = "Player";
		player.transform.position = this.gameObject.transform.position;
	}
	

}
