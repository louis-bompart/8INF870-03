using UnityEngine;
using System.Collections;

public class BRLIntersection : GenericMazeZone
{

	public BRLIntersection()
	{
		this.setOrientation ();
	}

	public BRLIntersection(Room room, Vector3 position) : base(room, position) { 
		this.setOrientation ();
	}

	protected override void setOrientation(){
		this.leftSideOpen = true;
		this.rightSideOpen = true;
		this.forwardOpen = false;
		this.backOpen = true;
	}

	public override Room GetCopy(Vector3 position)
	{
		return new BRLIntersection(this, position);
	}

	protected override void Initialize()
	{
		base.Initialize();
		//rules.Add(new CorrespondingDirectionsRule(this));
		prefab = Resources.Load<GameObject>("WorldGenerators/Maze/Rooms/BRLIntersection/Prefab");
	}

}

