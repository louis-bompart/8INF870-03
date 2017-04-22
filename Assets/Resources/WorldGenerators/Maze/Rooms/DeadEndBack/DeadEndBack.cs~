using UnityEngine;
using System.Collections;

public class DeadEndForward : GenericMazeZone
{

	public DeadEndForward()
	{
		this.setOrientation ();
	}

	public DeadEndForward(Room room, Vector3 position) : base(room, position) { 
		this.setOrientation ();
	}

	protected override void setOrientation(){
		this.leftSideOpen = false;
		this.rightSideOpen = false;
		this.forwardOpen = true;
		this.backOpen = false;
	}

	public override Room GetCopy(Vector3 position)
	{
		return new DeadEndForward(this, position);
	}

	protected override void Initialize()
	{
		base.Initialize();
		//rules.Add(new CorrespondingDirectionsRule(this));
		prefab = Resources.Load<GameObject>("WorldGenerators/Maze/Rooms/DeadEndForward/Prefab");
	}

}

