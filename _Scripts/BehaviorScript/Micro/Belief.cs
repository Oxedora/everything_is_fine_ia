using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belief {

	private MesoGroup myGroup;
	public MesoGroup MyGroup {
		get {
			return myGroup;
		}
	}

	private Dictionary<Agent, Vector3> positionsOfMesoGroup;
	public Dictionary<Agent, Vector3> PositionsOfMesoGroup {
		get {
			return positionsOfMesoGroup;
		}
	}

	// sous quelle forme stocker l'indication de la sortie ?
	private Vector3 indication;
	public Vector3 Indication {
		get {
			return indication;
		}
	}

	private List<Node> path;
	public List<Node> Path {
		get {
			return path;
		}
	}

	public Belief(MesoGroup mg, Vector3 i, List<Node> p){
		myGroup = mg;
		foreach(Agent a in myGroup.Group){
			positionsOfMesoGroup.Add(a, a.transform.position);
		}
		indication = i;
		path = p;
	}

	// Update is called once per frame
	public void Update (Perception p) {
		UpdateMesoPosition(p);
		NextIndicationToFollow(p);
		UpdatePath(p);
	}

	public void UpdateMesoPosition(Perception p){
		List<Agent> anyMesoToUpdate = myGroup.Intersect(p.AgentsInSight);
		foreach(Agent a in anyMesoToUpdate){
			positionsOfMesoGroup(a) = a.transform.position;
		}
	}

	public void NextIndicationToFollow(Perception p){
		List<GameObject> indicationsInSight = p.IndicationsInSight;
		if(indicationsInSight.Count > 0){
			System.Random rand = new System.Random();
			indication = indicationsInSight(rand.Next(0, indicationsInSight.Count));
		}
	}

	public void UpdatePath(Perception p){

	}
}
