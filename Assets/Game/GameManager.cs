using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {


	/*public GameObject text;
	public float initialTime;
	private float remaningTime;

	// Use this for initialization
	void Start () {
		
		remaningTime = initialTime;
		Debug.Log (text.GetType ());
	}
	
	// Update is called once per frame
	void Update () {
		remaningTime -= Time.deltaTime;
	}

	private void DisplayTime(){
	}*/


	public int size;
	private LocalWorldGenerator world;
	private GameObject start;
	private GameObject finish;
	private bool initialzed = false;

	void Start(){
		int seed = 11400;//Random.Range(0,200) * 100;
		Debug.Log (seed);
		world = LocalWorldGenerator.Create((int)seed, size);

	}
	void Update(){
		if (!initialzed && world.finishInitialize) {
			start = GameObject.FindGameObjectWithTag ("Respawn");
			start.AddComponent<StartPosition> ();

			finish = GameObject.FindGameObjectWithTag ("Finish");
			finish.AddComponent<FinishPosition> ();
			initialzed = true;
		} else {
			if (finish.GetComponent<FinishPosition> ().finished) {
				endGame ();
			}
		}
	}

	private void endGame(){
		SceneManager.LoadScene ("Menu");
	}
		

}
