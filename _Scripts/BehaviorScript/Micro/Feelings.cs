using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feelings : MonoBehaviour {
	[Range(0,1)]
	private float anger;
	public float Anger {
		get {
			return anger;
		}
	}

	[Range(0,1)]
	private float fear;
	public float Fear {
		get {
			return fear;
		}
	}
	// Use this for initialization
	void Start () {
		System.Random rand = new System.Random();
		anger = (float) rand.Next(0, 10) / (float)10.0;
		fear = (float) rand.Next(0, 10) / (float)10.0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
