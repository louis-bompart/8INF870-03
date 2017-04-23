using UnityEngine;
using System.Collections;

public class FBLIntersection : GenericMazeZone
{

	public FBLIntersection()
	{
		this.setOrientation ();
	}

	public FBLIntersection(Room room, Vector3 position) : base(room, position) { 
		this.setOrientation ();
	}

	protected override void setOrientation(){
		this.leftSideOpen = true;
		this.rightSideOpen = false;
		this.forwardOpen = true;
		this.backOpen = true;
	}

	public override Room GetCopy(Vector3 position)
	{
		return new FBLIntersection(this, position);
	}

	protected override void Initialize()
	{
		base.Initialize();
		//rules.Add(new CorrespondingDirectionsRule(this));
		prefab = Resources.Load<GameObject>("WorldGenerators/Maze/Rooms/FBLIntersection/Prefab");
	}

}

