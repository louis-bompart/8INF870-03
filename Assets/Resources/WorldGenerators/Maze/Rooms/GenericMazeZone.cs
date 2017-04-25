using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericMazeZone : Room
{

    public bool leftSideOpen;
    public bool rightSideOpen;
    public bool forwardOpen;
    public bool backOpen;
    public bool isExit;
    public bool isStart;
    public bool linkedToExit;
    public bool linkedToStart;

    public GenericMazeZone()
    {
    }

    public GenericMazeZone(Room room, Vector3 position) : base(room, position)
    {
        //isExit = (room as GenericMazeZone).isExit;
        //isStart = (room as GenericMazeZone).isStart;
    }

    protected abstract void setOrientation();

    /*public override Room GetCopy(Vector3 position)
	{
		return new GenericMazeZone(this, position);
	}*/

    protected override void Initialize()
    {
        base.Initialize();
        linkedToExit = false;
        linkedToExit = false;
        isStart = false;
        isExit = false;
        rules.Add(new CorrespondingDirectionsRule(this));
        //rules.Add(new IsLinkedToExitRule(this));
        //rules.Add(new IsLinkedToStartRule(this));
        //prefab = Resources.Load<GameObject>("WorldGenerators/Example/Rooms/BlueRoom/Prefab");
    }
}
