using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericMazeZone : Room {

	public bool leftSideOpen;
	public bool rightSideOpen;
	public bool forwardOpen;
	public bool backOpen;

	public GenericMazeZone()
	{
	}

	public GenericMazeZone(Room room, Vector3 position) : base(room, position) { }

	protected abstract void setOrientation ();

	/*public override Room GetCopy(Vector3 position)
	{
		return new GenericMazeZone(this, position);
	}*/

	protected override void Initialize()
	{
		base.Initialize();
		rules.Add(new CorrespondingDirectionsRule(this));
		//prefab = Resources.Load<GameObject>("WorldGenerators/Example/Rooms/BlueRoom/Prefab");
	}
}
