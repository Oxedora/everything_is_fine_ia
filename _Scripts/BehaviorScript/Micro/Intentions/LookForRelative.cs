using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AgentAllowed(typeof(Agent))]
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

	public override Vector3 DefaultState(Agent agent){
        return Vector3.zero;
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

    public override void UpdatePriority(Agent agent)
    {
        // Counting the number of agent that aren't in my sight
        int nbAgentLost = agent.Bdi.myBelief.MyGroup.Group.Count;
        foreach(Agent relative in agent.Bdi.myBelief.MyGroup.Group.Keys)
        {
            if(agent.Bdi.myPerception.AgentsInSight.Contains(relative))
            {
                nbAgentLost--;
            }
        }

        // If there is agent to seek i update my priority else it's 0
        Priority = (nbAgentLost > 0 && agent.Bdi.myFeelings.Fear < agent.Settings.RatioFear ? ((float)nbAgentLost / (float)agent.Bdi.myBelief.MyGroup.Group.Count) - agent.Bdi.myFeelings.Fear : 0);
    }
}
