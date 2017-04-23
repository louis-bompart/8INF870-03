using UnityEngine;
using System.Collections;

public class BRCorner : GenericMazeZone
{

	public BRCorner()
	{
		this.setOrientation ();
	}

	public BRCorner(Room room, Vector3 position) : base(room, position) { 
		this.setOrientation ();
	}

	protected override void setOrientation(){
		this.leftSideOpen = false;
		this.rightSideOpen = true;
		this.forwardOpen = false;
		this.backOpen = true;
	}

	public override Room GetCopy(Vector3 position)
	{
		return new BRCorner(this, position);
	}

	protected override void Initialize()
	{
		base.Initialize();
		//rules.Add(new CorrespondingDirectionsRule(this));
		prefab = Resources.Load<GameObject>("WorldGenerators/Maze/Rooms/BRCorner/Prefab");
	}

}

