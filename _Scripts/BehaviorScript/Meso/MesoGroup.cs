using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <Summary>
/// Level of crowd representation.
/// The meso modelisation represents people that know each other.
/// </Summary>
public class MesoGroup {
	/// <Summary>
	/// The dictionnary containing the agents and theirs positions
	/// </Summary>
	private Dictionary<Agent, Vector3?> group;
	public Dictionary<Agent, Vector3?> Group {
		get {
			return group;
		}
	}

	/// <Summary>
	/// Create a MesoGroup with every Agent and their position
	/// </Summary>
	/// <param name="d"> The dictionnary containing the agents and theirs positions </param>
	public MesoGroup (Dictionary<Agent, Vector3?> d) {
		group = d;
	}
	
	/// <Summary>
	/// Every frame, updates the position of every member in the group if the agent sees them
	/// </Summary>
	/// <param name="p"> The agent perception during this frame </param>
	void UpdateGroup (Perception p) {
		foreach(Agent a in group.Keys){ // for each member of this group
			if(p.AgentsInSight.Contains(a)){ // if the given agent sees this member
				group[a] = a.transform.position; // updates the position of this member for the given agent
			}
		}
	}

	/// <Summary>
	/// Sets the position of the given agent to null if he is part of the group
	/// </Summary>
	/// <param name="agent"> The agent whose position needs to be modified </param>
	private void LostTraceOf(Agent agent){
		if(group.Keys.Contains(agent)){ // if the given agent is in this group
			group[agent] = null; // set his position to unknown
		}
	}


	/// <Summary>
	/// Returns the position of the given agent if he is part of the group, null otherwise
	/// </Summary>
	/// <param name="agent"> The agent whose position is requested </param>
	public Vector3? PositionOfMember(Agent agent){
		return (group.Keys.Contains(agent) ? group[agent] : null);
	}
}
