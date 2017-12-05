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

    [Range(0f, 1f)]
    private float priority;
    public float Priority
    {
        get
        {
            return priority;
        }
        set
        {
            priority = value;
        }
    }

	private ActionDelegate currentState;
	public ActionDelegate CurrentState {
		get {
			return currentState;
		}
	}

	// Use this for initialization
	public Intention() {
	}

    // Update is called once per frame
    /*	private Vector3 Update (Agent myAgent, Perception p) {
            Vector3 dir = Reflexes(myAgent, p);
            return (dir.magnitude > 0 ? dir : currentState());
        }*/

    public abstract Vector3 DefaultState(Agent agent);

    public abstract void UpdatePriority(BDI bdi);
}
