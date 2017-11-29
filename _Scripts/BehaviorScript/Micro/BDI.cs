using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BDI {
	private Belief belief;
	public Belief myBelief {
		get {
			return belief;
		}
	}

	private Desire desire;
	public Desire myDesire {
		get {
			return desire;
		}
	}

	private Intention intention;
	public Intention myIntention {
		get {
			return intention;
		}
	}

	private Perception perception;
	public Perception myPerception {
		get {
			return perception;
		}
	}

	private Feelings feelings;
	public Feelings myFeelings {
		get {
			return feelings;
		}
	}

	private Reasoning reasoning;
	public Reasoning myReasoning {
		get {
			return reasoning;
		}
	}

	private Agent myAgent;
	public Agent MyAgent {
		get {
			return myAgent;
		}
	}

	// Use this for initialization
	public BDI (Agent agent) {
		myAgent = agent;
		belief = new Belief(agent);
		desire = new Desire(agent);
		intention = null;
		perception = new Perception(agent);
		feelings = new Feelings(agent);
		reasoning = new Reasoning(agent);
	}
	
	// Update is called once per frame
	void Update () {
        perception.Update();
	}
}
