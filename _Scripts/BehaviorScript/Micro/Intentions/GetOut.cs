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


	public override Vector3 DefaultState(Agent agent){
        Debug.Log("cpTarget state : " + cpTarget);
		if(agent.OnCheckpoint){
			SelectCheckPoint(agent);
		}
		else if(cpTarget == null && agent.Bdi.myPerception.IndicationsInSight.Count > 0){
			return FollowIndication(agent.transform.position, agent.Bdi.myPerception.IndicationsInSight[0]);
		}
		
		return (cpTarget == null ? Vector3.zero : (cpTarget.transform.position - agent.transform.position));
	}

	private void SelectCheckPoint(Agent agent){
        Debug.Log("Selecting a checkpoint");
        cpTarget = null;
		if(agent.Bdi.myPerception.IndicationsInSight.Count > 0){
			agent.transform.rotation = Quaternion.LookRotation(agent.Bdi.myPerception.IndicationsInSight[0].transform.TransformDirection(Vector3.forward));
		}
		List<GameObject> cpInSight = agent.Bdi.myPerception.getGameObjectsInSight(agent, agent.Settings.CheckpointMask);

        float minDist = float.MaxValue;
        GameObject cpWhereImIn = null;

        foreach(GameObject go in cpInSight)
        {
            float dist = Vector3.Distance(agent.transform.position, go.transform.position);
            if(dist < minDist)
            {
                minDist = dist;
                cpWhereImIn = go;
            }
        }
        if(cpWhereImIn != null)
        {
            cpInSight.Remove(cpWhereImIn);
        }

        GameObject cpToGo = null;
		int i = 0;
		while(i < cpInSight.Count && cpToGo == null){
			if(!agent.CheckedPoints.Keys.Contains(cpInSight[i])){
				cpToGo = cpInSight[i];
			}
		}

		if(!(cpToGo == null)){
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
			if(!(cpToGo == null)){cpTarget = cpToGo;}
		}

        if (!(cpToGo == null)) { Debug.Log("Going to " + cpToGo.name); }
		agent.OnCheckpoint = false;
	}

	private Vector3 FollowIndication(Vector3 position, GameObject indication){
        Debug.Log("Following position");
		return (Vector3.Distance(position, indication.transform.position) < 5 ? // if i am close enough
			(Vector3) indication.transform.TransformDirection(Vector3.forward) : // i follow the indication
			indication.transform.position - position); // otherwise i get closer
	} 
}
