using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefineIntention("Adult")]
[DefineIntention("Elder")]
[DefineIntention("Worker")]
public class LookForRelative : Intention {
	private Agent myAgent;
	public Agent MyAgent {
		get {
			return myAgent;
		}
	}

	public LookForRelative(Agent agent) : base(agent) {}

	private void DefaultState(){

	}

	private void WhichMesoMember(){
		List<Agent> meso = myAgent.BDI.myBelief.myGroup.Group;
		List<Agent> tooFar = new List<Agent>();
		foreach(Agent a in meso){
			
		}
	}
}
