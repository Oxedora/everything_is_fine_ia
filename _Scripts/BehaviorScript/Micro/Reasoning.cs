using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reasoning {
	private float ratioFear;
	public float RatioFear {
		get {
			return ratioFear;
		}
	}

	// Use this for initialization
	public Reasoning () {
	}
	
	// Update is called once per frame
	void UpdateReasoning (Belief b, Desire d, Feelings f) {
		if(f.Fear >= ratioFear){
			// renvoyer la folie
		}
		// sinon renvoyer le désire le plus grand
		// à quoi sert le belief ?
	}
}
