using UnityEngine;
using System.Collections;

public class BLCorner : GenericMazeZone
{

	public BLCorner()
	{
		this.setOrientation ();
	}

	public BLCorner(Room room, Vector3 position) : base(room, position) { 
		this.setOrientation ();
	}

	protected override void setOrientation(){
		this.leftSideOpen = true;
		this.rightSideOpen = false;
		this.forwardOpen = false;
		this.backOpen = true;
	}

	public override Room GetCopy(Vector3 position)
	{
		return new BLCorner(this, position);
	}

	protected override void Initialize()
	{
		base.Initialize();
		//rules.Add(new CorrespondingDirectionsRule(this));
		prefab = Resources.Load<GameObject>("WorldGenerators/Maze/Rooms/BLCorner/Prefab");
	}

}

