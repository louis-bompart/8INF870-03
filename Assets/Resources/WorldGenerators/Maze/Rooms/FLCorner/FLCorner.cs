using UnityEngine;
using System.Collections;

public class FLCorner : GenericMazeZone
{

	public FLCorner()
	{
		this.setOrientation ();
	}

	public FLCorner(Room room, Vector3 position) : base(room, position) { 
		this.setOrientation ();
	}

	protected override void setOrientation(){
		this.leftSideOpen = true;
		this.rightSideOpen = false;
		this.forwardOpen = true;
		this.backOpen = false;
	}

	public override Room GetCopy(Vector3 position)
	{
		return new FLCorner(this, position);
	}

	protected override void Initialize()
	{
		base.Initialize();
		//rules.Add(new CorrespondingDirectionsRule(this));
		prefab = Resources.Load<GameObject>("WorldGenerators/Maze/Rooms/FLCorner/Prefab");
	}

}

