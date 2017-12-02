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
            return checkedPoints;
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
        bdi.UpdateBDI();
        Vector3 intentionDirection = bdi.myIntention.DefaultState(this).normalized;
        intentionDirection.y = 0;
        bdi.UpdateBDI();
        List<Agent> neighbors = bdi.myPerception.AgentsInSight;

        Vector3 viewAngleA = bdi.myPerception.DirFromAngle(this, - settings.ViewAngle / 2, false);
        Vector3 viewAngleB = bdi.myPerception.DirFromAngle(this, settings.ViewAngle / 2, false);

        Debug.DrawLine(transform.position, transform.position + viewAngleA * settings.ViewRadius);
        Debug.DrawLine(transform.position, transform.position + viewAngleB * settings.ViewRadius);

        foreach(Agent a in neighbors){
            Debug.DrawLine(transform.position, a.transform.position, Color.yellow);
        }

        Vector3 force = flocking.Flocking(neighbors) + intentionDirection;
        Vector3 destination = transform.position + force.normalized;
        Debug.DrawLine(transform.position, destination, Color.black);
        rb.velocity = (force.Equals(Vector3.zero) ? (Vector3)transform.TransformDirection(Vector3.forward) : force) * settings.MaxSpeed;
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.layer.Equals(settings.CheckpointMask))
    //    {
    //        Debug.Log("CHECKPOOOOOOOOOOOINT " + collision.gameObject.name);
    //        onCheckpoint = true;
    //        if(checkedPoints.Keys.Contains(collision.gameObject))
    //        {
    //            checkedPoints[collision.gameObject]++;
    //        }
    //        else
    //        {
    //            checkedPoints.Add(collision.gameObject, 0);
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("What is that trigger my dear ?");
        //Debug.Log(collision.gameObject.layer.ToString()+", And layer mask value : "+ settings.CheckpointMask.ToString());
        if ((settings.CheckpointMask & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            if(collision.gameObject.tag.Equals("Exit"))
            {
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("CHECKPOOOOOOOOOOOINT " + collision.gameObject.name);
                onCheckpoint = true;
                if (checkedPoints.Keys.Contains(collision.gameObject))
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

}
