using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AgentAllowed(typeof(Adult))]
[AgentAllowed(typeof(Elder))]
[AgentAllowed(typeof(Worker))]
public class GetOut : Intention {

	public GetOut() : base() {}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void DefaultState(){
		
	}
}
