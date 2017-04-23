using UnityEngine;
using System.Collections;

public class FBRIntersection : GenericMazeZone
{

	public FBRIntersection()
	{
		this.setOrientation ();
	}

	public FBRIntersection(Room room, Vector3 position) : base(room, position) { 
		this.setOrientation ();
	}

	protected override void setOrientation(){
		this.leftSideOpen = false;
		this.rightSideOpen = true;
		this.forwardOpen = true;
		this.backOpen = true;
	}

	public override Room GetCopy(Vector3 position)
	{
		return new FBRIntersection(this, position);
	}

	protected override void Initialize()
	{
		base.Initialize();
		//rules.Add(new CorrespondingDirectionsRule(this));
		prefab = Resources.Load<GameObject>("WorldGenerators/Maze/Rooms/FBRIntersection/Prefab");
	}

}

