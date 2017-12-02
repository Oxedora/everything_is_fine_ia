using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[AgentAllowed(typeof(Adult))]
[AgentAllowed(typeof(Elder))]
[AgentAllowed(typeof(Worker))]
public class GetOut : Intention {
	private GameObject cpTarget = null;
	public GameObject CpTarget {
		get {
			return cpTarget;
		}
	}

	public GetOut() : base() {}


	public Vector3 DefaultState(Agent agent){
		if(agent.OnCheckpoint){
			SelectCheckPoint(agent);
		}
		else if(cpTarget.Equals(null) && agent.Bdi.myPerception.IndicationsInSight.Count > 0){
			return FollowIndication(agent.transform.position, agent.Bdi.myPerception.IndicationsInSight[0]);
		}
		
		return (cpTarget.Equals(null) ? Vector3.zero : cpTarget.transform.position - agent.transform.position);
	}

	private void SelectCheckPoint(Agent agent){
		if(agent.Bdi.myPerception.IndicationsInSight.Count > 0){
			agent.transform.rotation = Quaternion.LookRotation(agent.Bdi.myPerception.IndicationsInSight[0].transform.TransformDirection(Vector3.forward));
		}
		List<GameObject> cpInSight = agent.Bdi.myPerception.getGameObjectsInSight(agent, agent.Settings.CheckpointMask);
		GameObject cpToGo = null;
		int i = 0;
		while(i < cpInSight.Count && cpToGo.Equals(null)){
			if(!agent.CheckedPoints.Keys.Contains(cpInSight[i])){
				cpToGo = cpInSight[i];
			}
		}

		if(!cpToGo.Equals(null)){
			cpTarget = cpToGo;
		}
		else {
			int prio = Int32.MaxValue;
			foreach(GameObject go in cpInSight){
				if(agent.CheckedPoints.Keys.Contains(go) && agent.CheckedPoints[go] < prio){
					cpToGo = go;
					prio = agent.CheckedPoints[go];
				}
			}
			if(!cpToGo.Equals(null)){cpTarget = cpToGo;}
		}

		agent.OnCheckpoint = false;
	}

	private Vector3 FollowIndication(Vector3 position, GameObject indication){
		return (Vector3.Distance(position, indication.transform.position) < 10 ? // if i am close enough
			(Vector3) indication.transform.TransformDirection(Vector3.forward) : // i follow the indication
			indication.transform.position - position); // otherwise i get closer
	} 
}
