﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsLinkedToExitRule : RoomRule
{
    public IsLinkedToExitRule(Room attachedRoom) : base(attachedRoom)
    {
        constainedRooms.Add(Vector3.zero);
    }

    public IsLinkedToExitRule(RoomRule rule, Room newSelf) : base(rule, newSelf)
    {
    }

    public override RoomRule GetCopy(Room room)
    {
        return new IsLinkedToExitRule(this, room);
    }

    public override bool isAdmissible(Room other)
    {
        GenericMazeZone zone = (GenericMazeZone)(this.self);
        return (zone.isExit || zone.linkedToExit);
    }
}


