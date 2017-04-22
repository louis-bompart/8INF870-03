using UnityEngine;
using System.Collections;

public class DeadEndLeft : GenericMazeZone
{

	public DeadEndLeft()
	{
		this.setOrientation ();
	}

	public DeadEndLeft(Room room, Vector3 position) : base(room, position) { 
		this.setOrientation ();
	}

	protected override void setOrientation(){
		this.leftSideOpen = true;
		this.rightSideOpen = false;
		this.forwardOpen = false;
		this.backOpen = false;
	}

	public override Room GetCopy(Vector3 position)
	{
		return new DeadEndLeft(this, position);
	}

	protected override void Initialize()
	{
		base.Initialize();
		//rules.Add(new CorrespondingDirectionsRule(this));
		prefab = Resources.Load<GameObject>("WorldGenerators/Maze/Rooms/DeadEndLeft/Prefab");
	}

}

