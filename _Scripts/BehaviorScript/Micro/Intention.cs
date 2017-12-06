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

	// Use this for initialization
	public Intention() {
	}

    public abstract Vector3 DefaultState(Agent agent);

    public abstract void UpdatePriority(Agent agent);
}
