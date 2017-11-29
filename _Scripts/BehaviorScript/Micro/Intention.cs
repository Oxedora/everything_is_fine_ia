using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class AgentAllowed : System.Attribute {
	protected Type agentType;
	public Type AgentType {
		get { 
			return agentType; 
		}
	}

	public AgentAllowed(Type at) {
		agentType = at;
	}
}

public abstract class Intention {

	// Next state to execute
	public delegate Vector3 ActionDelegate();

	private ActionDelegate currentState;
	public ActionDelegate CurrentState {
		get {
			return currentState;
		}
	}

	// Use this for initialization
	public Intention() {
		currentState = DefaultState;
	}
	
	// Update is called once per frame
	private Vector3 Update (Agent myAgent, Perception p) {
		Vector3 dir = Reflexes(myAgent, p);
		return (dir.magnitude > 0 ? dir : currentState());
	}

	private Vector3 DefaultState() {return Vector3.zero;}

	// Check agent reflexes and adapt accordingly
	private Vector3 Reflexes(Agent myAgent, Perception p) {
		Vector3 reflexeDir = Vector3.zero;
		if(p.FireInSight.Count > 0){
			reflexeDir = DodgeObjects(myAgent, p.FireInSight);
		}
		else if(p.IndicationsInSight.Count > 0){
			reflexeDir = DodgeObjects(myAgent, p.IndicationsInSight);
		}
		return reflexeDir;
	}

	// Returns the Vector3 to dodge GameObjects of the given list
	private Vector3 DodgeObjects(Agent myAgent, List<GameObject> objectsToDodge){
		Vector3 sep = Vector3.zero;

		foreach(GameObject go in objectsToDodge){
			sep += myAgent.transform.position - go.transform.position;
		}
        sep.y = 0;
		return (sep.magnitude > 1.0f ? sep.normalized : sep);
	}

	// Returns the Vector3 position of the closest indication to follow
	private Vector3 FollowIndication(Agent myAgent, List<GameObject> indicationToFollow){
		float closest = Vector3.Distance(myAgent.transform.position, indicationToFollow[0].transform.position);
		Vector3 indic =  indicationToFollow[0].transform.position;
		foreach(GameObject go in indicationToFollow){
			float dist = Vector3.Distance(myAgent.transform.position, go.transform.position);
			if(dist < closest){
				closest = dist;
				indic = go.transform.position;
			}
		}
		return indic;
	}
}
