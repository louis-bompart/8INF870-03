﻿using UnityEngine;
using System.Collections;

public class FRLIntersection : GenericMazeZone
{

	public FRLIntersection()
	{
		this.setOrientation ();
	}

	public FRLIntersection(Room room, Vector3 position) : base(room, position) { 
		this.setOrientation ();
	}

	protected override void setOrientation(){
		this.leftSideOpen = true;
		this.rightSideOpen = true;
		this.forwardOpen = true;
		this.backOpen = false;
	}

	public override Room GetCopy(Vector3 position)
	{
		return new FRLIntersection(this, position);
	}

	protected override void Initialize()
	{
		base.Initialize();
		//rules.Add(new CorrespondingDirectionsRule(this));
		prefab = Resources.Load<GameObject>("WorldGenerators/Maze/Rooms/FRLIntersection/Prefab");
	}

}

