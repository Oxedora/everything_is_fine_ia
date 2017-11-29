using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception {
	private List<Agent> agentsInSight;
	public List<Agent> AgentsInSight {
		get {
			return agentsInSight;
		}
	}

	private List<Agent> agentsTooClose;
	public List<Agent> AgentsTooClose {
		get {
			return agentsTooClose;
		}
	}

	private List<GameObject> wallsInSight;
	public List<GameObject> WallsInSight {
		get {
			return wallsInSight;
		}
	}

	private List<GameObject> doorsInSight;
	public List<GameObject> DoorsInSight {
		get {
			return doorsInSight;
		}
	}

	private List<GameObject> indicationsInSight;
	public List<GameObject> IndicationsInSight {
		get {
			return indicationsInSight;
		}
	}

	private List<GameObject> fireInSight;
	public List<GameObject> FireInSight {
		get {
			return fireInSight;
		}
	}

    private List<GameObject> checkpointsInSight;
    public List<GameObject> CheckpointsInSight
    {
        get
        {
            return checkpointsInSight;
        }
    }

	// Use this for initialization
	public Perception(Agent agent) {
		agentsInSight = new List<Agent>();
		agentsTooClose = new List<Agent>();
		wallsInSight = new List<GameObject>();
		doorsInSight = new List<GameObject>();
		indicationsInSight = new List<GameObject>();
		fireInSight = new List<GameObject>();
        checkpointsInSight = new List<GameObject>();
	}

	// Update is called once per frame
	public void Update (Agent myAgent) {
		agentsInSight = getAgentsInSight(myAgent);
		agentsTooClose = getAgentsTooClose(myAgent);
		wallsInSight = getWallsInSight(myAgent);
		doorsInSight = getGameObjectsInSight(myAgent, myAgent.Settings.DoorMask);
		indicationsInSight = getGameObjectsInSight(myAgent, myAgent.Settings.IndicationMask);
		fireInSight = getGameObjectsInSight(myAgent, myAgent.Settings.FireMask);
        checkpointsInSight = getGameObjectsInSight(myAgent, myAgent.Settings.CheckpointMask);
	}

	public List<Agent> getAgentsInSight(Agent myAgent){
		List<Agent> visibleTargets = new List<Agent>();
		List<GameObject> go = getGameObjectsInSight(myAgent, myAgent.Settings.TargetMask);

		foreach(GameObject g in go){
			Agent target = g.GetComponent<Agent>();
			if(target != null){
					visibleTargets.Add(target);
			}
		}

		return visibleTargets;
	}

	public List<Agent> getAgentsTooClose(Agent myAgent){
		List<Agent> visibleTargets = new List<Agent>();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(myAgent.transform.position, myAgent.Settings.SafeSpace, myAgent.Settings.TargetMask);

		foreach (Collider c in targetsInViewRadius)
		{
			Agent target = c.gameObject.GetComponent<Agent>();
			if(target != null){
				Vector3 dirToTarget = (target.transform.position - myAgent.transform.position).normalized;
				
				float dstToTarget = Vector3.Distance(myAgent.transform.position, target.transform.position);
				if (!Physics.Raycast(myAgent.transform.position, dirToTarget, dstToTarget, myAgent.Settings.ObstacleMask))
				{
					visibleTargets.Add(target);
				}
			}
		}

		return visibleTargets;
	}

	public List<GameObject> getWallsInSight(Agent myAgent){
		List<GameObject> visibleTargets = new List<GameObject>();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(myAgent.transform.position, myAgent.Settings.ViewRadius, myAgent.Settings.ObstacleMask);

		foreach (Collider c in targetsInViewRadius)
		{
			GameObject target = c.gameObject;
			if(target != null){
				Vector3 dirToTarget = (target.transform.position - myAgent.transform.position).normalized;

				if (Vector3.Angle(myAgent.transform.forward, dirToTarget) < myAgent.Settings.ViewAngle / 2)
				{
					float dstToTarget = Vector3.Distance(myAgent.transform.position, target.transform.position);

					RaycastHit hitInfo;
					if (Physics.Raycast(myAgent.transform.position, dirToTarget, out hitInfo, dstToTarget, myAgent.Settings.ObstacleMask) 
						&& (hitInfo.collider.gameObject.GetInstanceID() == target.GetInstanceID()))
					{
						visibleTargets.Add(target);
					}
				}
		    }
		}

		return visibleTargets;
	}

	public List<GameObject> getGameObjectsInSight(Agent myAgent, LayerMask layer){
		List<GameObject> visibleTargets = new List<GameObject>();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(myAgent.transform.position, myAgent.Settings.ViewRadius, layer);

		foreach (Collider c in targetsInViewRadius)
		{
			GameObject target = c.gameObject;
			if(isInSight(myAgent, target)){visibleTargets.Add(target);}
		}

		return visibleTargets;
	}

	public bool isInSight(Agent myAgent, GameObject target){
		bool inSight = false;
		if(target != null){
				Vector3 dirToTarget = (target.transform.position - myAgent.transform.position).normalized;

				if (Vector3.Angle(myAgent.transform.forward, dirToTarget) < myAgent.Settings.ViewAngle / 2)
				{
					float dstToTarget = Vector3.Distance(myAgent.transform.position, target.transform.position);

					if (!Physics.Raycast(myAgent.transform.position, dirToTarget, dstToTarget, myAgent.Settings.ObstacleMask))
					{
						inSight = true;
					}
				}
		}
		return inSight;
	}

	public Vector3 DirFromAngle(Agent myAgent, float angleInDegrees, bool angleIsGlobal = false)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += myAgent.transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
