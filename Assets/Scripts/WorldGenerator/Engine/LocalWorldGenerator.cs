using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class LocalWorldGenerator : MonoBehaviour
{

	//public bool testtemp = false;
    //ToDo put to static when worldgenerators'll be read from Resources
    public static List<LocalWorldGenerator> worldGenerators;

    public static LocalWorldGenerator Create(int seed)
    {
        if (worldGenerators == null)
        {
            LoadGenerators();
        }

        UnityEngine.Random.InitState(seed);
        LocalWorldGenerator newLWG = Instantiate<LocalWorldGenerator>(worldGenerators[UnityEngine.Random.Range(1, worldGenerators.Count) - 1]);
        UnityEngine.Random.InitState(seed);
        newLWG.InitializeRoomList();
        List<Room> copyRooms = new List<Room>(newLWG.rooms);
        //int i = 1;
        //int whichSide = UnityEngine.Random.Range(0, 4);
        //switch (whichSide)
        //{
        //    case 0:

        //    default:
        //        break;
        //}
        newLWG.startPosition = new Vector3(UnityEngine.Random.Range(1, newLWG.xWorldSize - 2), 0, UnityEngine.Random.Range(1, newLWG.yWorldSize - 2));
        //newLWG.startPosition = new Vector3(1, 0, 1);
        do
        {
            //newLWG.exitPosition = new Vector3(newLWG.xWorldSize - 2, 0, newLWG.yWorldSize - 2);
            newLWG.exitPosition = new Vector3(UnityEngine.Random.Range(1, newLWG.xWorldSize - 2), 0, UnityEngine.Random.Range(1, newLWG.yWorldSize - 2));
        } while (newLWG.exitPosition == newLWG.startPosition);
        newLWG.localWorld = new Dictionary<Vector3, Room>();
        newLWG.GenerateCSP();
        newLWG.BacktrackingSearch();
        return newLWG;
    }

    private static void LoadGenerators()
    {

        worldGenerators = new List<LocalWorldGenerator>(Resources.Load<GameObject>("WorldGenerators/LWGList").GetComponent<LWGList>().list);
    }

    private bool BacktrackingSearch()
    {
        Dictionary<Vector3, Room> result = RecursiveBacktracking(new Dictionary<Vector3, Room>(localWorld), csp);
        if (result.Count > 0)
        {
            localWorld = result;
            return true;
        }
        return false;
    }

    private Dictionary<Vector3, Room> RecursiveBacktracking(Dictionary<Vector3, Room> assignment, Dictionary<Vector3, List<Room>> csp)
    {
        //SetAccessibility(ref csp);
        AC3.Execute(ref csp);
		if (CheckAssignment(assignment))//,ref csp))
        {
            return assignment;
        }
        if (HasNullValue(csp))
        {
            //return new Dictionary<Vector3, List<Room>>();
            return new Dictionary<Vector3, Room>();
        }
        Vector3 variable = SelectUnassignedVariable(assignment);



		//debug
		/*if(testtemp){
			Debug.Log (variable);
			testtemp = false;
		}*/
			
        if (variable == Vector3.up)
        {
            Debug.Log("No variable found during the recursivebacktracking");
            //return new Dictionary<Vector3, List<Room>>();
            return new Dictionary<Vector3, Room>();

        }
        IEnumerable<Room> sortedUnassignedValues = OrderDomainValues(variable);
        foreach (Room value in sortedUnassignedValues)
        {
            //Consistent thanks to AC3
            //if (!assignment.ContainsKey(variable))
            //    assignment.Add(variable, new List<Room>());
            //assignment[variable].Add(value);
            assignment.Add(variable, value);
            List<Room> tmp = new List<Room>(csp[variable]);
            csp[variable].Clear();
            csp[variable].Add(value);
            Dictionary<Vector3, Room> result = RecursiveBacktracking(assignment, csp);
            if (result.Count > 0)
            {
                return result;
            }
            assignment.Remove(variable);
            csp[variable] = tmp;
        }
        return new Dictionary<Vector3, Room>();
        //return new Dictionary<Vector3, List<Room>>();
    }
    private void ResetAllAccessibility(ref Dictionary<Vector3, List<Room>> csp)
    {
        foreach (Vector3 key in csp.Keys)
        {
            for (int i = 0; i < csp[key].Count; i++)
            {
                if (csp[key][i] is GenericMazeZone)
                {
                    (csp[key][i] as GenericMazeZone).linkedToExit = (csp[key][i] as GenericMazeZone).isExit;
                    (csp[key][i] as GenericMazeZone).linkedToStart = (csp[key][i] as GenericMazeZone).isStart;
                }
            }

        }
    }

    private void SetAccessibility(ref Dictionary<Vector3, List<Room>> csp)
    {
        ResetAllAccessibility(ref csp);
        HashSet<Vector3> tmp = new HashSet<Vector3>();
        SetStartAccessibility(ref csp, startPosition, ref tmp);
        tmp.Clear();
        SetExitAccessibility(ref csp, exitPosition, ref tmp);
    }

    private void SetStartAccessibility(ref Dictionary<Vector3, List<Room>> csp, Vector3 position, ref HashSet<Vector3> alreadySet)
    {
        HashSet<Room> linkedToStart = new HashSet<Room>();
        List<Room> currentRooms = new List<Room>();
        if (!csp.TryGetValue(position, out currentRooms))
            return;
        HashSet<Vector3> nextRooms = new HashSet<Vector3>();
        foreach (GenericMazeZone room in currentRooms)
        {
            room.linkedToStart = true;
            alreadySet.Add(room.position);
            List<Room> tmp = new List<Room>();
            if (room.leftSideOpen)
                if (csp.TryGetValue(room.position + Vector3.left, out tmp))
                    if (tmp.Exists(x => (x as GenericMazeZone).rightSideOpen && !(x as GenericMazeZone).linkedToStart))
                        nextRooms.Add(room.position + Vector3.left);
            if (room.rightSideOpen)
                if (csp.TryGetValue(room.position + Vector3.right, out tmp))
                    if (tmp.Exists(x => (x as GenericMazeZone).leftSideOpen && !(x as GenericMazeZone).linkedToStart))
                        nextRooms.Add(room.position + Vector3.right);
            if (room.backOpen)
                if (csp.TryGetValue(room.position + Vector3.back, out tmp))
                    if (tmp.Exists(x => (x as GenericMazeZone).forwardOpen && !(x as GenericMazeZone).linkedToStart))
                        nextRooms.Add(room.position + Vector3.back);
            if (room.forwardOpen)
                if (csp.TryGetValue(room.position + Vector3.forward, out tmp))
                    if (tmp.Exists(x => (x as GenericMazeZone).backOpen && !(x as GenericMazeZone).linkedToStart))
                        nextRooms.Add(room.position + Vector3.forward);
        }
        HashSet<Vector3> toSet = new HashSet<Vector3>(nextRooms);
        toSet.ExceptWith(alreadySet);
        alreadySet.UnionWith(toSet);
        foreach (Vector3 room in toSet)
        {
            SetStartAccessibility(ref csp, room, ref alreadySet);
        }
    }

    private void SetExitAccessibility(ref Dictionary<Vector3, List<Room>> csp, Vector3 position, ref HashSet<Vector3> alreadySet)
    {
        HashSet<Room> linkedToExit = new HashSet<Room>();
        List<Room> currentRooms = new List<Room>();
        if (!csp.TryGetValue(position, out currentRooms))
            return;
        HashSet<Vector3> nextRooms = new HashSet<Vector3>();
        foreach (GenericMazeZone room in currentRooms)
        {
            room.linkedToExit = true;
            alreadySet.Add(room.position);
            List<Room> tmp = new List<Room>();
            if (room.leftSideOpen)
                if (csp.TryGetValue(room.position + Vector3.left, out tmp))
                    if (tmp.Exists(x => (x as GenericMazeZone).rightSideOpen && !(x as GenericMazeZone).linkedToExit))
                        nextRooms.Add(room.position + Vector3.left);
            if (room.rightSideOpen)
                if (csp.TryGetValue(room.position + Vector3.right, out tmp))
                    if (tmp.Exists(x => (x as GenericMazeZone).leftSideOpen && !(x as GenericMazeZone).linkedToExit))
                        nextRooms.Add(room.position + Vector3.right);
            if (room.backOpen)
                if (csp.TryGetValue(room.position + Vector3.back, out tmp))
                    if (tmp.Exists(x => (x as GenericMazeZone).forwardOpen && !(x as GenericMazeZone).linkedToExit))
                        nextRooms.Add(room.position + Vector3.back);
            if (room.forwardOpen)
                if (csp.TryGetValue(room.position + Vector3.forward, out tmp))
                    if (tmp.Exists(x => (x as GenericMazeZone).backOpen && !(x as GenericMazeZone).linkedToExit))
                        nextRooms.Add(room.position + Vector3.forward);
        }
        HashSet<Vector3> toSet = new HashSet<Vector3>(nextRooms);
        toSet.ExceptWith(alreadySet);
        alreadySet.UnionWith(toSet);
        foreach (Vector3 room in toSet)
        {
            SetExitAccessibility(ref csp, room, ref alreadySet);
        }
    }

    private bool HasNullValue(Dictionary<Vector3, List<Room>> csp)
    {
        foreach (List<Room> item in csp.Values)
        {
            if (item.Count < 1)
                return true;
        }
        return false;
    }

    private IEnumerable<Room> OrderDomainValues(Vector3 position)
    {
        List<CountedRoom> sortedRoom = new List<CountedRoom>();
        Queue<Arc> arcs = AC3.GetNeighbors(csp, position);
        List<Room> neighbors = new List<Room>();
        while (arcs.Count > 0)
        {
            neighbors.Concat<Room>(csp[arcs.Dequeue().roomJ]);
        }
        foreach (Room room in csp[position])
        {
            sortedRoom.Add(new CountedRoom(CountRoom(room.GetType(), neighbors), room));
        }
        sortedRoom.Sort();
        Queue<Room> output = new Queue<Room>();
        foreach (CountedRoom cr in sortedRoom)
        {
            output.Enqueue(cr.room);
        }
        return output;
    }

    private int CountRoom(Type type, List<Room> neighbors)
    {
        return neighbors.Count(d => d.GetType() == type);
    }

	private bool CheckAssignment(Dictionary<Vector3, Room> assignment)//, ref Dictionary<Vector3, List<Room>> csp)
    {

		return assignment.Count == csp.Count;

		/*if(assignment.Count == csp.Count){
			Vector3 exit = this.exitPosition;
			Debug.Log ("exit pos " + exit);
			ResetAllAccessibility (ref csp);
			Debug.Log("exit can access start " + ((GenericMazeZone)(csp[exit].First ())).linkedToStart);
			SetAccessibility (ref csp);
			Debug.Log("exit can access start after" + ((GenericMazeZone)(csp[exit].First ())).linkedToStart);

			if (csp [exit].Count == 1) {
				Debug.Log ("count ok");
				//testtemp = true;
				return ((GenericMazeZone)(csp[exit].First ())).linkedToStart;
			}
		}
        return false;*/
        //Minus 1 due to the center which is already assigned.
        //if (assignment.Count != csp.Count - 1)
        //{
        //    return false;
        //}
        //foreach (List<Room> room in assignment.Values)
        //{
        //    if (room.Count != 1)
        //        return false;
        //}
        //return true;
    }

	/*private bool isThereAWay(Vector3 pos, List<Vector3> border, ref Dictionary<Vector3, List<Room>> csp){
	}*/

    private Vector3 SelectUnassignedVariable(Dictionary<Vector3, Room> assignment)
    {
        List<Vector3> selectedKeys = new List<Vector3>();
        int maxValCount = int.MaxValue;
        foreach (Vector3 key in csp.Keys)
        {
            if (csp[key].Count <= maxValCount && !assignment.ContainsKey(key))
            {
                if (csp[key].Count < maxValCount)
                    selectedKeys.Clear();
                selectedKeys.Add(key);
                maxValCount = csp[key].Count;
            }
        }
        Vector3 selectedKey = Vector3.up;
        int maxConstraintCount = int.MinValue;
        foreach (Vector3 key in selectedKeys)
        {
            int nbConstraint = AC3.GetNeighbors(csp, key).Count;
            if (nbConstraint > maxConstraintCount)
            {
                selectedKey = key;
                maxConstraintCount = nbConstraint;
            }

        }
        return selectedKey;
    }

    private void GenerateCSP()
    {
        csp = new Dictionary<Vector3, List<Room>>();
        List<Room> tmp = new List<Room>();
        for (int i = 0; i < xWorldSize; i++)
        {
            for (int j = 0; j < yWorldSize; j++)
            {
                Vector3 currentPos = new Vector3(i, 0, j);
                if (i == 0 || j == 0 || i == xWorldSize - 1 || j == yWorldSize - 1)
                {
                    tmp = new List<Room>();
                    Room wall = new Wall();
                    tmp.Add(wall.GetCopy(currentPos));
                }
                else
                    tmp = generateRoomsCopy(currentPos);
                if (i == startPosition.x && j == startPosition.z)
                {
                    tmp = tmp.FindAll(x => x.canBeFirst == true);
                    for (int k = 0; k < tmp.Count; k++)
                    {
                        (tmp[k] as GenericMazeZone).isStart = true;
                    }
                }
                if (i == exitPosition.x && j == exitPosition.z)
                {
                    tmp = tmp.FindAll(x => x.canBeFirst == true);
                    for (int k = 0; k < tmp.Count; k++)
                    {
                        (tmp[k] as GenericMazeZone).isExit = true;
                    }
                }
                //tmp = generateRoomsCopy(-currentPos);
                csp.Add(currentPos, tmp);
            }
        }
    }

    private List<Room> generateRoomsCopy(Vector3 atPosition)
    {
        List<Room> rooms = new List<Room>();
        foreach (Room room in this.rooms)
        {
            rooms.Add(room.GetCopy(atPosition));
        }
        return rooms;
    }

    public int xWorldSize;
    public int yWorldSize;
    public float roomSize;
    public List<Room> rooms;
    public Dictionary<Vector3, Room> localWorld;
    public Dictionary<Vector3, List<Room>> csp;
    public Vector3 startPosition;
    public Vector3 exitPosition;

    //public int partialCounter;
    //public int partialMax;
    //public float partialThreshold;

    protected abstract void InitializeRoomList();

    private class CountedRoom : IComparable
    {
        public Room room;
        public int count;
        public CountedRoom(int count, Room room)
        {
            this.room = room;
            this.count = count;
        }

        public int CompareTo(object obj)
        {
            int toReturn = ((obj as CountedRoom).count - this.count);
            if (toReturn == 0)
            {
                return UnityEngine.Random.Range(-1, 2);
            }
            return toReturn;
        }
    }
}