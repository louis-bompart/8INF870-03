using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LWGMaze : LocalWorldGenerator
{
	protected override void InitializeRoomList()
	{

		this.rooms = new List<Room>();
		rooms.Add(new DeadEndRight());
		rooms.Add(new DeadEndLeft());
		rooms.Add(new DeadEndForward());
		rooms.Add(new DeadEndBack());
		rooms.Add(new HorizontalLine());
		rooms.Add(new VerticalLine());
		rooms.Add(new Cross());
		rooms.Add(new Wall());
    }

	protected void Start()
	{
		foreach (Vector3 key in localWorld.Keys)
		{
			Instantiate(localWorld[key].prefab, key*roomSize, localWorld[key].prefab.transform.rotation, transform.parent);
		}
		Debug.Log("A World of " + localWorld.Count + " cases has been generated in" + Time.realtimeSinceStartup + "s.");
	}
}


