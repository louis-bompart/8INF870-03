using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPosition : MonoBehaviour
{


    public GameObject player;
    public bool finished = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            if ((player.transform.position - this.gameObject.transform.position).magnitude <= 2)
            {
                finished = true;
            }
        }
        if(finished)
        {
            player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        }
    }
}
