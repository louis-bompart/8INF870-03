using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowRoom : Room
{
    public YellowRoom()
    {
    }

    public YellowRoom(Room room, Vector3 position) : base(room, position) { }

    public override Room GetCopy(Vector3 position)
    {
        return new YellowRoom(this, position);
    }

    protected override void Initialize()
    {
        base.Initialize();
        rules.Add(new NoSameNeighborRule(this));
        canBeFirst = true;
        prefab = Resources.Load<GameObject>("WorldGenerators/Example/Rooms/YellowRoom/Prefab");

    }
}