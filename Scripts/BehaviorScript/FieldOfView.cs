using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

    private Agent myAgent;

    public Agent MyAgent
    {
        get
        {
            return myAgent;
        }
    }

    private void Start()
    {
        myAgent = GetComponent<Agent>();
    }

    public List<Agent> FindVisibleTargets()
    {
        List<Agent> visibleTargets = new List<Agent>();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, MyAgent.Settings.ViewRadius, MyAgent.Settings.TargetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Agent target = targetsInViewRadius[i].gameObject.GetComponent<Agent>();
            if(target != null){
                Vector3 dirToTarget = (target.transform.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, dirToTarget) < MyAgent.Settings.ViewAngle / 2)
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.transform.position);

                    if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, MyAgent.Settings.ObstacleMask))
                    {
                        visibleTargets.Add(target);
                    }
                }
            }
        }

        return visibleTargets;
    }

    public List<Agent> FindCloseTargets()
    {
        List<Agent> visibleTargets = new List<Agent>();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, MyAgent.Settings.SafeSpace, MyAgent.Settings.TargetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Agent target = targetsInViewRadius[i].gameObject.GetComponent<Agent>();
            if(target != null){
                Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
                float dstToTarget = Vector3.Distance(transform.position, target.transform.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, MyAgent.Settings.ObstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }

        return visibleTargets;
    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal = false)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
