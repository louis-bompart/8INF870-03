using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LWGMaze : LocalWorldGenerator
{
    protected override void InitializeRoomList()
    {

        this.rooms = new List<Room>();
        rooms.Add(new DeadEndRight());
        rooms.Add(new DeadEndLeft());
        rooms.Add(new DeadEndForward());
        rooms.Add(new DeadEndBack());
        rooms.Add(new HorizontalLine());
        rooms.Add(new VerticalLine());
        rooms.Add(new Cross());
        rooms.Add(new Wall());
        rooms.Add(new BRCorner());
        rooms.Add(new FRCorner());
        rooms.Add(new FLCorner());
        rooms.Add(new BLCorner());
        rooms.Add(new FBLIntersection());
        rooms.Add(new BRLIntersection());
        rooms.Add(new FRLIntersection());
        rooms.Add(new FBRIntersection());
    }

    protected void Start()
    {
		linkStartToExit (ref localWorld);
        foreach (Vector3 key in localWorld.Keys)
        {
			if (((GenericMazeZone)localWorld [key]).isStart) {
				Debug.Log ("dep " + key);
				GameObject prefabDepArr = Resources.Load<GameObject>("WorldGenerators/Maze/Rooms/depArr");
				Instantiate(prefabDepArr, key * roomSize, localWorld[key].prefab.transform.rotation, transform.parent).tag = "Respawn";
				continue;
			}
			if (((GenericMazeZone)localWorld [key]).isExit) {
				Debug.Log ("arr " + key);
				GameObject prefabDepArr = Resources.Load<GameObject>("WorldGenerators/Maze/Rooms/depArr");
				Instantiate(prefabDepArr, key * roomSize, localWorld[key].prefab.transform.rotation, transform.parent).tag = "Finish";
				continue;
			}
            Instantiate(localWorld[key].prefab, key * roomSize, localWorld[key].prefab.transform.rotation, transform.parent);
        }
        Debug.Log("A World of " + localWorld.Count + " cases has been generated in" + Time.realtimeSinceStartup + "s.");

		this.finishInitialize = true;
    }



	private void linkStartToExit(ref Dictionary<Vector3,Room> world){

		//identifier les deux amas
		List<Vector3> borderE = new List<Vector3>();
		borderE.Add(this.exitPosition);
		//border.Add(this.startPosition);
		List<Vector3> clusterE = new List<Vector3> ();
		clusterE.Add(this.exitPosition);
		//cluster.Add(this.startPosition);
		List<Vector3> borderS = new List<Vector3>();
		borderS.Add(this.startPosition);
		List<Vector3> clusterS = new List<Vector3> ();
		clusterS.Add(this.startPosition);

		List<Vector3> clusterStart = clusteriser(borderS, clusterS, world);
		List<Vector3> clusterExit = clusteriser(borderE, clusterE, world);

		//si reliés, fini, sinon
		if (clusterStart.Contains (this.exitPosition))
			return;

		//choisir la paire de vecteurs les plus proches
		Vector3 pStart = new Vector3();
		Vector3 pExit = new Vector3();
		float minDist = this.xWorldSize * this.yWorldSize;
		float tmpDist;

		foreach (Vector3 testStart in clusterStart) {
			if (testStart == this.startPosition)
				continue;
			foreach (Vector3 testExit in clusterExit) {
				if (testExit == this.exitPosition)
					continue;
				tmpDist = (testStart - testExit).sqrMagnitude;
				if (tmpDist <= minDist) {
					pStart = testStart;
					pExit = testExit;
					minDist = tmpDist;
				}
			}
		}

		//Debug.Log ("best start " + pStart);
		//Debug.Log ("best exit " + pExit);

		//creuser horizontalement puis verticalement
		//prendre la case a creuser, creer l ensemble des bool d ouverture
		int horizontalDiff = (int)(pExit.x - pStart.x);
		int heigh = (int)pStart.z;
		if (horizontalDiff != 0) {

			for (int hor = Mathf.Min((int)pStart.x,(int)pExit.x); hor <= Mathf.Max((int)pStart.x,(int)pExit.x); hor++) {
				GenericMazeZone curZone = (GenericMazeZone)(world [new Vector3 (hor, 0.0f, heigh)]);

				bool back = curZone.backOpen;
				bool forward = curZone.forwardOpen;
				bool right = true;//curZone.rightSideOpen;
				bool left = true;//curZone.leftSideOpen;
			
				if (horizontalDiff > 0) {
					if (hor == pStart.x) {
						left = curZone.leftSideOpen;
					} 
					if (hor == pExit.x) {
						right = curZone.rightSideOpen;
					}
				} else {
					if (hor == pStart.x) {
						right = curZone.rightSideOpen;
					} 
					if (hor == pExit.x) {
						left = curZone.leftSideOpen;
					}
				}
				/*Debug.Log ("cur x " + hor);
				Debug.Log ("forward " + forward);
				Debug.Log ("right " + right);
				Debug.Log ("left " + left);
				Debug.Log ("back " + back);*/
				// comparer a l ensemble des cases disponibles, prendre une copie du bon type
				Room toInstanciate = new Room();
				foreach(GenericMazeZone type in this.rooms){
					if ((type.rightSideOpen == right) && (type.leftSideOpen == left) && (type.backOpen == back) && (type.forwardOpen == forward)) {
						toInstanciate = type.GetCopy (curZone.position);
						((GenericMazeZone)toInstanciate).isExit = curZone.isExit;
						((GenericMazeZone)toInstanciate).isStart = curZone.isStart;
					}
				}

				world.Remove (toInstanciate.position);
				world.Add (toInstanciate.position, toInstanciate);

			}
		}
		int verticalDiff = (int)(pExit.z - pStart.z);
		int abs = (int)pExit.x;
		if (verticalDiff != 0) {

			for (int ver = Mathf.Min((int)pStart.z,(int)pExit.z); ver <= Mathf.Max((int)pStart.z,(int)pExit.z); ver++) {
				GenericMazeZone curZone = (GenericMazeZone)(world [new Vector3 (abs, 0.0f, ver)]);

				bool back = true;
				bool forward = true;
				bool right = curZone.rightSideOpen;
				bool left = curZone.leftSideOpen;

				if (verticalDiff > 0) {
					if (ver == pStart.z) {
						back = curZone.backOpen;
					} 
					if (ver == pExit.x) {
						forward = curZone.forwardOpen;
					}
				} else {
					if (ver == pStart.x) {
						forward = curZone.forwardOpen;
					} 
					if (ver == pExit.x) {
						back = curZone.backOpen;
					}
				}
				// comparer a l ensemble des cases disponibles, prendre une copie du bon type
				Room toInstanciate = new Room();
				foreach(GenericMazeZone type in this.rooms){
					if ((type.rightSideOpen == right) && (type.leftSideOpen == left) && (type.backOpen == back) && (type.forwardOpen == forward)) {
						toInstanciate = type.GetCopy (curZone.position);
						((GenericMazeZone)toInstanciate).isExit = curZone.isExit;
						((GenericMazeZone)toInstanciate).isStart = curZone.isStart;
					}
				}

				world.Remove (toInstanciate.position);
				world.Add (toInstanciate.position, toInstanciate);

			}
		}	

	}

	private List<Vector3> clusteriser(List<Vector3> border, List<Vector3> cluster, Dictionary<Vector3,Room> world){

		if (border.Count == 0)
			return cluster;

		List<Vector3> newBorder = new List<Vector3> ();
		//List<Vector3> addToCluster = new List<Vector3> ();

		foreach (Vector3 curPos in border) {

			GenericMazeZone curZone = (GenericMazeZone)(world[curPos]);
			IEnumerable<Vector3> neiborhood = curZone.rules.First ().GetConstrainedPositions ();
			foreach (Vector3 curNeighborPos in neiborhood) {
				Vector3 curNeighborActualPos = curPos + curNeighborPos;

				//si le voisin existe
				if (world.ContainsKey (curNeighborActualPos)) {
					GenericMazeZone neighbor = (GenericMazeZone)(world[curNeighborActualPos]);

					//si il est relie
					if (curNeighborPos == Vector3.left) {
						if (curZone.leftSideOpen == neighbor.rightSideOpen && curZone.leftSideOpen) {

							//si il n est pas deja dans l amas
							if ((!cluster.Contains (curNeighborActualPos)) && (!newBorder.Contains(curNeighborActualPos))) {
								newBorder.Add (curNeighborActualPos);
							}

						}
					}

					if (curNeighborPos == Vector3.right){
						if (curZone.rightSideOpen == neighbor.leftSideOpen && curZone.rightSideOpen) {

							//si il n est pas deja dans l amas
							if ((!cluster.Contains (curNeighborActualPos)) && (!newBorder.Contains(curNeighborActualPos))) {
								newBorder.Add (curNeighborActualPos);
							}

						}
					}

					if (curNeighborPos == Vector3.forward){
						if (curZone.forwardOpen == neighbor.backOpen && curZone.forwardOpen) {

							//si il n est pas deja dans l amas
							if ((!(cluster.Contains(curNeighborActualPos))) && (!newBorder.Contains(curNeighborActualPos))) {
								newBorder.Add (curNeighborActualPos);
							}

						}
					}

					if (curNeighborPos == Vector3.back){
						if (curZone.backOpen == neighbor.forwardOpen && curZone.backOpen) {

							//si il n est pas deja dans l amas
							if ((!cluster.Contains (curNeighborActualPos)) && (!newBorder.Contains(curNeighborActualPos))) {
								newBorder.Add (curNeighborActualPos);
							}

						}
					}
						
				}
			}
		}

		List<Vector3> curCluster = new List<Vector3> (cluster);
		//Debug.Log ((curCluster.Concat(newBorder)).GetType ());
		curCluster = (List<Vector3>)(curCluster.Concat<Vector3>(newBorder).ToList());
		return (List<Vector3>)((clusteriser(newBorder, curCluster, world)).ToList());

	}
}


