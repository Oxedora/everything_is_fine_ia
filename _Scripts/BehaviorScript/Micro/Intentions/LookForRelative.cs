using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AgentAllowed(typeof(Adult))]
public class LookForRelative : Intention {
    private Agent agentToFetch;
    public Agent AgentToFetch
    {
        get
        {
            return agentToFetch;
        }
    }

	public LookForRelative() : base() {}

	private void DefaultState(){

	}

	private void WhichMesoMember(Agent myAgent){
		List<Agent> meso = new List<Agent>(myAgent.Bdi.myBelief.MyGroup.Group.Keys);
		List<Agent> tooFar = new List<Agent>();

        foreach (Agent a in meso){// For each agent of my group
			if(!myAgent.Bdi.myBelief.MyGroup.Group[a].Equals(null))// If i (supposedly) know where he might be
            {
                if(!myAgent.Bdi.myPerception.getAgentsInSight(myAgent).Contains(a))// And if i don't see him
                {
                    tooFar.Add(a);
                }
            }
		}

        if(tooFar.Count > 0 && agentToFetch.Equals(null))
        {
            agentToFetch = tooFar[0];
            for(int i = 1; i < tooFar.Count; i++)
            {
                if(Vector3.Distance(tooFar[i].Rb.position, myAgent.Rb.position) 
                    < Vector3.Distance(agentToFetch.Rb.position, myAgent.Rb.position))
                {
                    agentToFetch = tooFar[i];
                }
            }
        }
	}
}
