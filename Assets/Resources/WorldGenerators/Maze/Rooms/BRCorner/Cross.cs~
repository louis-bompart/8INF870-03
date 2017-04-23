using UnityEngine;
using System.Collections;

public class VerticalLine : GenericMazeZone
{

	public VerticalLine()
	{
		this.setOrientation ();
	}

	public VerticalLine(Room room, Vector3 position) : base(room, position) { 
		this.setOrientation ();
	}

	protected override void setOrientation(){
		this.leftSideOpen = false;
		this.rightSideOpen = false;
		this.forwardOpen = true;
		this.backOpen = true;
	}

	public override Room GetCopy(Vector3 position)
	{
		return new VerticalLine(this, position);
	}

	protected override void Initialize()
	{
		base.Initialize();
		//rules.Add(new CorrespondingDirectionsRule(this));
		prefab = Resources.Load<GameObject>("WorldGenerators/Maze/Rooms/VerticalLine/Prefab");
	}

}

