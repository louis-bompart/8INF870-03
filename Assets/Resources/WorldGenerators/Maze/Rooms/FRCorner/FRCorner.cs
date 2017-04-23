using UnityEngine;
using System.Collections;

public class FRCorner : GenericMazeZone
{

	public FRCorner()
	{
		this.setOrientation ();
	}

	public FRCorner(Room room, Vector3 position) : base(room, position) { 
		this.setOrientation ();
	}

	protected override void setOrientation(){
		this.leftSideOpen = false;
		this.rightSideOpen = true;
		this.forwardOpen = true;
		this.backOpen = false;
	}

	public override Room GetCopy(Vector3 position)
	{
		return new FRCorner(this, position);
	}

	protected override void Initialize()
	{
		base.Initialize();
		//rules.Add(new CorrespondingDirectionsRule(this));
		prefab = Resources.Load<GameObject>("WorldGenerators/Maze/Rooms/FRCorner/Prefab");
	}

}

