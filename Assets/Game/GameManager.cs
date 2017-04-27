using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{


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
    public UnityEngine.UI.Text score;
    private GameObject finish;
    private bool initialzed = false;
    private float startTime;
    public GameObject panel;
    bool doOnce;

    void Start()
    {
        doOnce = false;
        int seed = /*11400;*/UnityEngine.Random.Range(0, 200) * 100;
        Debug.Log(seed);
        world = LocalWorldGenerator.Create((int)seed, size);
        startTime = Time.time;

    }
    void Update()
    {
        if (!initialzed && world.finishInitialize)
        {
            start = GameObject.FindGameObjectWithTag("Respawn");
            start.AddComponent<StartPosition>();

            finish = GameObject.FindGameObjectWithTag("Finish");
            finish.AddComponent<FinishPosition>();
            initialzed = true;
        }
        else
        {
            if (finish.GetComponent<FinishPosition>().finished)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    endGame();
                DisplayScore();
            }
        }
    }

    private void DisplayScore()
    {
        if (!Cursor.visible)
        {
            Cursor.visible = true;
        }
        int time = (int)(startTime - Time.time);
        score.text = time / 60 + "m" + time % 60 + "s";
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void endGame()
    {
        SceneManager.LoadScene("Menu");
    }


}
