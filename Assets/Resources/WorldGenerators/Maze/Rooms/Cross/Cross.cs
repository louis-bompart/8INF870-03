using UnityEngine;
using System.Collections;

public class Cross : GenericMazeZone
{

	public Cross()
	{
		this.setOrientation ();
	}

	public Cross(Room room, Vector3 position) : base(room, position) { 
		this.setOrientation ();
	}

	protected override void setOrientation(){
		this.leftSideOpen = true;
		this.rightSideOpen = true;
		this.forwardOpen = true;
		this.backOpen = true;
	}

	public override Room GetCopy(Vector3 position)
	{
		return new Cross(this, position);
	}

	protected override void Initialize()
	{
		base.Initialize();
		//rules.Add(new CorrespondingDirectionsRule(this));
		prefab = Resources.Load<GameObject>("WorldGenerators/Maze/Rooms/Cross/Prefab");
	}

}

