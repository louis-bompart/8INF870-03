using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsLinkedToStartRule : RoomRule
{
    public IsLinkedToStartRule(Room attachedRoom) : base(attachedRoom)
    {
        constainedRooms.Add(Vector3.zero);
    }

    public IsLinkedToStartRule(RoomRule rule, Room newSelf) : base(rule, newSelf)
    {
    }

    public override RoomRule GetCopy(Room room)
    {
        return new IsLinkedToStartRule(this, room);
    }

    public override bool isAdmissible(Room other)
    {
        GenericMazeZone zone = (GenericMazeZone)(this.self);
        return (zone.isStart || zone.linkedToStart);
    }
}


