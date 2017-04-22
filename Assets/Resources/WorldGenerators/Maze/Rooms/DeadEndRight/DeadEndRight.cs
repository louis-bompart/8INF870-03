using UnityEngine;
using System.Collections;

public class DeadEndRight : GenericMazeZone
{

	public DeadEndRight()
	{
		this.setOrientation ();
	}

	public DeadEndRight(Room room, Vector3 position) : base(room, position) { 
		this.setOrientation ();
	}

	protected override void setOrientation(){
		this.leftSideOpen = false;
		this.rightSideOpen = true;
		this.forwardOpen = false;
		this.backOpen = false;
	}

	public override Room GetCopy(Vector3 position)
	{
		return new DeadEndRight(this, position);
	}

	protected override void Initialize()
	{
		base.Initialize();
		//rules.Add(new CorrespondingDirectionsRule(this));
		prefab = Resources.Load<GameObject>("WorldGenerators/Maze/Rooms/DeadEndRight/Prefab");
	}

}

