﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrespondingDirectionsRule : RoomRule
{
	public CorrespondingDirectionsRule(Room attachedRoom) : base(attachedRoom)
	{
		constainedRooms.Add(Vector3.left);
		constainedRooms.Add(Vector3.right);
		constainedRooms.Add(Vector3.forward);
		constainedRooms.Add(Vector3.back);
	}

	public CorrespondingDirectionsRule(RoomRule rule, Room newSelf) : base(rule, newSelf)
	{
	}

	public override RoomRule GetCopy(Room room)
	{
		return new CorrespondingDirectionsRule(this, room);
	}

	public override bool isAdmissible(Room other)
	{
		GenericMazeZone otherZone = (GenericMazeZone)other;
		GenericMazeZone zone = (GenericMazeZone)(this.self);

		Vector3 neighbor = otherZone.position - zone.position;

		if (neighbor == Vector3.left)
			return (zone.leftSideOpen == otherZone.rightSideOpen);

		if (neighbor == Vector3.right)
			return (zone.rightSideOpen == otherZone.leftSideOpen);
		
		if (neighbor == Vector3.forward)
			return (zone.forwardOpen == otherZone.backOpen);
		
		if (neighbor == Vector3.back)
			return (zone.backOpen == otherZone.forwardOpen);
        //Debug.Log ("this case is not a neighbor");
        return false;
	}
}


