using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LWGMaze : LocalWorldGenerator
{

	public int sideOfTheSquare;

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
	}

	protected void Start()
	{
		foreach (Vector3 key in localWorld.Keys)
		{
			Instantiate(localWorld[key].prefab, key*roomSize, localWorld[key].prefab.transform.rotation, transform.parent);
		}
		Debug.Log("A World of " + localWorld.Count + " cases has been generated in" + Time.realtimeSinceStartup + "s.");
	}

	protected override void GenerateCSP()
	{
		csp = new Dictionary<Vector3, List<Room>>();
		List<Room> tmp = new List<Room>();
		tmp.Add(localWorld[Vector3.zero]);
		csp.Add(Vector3.zero, tmp);
		for (int i = 0; i < sideOfTheSquare; i++)
		{
			for (int j = 0; j < sideOfTheSquare; j++)
			{
				Vector3 currentPos = new Vector3(i, 0, j);
				//Debug.Log (currentPos);
				if (i != 0 || j != 0)
				{
					tmp = generateRoomsCopy(currentPos);
					//tmp = generateRoomsCopy(-currentPos);
					csp.Add(currentPos, tmp);
				}

			}
		}
	}
}


