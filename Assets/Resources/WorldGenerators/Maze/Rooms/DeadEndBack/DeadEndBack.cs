using UnityEngine;
using System.Collections;

public class DeadEndBack : GenericMazeZone
{

	public DeadEndBack()
	{
		this.setOrientation ();
	}

	public DeadEndBack(Room room, Vector3 position) : base(room, position) { 
		this.setOrientation ();
	}

	protected override void setOrientation(){
		this.leftSideOpen = false;
		this.rightSideOpen = false;
		this.forwardOpen = false;
		this.backOpen = true;
	}

	public override Room GetCopy(Vector3 position)
	{
		return new DeadEndBack(this, position);
	}

	protected override void Initialize()
	{
		base.Initialize();
		canBeFirst = true;
		//rules.Add(new CorrespondingDirectionsRule(this));
		prefab = Resources.Load<GameObject>("WorldGenerators/Maze/Rooms/DeadEndBack/Prefab");
	}

}

