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

    private bool onCheckpoint = false;
    public bool OnCheckpoint
    {
        get
        {
            return onCheckpoint;
        }

        set
        {
            onCheckpoint = value;
        }
    }

    private Dictionary<GameObject, int> checkedPoints;
    public Dictionary<GameObject, int> CheckedPoints
    {
        get
        {
            return CheckedPoints;
        }
    }


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		settings = GetComponent<AgentsSettings>();
		flocking = new Flock(this);
		bdi = new BDI(this);
        checkedPoints = new Dictionary<GameObject, int>();
	}
	
	// Update is called once per frame
	void Update () {
        List<Agent> neighbors = bdi.myPerception.AgentsInSight;

        Vector3 viewAngleA = bdi.myPerception.DirFromAngle(this, - settings.ViewAngle / 2, false);
        Vector3 viewAngleB = bdi.myPerception.DirFromAngle(this, settings.ViewAngle / 2, false);

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


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer.Equals(settings.CheckpointMask))
        {
            onCheckpoint = true;
            if(checkedPoints.Keys.Contains(collision.gameObject))
            {
                checkedPoints[collision.gameObject]++;
            }
            else
            {
                checkedPoints.Add(collision.gameObject, 0);
            }
        }
    }

}
