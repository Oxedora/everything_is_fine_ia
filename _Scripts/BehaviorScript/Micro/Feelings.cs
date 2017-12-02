using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feelings {
	[Range(0.0f, 1.0f)]
	private float anger;
	public float Anger {
		get {
			return anger;
		}
	}

	[Range(0.0f, 1.0f)]
	private float fear;
	public float Fear {
		get {
			return fear;
		}
		set {
			fear = (value > 1.0f ? 1.0f : value);
		}
	}
	// Use this for initialization
	public Feelings () {
		System.Random rand = new System.Random();
		anger = (float) rand.Next(0, 3) / (float)10.0;
		fear = (float) rand.Next(0, 5) / (float)10.0;
	}
	
	// Update is called once per frame
	public void UpdateFeelings (Perception p) {
		fear += (float) 0.1 * p.FireInSight.Count;

	}
}
