using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BDI : MonoBehaviour {
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

	// Use this for initialization
	void Start () {
		perception = new Perception(GetComponent<Agent>());
	}
	
	// Update is called once per frame
	void Update () {
        perception.Update();
	}
}
