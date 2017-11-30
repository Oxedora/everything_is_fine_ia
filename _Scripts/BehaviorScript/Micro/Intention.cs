﻿using System.Collections;
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

	private ActionDelegate currentState;
	public ActionDelegate CurrentState {
		get {
			return currentState;
		}
	}

	// Use this for initialization
	public Intention() {
		currentState = DefaultState;
	}
	
	// Update is called once per frame
/*	private Vector3 Update (Agent myAgent, Perception p) {
		Vector3 dir = Reflexes(myAgent, p);
		return (dir.magnitude > 0 ? dir : currentState());
	}*/

	private Vector3 DefaultState() {return Vector3.zero;}

	
}
