using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesoGroup {
	private List<Agent> group;
	public List<Agent> Group {
		get {
			return group;
		}
	}

	// Use this for initialization
	// à voir comment on groupe les agents (p-e ceux qui sont dans ma zone sont mon méso ?)
	void Start (List<Agent> g) {
		group = g;
	}
	
	// Update is called once per frame
	// le groupe a-t-il vocation à être modifié ?
	void Update () {
		
	}
}
