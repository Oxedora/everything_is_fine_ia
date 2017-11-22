using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public List<Agent> getAgentsInSight(){
		List<Agent> agentsInSight = new List<Agent>();
		return agentsInSight;
	}

	public List<Agent> getAgentsTooClose(){
		List<Agent> agentsTooClose = new List<Agent>();
		return agentsTooClose;
	}
}
