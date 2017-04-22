using UnityEngine;
using System.Collections;

public class HorizontalLine : GenericMazeZone
{

	public HorizontalLine()
	{
		this.setOrientation ();
	}

	public HorizontalLine(Room room, Vector3 position) : base(room, position) { 
		this.setOrientation ();
	}

	protected override void setOrientation(){
		this.leftSideOpen = true;
		this.rightSideOpen = true;
		this.forwardOpen = false;
		this.backOpen = false;
	}

	public override Room GetCopy(Vector3 position)
	{
		return new HorizontalLine(this, position);
	}

	protected override void Initialize()
	{
		base.Initialize();
		//rules.Add(new CorrespondingDirectionsRule(this));
		prefab = Resources.Load<GameObject>("WorldGenerators/Maze/Rooms/HorizontalLine/Prefab");
	}

}

