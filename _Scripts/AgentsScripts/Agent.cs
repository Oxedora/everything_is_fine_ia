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
		flocking = new Flock(this);
		bdi = new BDI(this);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 reflexes = bdi.UpdateBDI();
        if (!reflexes.Equals(Vector3.zero) || bdi.myIntention == null)
        {
            Vector3 destination = transform.position + reflexes.normalized;
            //Debug.DrawLine(transform.position, destination, Color.black);
            rb.velocity = ((destination - transform.position).normalized) * settings.MaxSpeed;
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        else
        {
            Vector3 intentionDirection = bdi.myIntention.DefaultState(this).normalized;

            intentionDirection.y = 0;
            bdi.UpdateBDI();
            List<Agent> neighbors = bdi.myPerception.AgentsInSight;

            //Vector3 viewAngleA = bdi.myPerception.DirFromAngle(this, - settings.ViewAngle / 2, false);
            //Vector3 viewAngleB = bdi.myPerception.DirFromAngle(this, settings.ViewAngle / 2, false);

            //Debug.DrawLine(transform.position, transform.position + viewAngleA * settings.ViewRadius);
            //Debug.DrawLine(transform.position, transform.position + viewAngleB * settings.ViewRadius);

            //foreach(Agent a in neighbors){
            //    Debug.DrawLine(transform.position, a.transform.position, Color.yellow);
            //}

            Vector3 force = (intentionDirection.Equals(Vector3.zero) ? flocking.Flocking(neighbors) : (1 - settings.CoeffI) * flocking.Flocking(neighbors) + settings.CoeffI * intentionDirection);
            Vector3 destination = transform.position + force.normalized;
            //Debug.DrawLine(transform.position, destination, Color.black);
            rb.velocity = (force.Equals(Vector3.zero) ? (Vector3)transform.TransformDirection(Vector3.forward) : (destination - transform.position).normalized) * settings.MaxSpeed;
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Checkpoint"))
        {
            bdi.myBelief.OnCheckpoint = collision.gameObject;
            if (bdi.myBelief.CheckedPoints.Keys.Contains(collision.gameObject))
            {
                bdi.myBelief.CheckPoint(collision.gameObject);
            }
            else
            {
                bdi.myBelief.AddCP(collision.gameObject);
            }
        }
        else if (collision.gameObject.tag.Equals("Exit"))
        {
            gameObject.SetActive(false);
        }
    }

}
