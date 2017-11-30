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
		foreach(Agent a in MesoInSight(p.AgentsInSight)){
			myGroup.Group[a] = a.transform.position;
		}
	}

	public void NextIndicationToFollow(Perception p){
		List<GameObject> indicationsInSight = p.IndicationsInSight;
		if(indicationsInSight.Count > 0){
			System.Random rand = new System.Random();
			indication = indicationsInSight[rand.Next(0, indicationsInSight.Count)].transform.position;
		}
	}

	public void UpdatePath(Perception p){

	}

	public List<Agent> MesoInSight(List<Agent> agentsInSight){
		List<Agent> mesoInSight = new List<Agent>();
		foreach(Agent a in myGroup.Group.Keys){
			foreach(Agent b in agentsInSight){
				if(a.Equals(b)){
					mesoInSight.Add(a);
					break;
				}
			}
		}
		return mesoInSight;
	}
}
