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

	private Agent myAgent;
	public Agent MyAgent {
		get {
			return myAgent;
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
		myAgent = agent;
	}

	// Update is called once per frame
	public void Update () {
		agentsInSight = getAgentsInSight();
		agentsTooClose = getAgentsTooClose();
		wallsInSight = getWallsInSight();
		doorsInSight = getGameObjectsInSight(MyAgent.Settings.DoorMask);
		indicationsInSight = getGameObjectsInSight(MyAgent.Settings.IndicationMask);
		fireInSight = getGameObjectsInSight(MyAgent.Settings.FireMask);
        checkpointsInSight = getGameObjectsInSight(MyAgent.Settings.CheckpointMask);
	}

	public List<Agent> getAgentsInSight(){
		List<Agent> visibleTargets = new List<Agent>();
		List<GameObject> go = getGameObjectsInSight(MyAgent.Settings.TargetMask);

		foreach(GameObject g in go){
			Agent target = g.GetComponent<Agent>();
			if(target != null){
					visibleTargets.Add(target);
			}
		}

		return visibleTargets;
	}

	public List<Agent> getAgentsTooClose(){
		List<Agent> visibleTargets = new List<Agent>();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(MyAgent.transform.position, MyAgent.Settings.SafeSpace, MyAgent.Settings.TargetMask);

		foreach (Collider c in targetsInViewRadius)
		{
			Agent target = c.gameObject.GetComponent<Agent>();
			if(target != null){
				Vector3 dirToTarget = (target.transform.position - MyAgent.transform.position).normalized;
				
				float dstToTarget = Vector3.Distance(MyAgent.transform.position, target.transform.position);
				if (!Physics.Raycast(MyAgent.transform.position, dirToTarget, dstToTarget, MyAgent.Settings.ObstacleMask))
				{
					visibleTargets.Add(target);
				}
			}
		}

		return visibleTargets;
	}

	public List<GameObject> getWallsInSight(){
		List<GameObject> visibleTargets = new List<GameObject>();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(MyAgent.transform.position, MyAgent.Settings.ViewRadius, MyAgent.Settings.ObstacleMask);

		foreach (Collider c in targetsInViewRadius)
		{
			GameObject target = c.gameObject;
			if(target != null){
				Vector3 dirToTarget = (target.transform.position - MyAgent.transform.position).normalized;

				if (Vector3.Angle(MyAgent.transform.forward, dirToTarget) < MyAgent.Settings.ViewAngle / 2)
				{
					float dstToTarget = Vector3.Distance(MyAgent.transform.position, target.transform.position);

					RaycastHit hitInfo;
					if (Physics.Raycast(MyAgent.transform.position, dirToTarget, out hitInfo, dstToTarget, MyAgent.Settings.ObstacleMask) 
						&& (hitInfo.collider.gameObject.GetInstanceID() == target.GetInstanceID()))
					{
						visibleTargets.Add(target);
					}
				}
		    }
		}

		return visibleTargets;
	}

	public List<GameObject> getGameObjectsInSight(LayerMask layer){
		List<GameObject> visibleTargets = new List<GameObject>();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(MyAgent.transform.position, MyAgent.Settings.ViewRadius, layer);

		foreach (Collider c in targetsInViewRadius)
		{
			GameObject target = c.gameObject;
			if(isInSight(target)){visibleTargets.Add(target);}
		}

		return visibleTargets;
	}

	public bool isInSight(GameObject target){
		bool inSight = false;
		if(target != null){
				Vector3 dirToTarget = (target.transform.position - MyAgent.transform.position).normalized;

				if (Vector3.Angle(MyAgent.transform.forward, dirToTarget) < MyAgent.Settings.ViewAngle / 2)
				{
					float dstToTarget = Vector3.Distance(MyAgent.transform.position, target.transform.position);

					if (!Physics.Raycast(MyAgent.transform.position, dirToTarget, dstToTarget, MyAgent.Settings.ObstacleMask))
					{
						inSight = true;
					}
				}
		}
		return inSight;
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal = false)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += MyAgent.transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
