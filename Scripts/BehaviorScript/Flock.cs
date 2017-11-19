using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {

	private Agent myAgent;
	public Agent MyAgent{
		get {
			return myAgent;
		}
	}

	// Use this for initialization
	void Start () {
		myAgent = GetComponent<Agent>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector3 Flocking(List<Agent> neighbors){
		Vector3 sep = Separate(neighbors);
		Vector3 ali = Align(neighbors);
		Vector3 coh = Cohere(neighbors);
		Vector3 dod = Dodge();

		Debug.DrawLine(MyAgent.transform.position, MyAgent.transform.position + sep, Color.green);
		Debug.DrawLine(MyAgent.transform.position, MyAgent.transform.position + ali, Color.blue);
		Debug.DrawLine(MyAgent.transform.position, MyAgent.transform.position + coh, Color.red);
		Debug.DrawLine(MyAgent.transform.position, MyAgent.transform.position + dod, Color.grey);
		
		/*Debug.Log("Separate : " + sep + " pour l'agent " + myAgent.gameObject.name);
		Debug.Log("Align : " + ali + " pour l'agent " + myAgent.gameObject.name);
		Debug.Log("Cohere : " + coh + " pour l'agent " + myAgent.gameObject.name);*/
		
		sep = sep * MyAgent.Settings.CoeffS;
		ali = ali * MyAgent.Settings.CoeffA;
		coh = coh * MyAgent.Settings.CoeffC;
		dod = dod * MyAgent.Settings.CoeffD;

		return ((1 - MyAgent.Settings.CoeffD) * (sep + ali + coh).normalized + dod * MyAgent.Settings.CoeffD).normalized;
	}

	private Vector3 Separate(List<Agent> neighbors){
		List<Agent> nearestAgents = new List<Agent>();

		foreach(Agent a in neighbors){
			if(Vector3.Distance(a.transform.position, MyAgent.transform.position) <= MyAgent.Settings.SafeSpace){ 
				nearestAgents.Add(a);
			}
		}

		Vector3 sep = Vector3.zero;

		foreach(Agent a in nearestAgents){
			sep += MyAgent.transform.position - a.transform.position;
		}
        sep.y = 0; // DEGUEUELAAAAAAAAAAAAASSSE
		return (sep.magnitude > 1.0f ? sep.normalized : sep);

	}

	private Vector3 Align(List<Agent> neighbors){
		Vector3 ali = Vector3.zero;

		foreach(Agent a in neighbors){
			ali += (Vector3) a.transform.TransformDirection(Vector3.forward);
		}
        //if(neighbors.Count > 0){ali = ali / (float) neighbors.Count;}
        ali.y = 0;
		return (ali.magnitude > 1.0f ? ali.normalized : ali);
	}

	private Vector3 Cohere(List<Agent> neighbors){
		Vector3 coh = Vector3.zero;

		foreach(Agent a in neighbors){
			coh += a.transform.position - transform.position;
		}
		//Debug.Log("cohesion vector : " + coh + " pour l'agent " + gameObject.name);
		//if(neighbors.Count > 0){coh = coh / (float) neighbors.Count;}
		Debug.Log("cohesion vector : " + coh + " pour l'agent " + gameObject.name);

        coh.y = 0;

		return (coh.magnitude > 1.0f ? coh.normalized : coh);
	}

	private Vector3 Dodge(){
		Vector3 dod = Vector3.zero;
		
		RaycastHit hitInfo, hitInfoLeft, hitInfoRight;
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		if(Physics.Raycast(transform.position, fwd, out hitInfo, myAgent.Settings.SafeSpace, myAgent.Settings.ObstacleMask)){
            Debug.DrawLine(transform.position, hitInfo.point, Color.magenta);
            dod = transform.position - hitInfo.point;
			float ratio = dod.magnitude / (float) myAgent.Settings.ViewRadius;
			float angle = myAgent.Settings.ViewAngle - myAgent.Settings.ViewAngle * ratio;
			dod = myAgent.Fov.DirFromAngle(angle);

            Vector3 left = transform.TransformDirection(Vector3.left);
            Vector3 right = transform.TransformDirection(Vector3.right);

            Physics.Raycast(transform.position, left, out hitInfoLeft, myAgent.Settings.ViewRadius, myAgent.Settings.ObstacleMask);
            Physics.Raycast(transform.position, right, out hitInfoRight, myAgent.Settings.ViewRadius, myAgent.Settings.ObstacleMask);

            if(Vector3.Distance(transform.position, hitInfoLeft.point) > Vector3.Distance(transform.position, hitInfoRight.point))
            {
                dod = -dod;
            }
        }

        dod.y = 0;

        return (dod.magnitude > 1.0f ? dod.normalized : dod);
	}
}
