    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor;

// Reference :
// https://processing.org/examples/flocking.html

public class Agent : MonoBehaviour {

    private Rigidbody rb;
    public Rigidbody Rb {
    	get {
    		return rb;
    	}
    }

    private AgentsSettings settings;
    public AgentsSettings Settings {
    	get {
    		return settings;
    	}
    }

    private Flock flocking;
    public Flock Flocking {
    	get {
    		return flocking;
    	}
    }

    private BDI bdi;
    public BDI Bdi {
    	get {
    		return bdi;
    	}
    }


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		settings = GetComponent<AgentsSettings>();
		flocking = GetComponent<Flock>();
		bdi = GetComponent<BDI>();
	}
	
	// Update is called once per frame
	void Update () {
        List<Agent> neighbors = bdi.myPerception.AgentsInSight;

        Vector3 viewAngleA = bdi.myPerception.DirFromAngle(- settings.ViewAngle / 2, false);
        Vector3 viewAngleB = bdi.myPerception.DirFromAngle(settings.ViewAngle / 2, false);

        Debug.DrawLine(transform.position, transform.position + viewAngleA * settings.ViewRadius);
        Debug.DrawLine(transform.position, transform.position + viewAngleB * settings.ViewRadius);

        foreach(Agent a in neighbors){
            Debug.DrawLine(transform.position, a.transform.position, Color.yellow);
        }

        Vector3 force = flocking.Flocking(neighbors);
        Vector3 destination = transform.position + force;
        Debug.DrawLine(transform.position, destination, Color.black);
        rb.velocity = (force.Equals(Vector3.zero) ? (Vector3)transform.TransformDirection(Vector3.forward) : force) * settings.MaxSpeed;
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

}
