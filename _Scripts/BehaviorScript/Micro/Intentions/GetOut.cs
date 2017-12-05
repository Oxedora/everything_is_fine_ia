using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[AgentAllowed(typeof(Agent))]
[AgentAllowed(typeof(Adult))]
[AgentAllowed(typeof(Elder))]
[AgentAllowed(typeof(Worker))]
public class GetOut : Intention {

	public GetOut() : base() {}

	public override Vector3 DefaultState(Agent agent){
        
        // If i'm on a checkpoint, i choose another to go if any
		if(agent.Bdi.myBelief.OnCheckpoint){
			SelectCheckPoint(agent);
		}
        // Else i follow the indications if any
		else if(agent.Bdi.myBelief.CpTarget == null && agent.Bdi.myPerception.IndicationsInSight.Count > 0){
			return FollowIndication(agent.transform.position, agent.Bdi.myPerception.IndicationsInSight[0]);
		}
		
		return (agent.Bdi.myBelief.CpTarget  == null ? agent.transform.TransformDirection(Vector3.zero) : (agent.Bdi.myBelief.CpTarget.transform.position - agent.transform.position));
	}

	private void SelectCheckPoint(Agent agent){
        // Removing old checkpoint target
        agent.Bdi.myBelief.CpTarget = null;
        // If i see an indications, i turn in the direction indicated
        if (agent.Bdi.myPerception.IndicationsInSight.Count > 0){
            //agent.transform.rotation = Quaternion.LookRotation(agent.Bdi.myPerception.IndicationsInSight[0].transform.TransformDirection(Vector3.forward));
            agent.transform.rotation = Quaternion.LookRotation(FollowIndication(agent.transform.position, agent.Bdi.myPerception.IndicationsInSight[0]));

        }

        // Getting the checkpoints in sight
		List<GameObject> cpInSight = agent.Bdi.myPerception.getGameObjectsInSight(agent, agent.Settings.CheckpointMask);

        // Removing the checkpoint i'm on if i see him
        if(cpInSight.Contains(agent.Bdi.myBelief.OnCheckpoint))
        {
            cpInSight.Remove(agent.Bdi.myBelief.OnCheckpoint);
        }

        // Select the first checkpoint in sight that i didn't already crossed
        GameObject cpToGo = null;
		int i = 0;
		while(i < cpInSight.Count && cpToGo == null){
			if(!agent.Bdi.myBelief.CheckedPoints.Keys.Contains(cpInSight[i])){
				cpToGo = cpInSight[i];
			}
            i++;
		}


		if(!(cpToGo == null)){
            agent.Bdi.myBelief.CpTarget  = cpToGo;
		}
        // If i already crossed every checkpoint in sight, i take the one i crossed less times
		else {
			int prio = Int32.MaxValue;
			foreach(GameObject go in cpInSight){
				if(agent.Bdi.myBelief.CheckedPoints.Keys.Contains(go) && agent.Bdi.myBelief.CheckedPoints[go] < prio){
					cpToGo = go;
					prio = agent.Bdi.myBelief.CheckedPoints[go];
				}
			}
			if(!(cpToGo == null)){ agent.Bdi.myBelief.CpTarget = cpToGo;}
		}
        
        // Reseting the agent OnCheckpoint
		agent.Bdi.myBelief.OnCheckpoint = null;
	}

	private Vector3 FollowIndication(Vector3 position, GameObject indication){
		return (Vector3.Distance(position, indication.transform.position) < 5 ? // if i am close enough
			(Vector3) indication.transform.TransformDirection(Vector3.forward) : // i follow the indication
			indication.transform.position - position); // otherwise i get closer
	}

    public override void UpdatePriority(BDI bdi)
    {
        Priority = bdi.myFeelings.Fear;
    }
}
