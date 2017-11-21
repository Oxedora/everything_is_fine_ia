using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Contains all settings for every agents
public class AgentsSettings : MonoBehaviour{

	// Perception radius
	private float viewRadius = 20.0f;
	public float ViewRadius {
		get {
			return viewRadius;
		}
	}

	// Perception angle
	[Range(0,360)]
	private float viewAngle= 120.0f;
	public float ViewAngle {
		get {
			return viewAngle;
		}
	}

	// Minimal distance between me and another agent
	private float safeSpace = 1.0f;
	public float SafeSpace {
		get {
			return safeSpace;
		}
	}

	// Maximal steering force
	private float maxForce = 1.0f;
	public float MaxForce {
		get {
			return maxForce;
		}
	}

	// Maximal speed
	private float maxSpeed = 5.0f;
	public float MaxSpeed {
		get {
			return maxSpeed;
		}
	}

	// Coeff for Align force
	private float coeffA = 0.33f;
	public float CoeffA {
		get {
			return coeffA;
		}
	}

	// Coeff for Cohesion force
	private float coeffC = 0.33f;
	public float CoeffC {
		get {
			return coeffC;
		}
	}

	// Coeff for Separate force
	private float coeffS = 3.33f;
	public float CoeffS {
		get {
			return coeffS;
		}
	}

	// Coeff for Dodge force
	[Range(0.0f, 1.0f)]
	private float coeffD = 0.9f;
	public float CoeffD {
		get {
			return coeffD;
		}
	}

	// Angle for dodging
	[Range(0, 180)]
	private float dodgingAngle = 30.0f;
	public float DodgingAngle {
		get {
			return dodgingAngle;
		}
	}

	// Agents layer 
	public LayerMask targetMask;
	public LayerMask TargetMask {
		get {
			return targetMask;
		}
	}

	// Obstacles layer
	public LayerMask obstacleMask;
	public LayerMask ObstacleMask {
		get {
			return obstacleMask;
		}
	}

	void Start() {
		/*targetMask = LayerMask.GetMask("targetMask");
		obstacleMask = LayerMask.GetMask("obstacleMask");*/
	}

    
}
