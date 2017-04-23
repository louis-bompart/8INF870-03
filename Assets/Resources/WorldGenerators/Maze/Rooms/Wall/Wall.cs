using UnityEngine;
using System.Collections;

public class Wall : GenericMazeZone
{

    public Wall()
    {
        this.setOrientation();
    }

    public Wall(Room room, Vector3 position) : base(room, position)
    {
        this.setOrientation();
    }

    protected override void setOrientation()
    {
        this.leftSideOpen = false;
        this.rightSideOpen = false;
        this.forwardOpen = false;
        this.backOpen = false;
    }

    public override Room GetCopy(Vector3 position)
    {
        return new Wall(this, position);
    }

    protected override void Initialize()
    {
        base.Initialize();
        //rules.Add(new CorrespondingDirectionsRule(this));
        prefab = Resources.Load<GameObject>("WorldGenerators/Maze/Rooms/Wall/Prefab");
    }

}

