using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Summary>
/// Agent reflexes and update of his intention
/// </Summary>
public class Reasoning {

	/// <Summary>
    /// Agent reflexes and update of his intention
    /// </Summary>
	public Reasoning () {}
	
	/// <Summary>
    /// Execute the agent reflexes, update his intention otherwise
    /// </Summary>
    /// <param name="i"> Agent current intention, null if he has none </param>
    /// <param name="d"> Agent desires </param>
    /// <param name="f"> Agent feelings </param>
    /// <param name="p"> Agent perceptions </param>
    /// <param name="ratioFear"> Agent treshold before going mad </param>
    /// <param name="position"> Agent position </param>
	public Vector3 UpdateReasoning (Intention i, Desire d, Feelings f, Perception p, float ratioFear, Vector3 position) {
        Vector3 reflexeDir = Vector3.zero;

        if (p.FireInSight.Count > 0)
        {
            f.Fear += p.FireInSight.Count * 0.1f;
        }
        // If he dies, he dies
		if(f.Fear >= ratioFear){
			i = null;
            return reflexeDir;
		}

        if (p.FireInSight.Count > 0)
        {
            reflexeDir = DodgeObjects(position, p.FireInSight);
        }
        else {
            i = d.Update(f.Fear, i);
        }

        return reflexeDir; 
	}

    /// <Summary>
    /// Returns the opposite vector3 of every object to dodge
    /// </Summary>
    /// <param name="position"> Agent position </param>
    /// <param name="objectsToDodge"> List of every objects the agent has to dodge </param>
    private Vector3 DodgeObjects(Vector3 position, List<GameObject> objectsToDodge)
    {
        Vector3 sep = Vector3.zero;

        foreach (GameObject go in objectsToDodge)
        {
            sep += position - go.transform.position;
        }
        sep.y = 0;
        return (sep.magnitude > 1.0f ? sep.normalized : sep);
    }

    /*// Returns the Vector3 position of the closest indication to follow
    private Vector3 FollowIndication(Agent myAgent, List<GameObject> indicationToFollow)
    {
        float closest = Vector3.Distance(myAgent.transform.position, indicationToFollow[0].transform.position);
        Vector3 indic = indicationToFollow[0].transform.position;
        foreach (GameObject go in indicationToFollow)
        {
            float dist = Vector3.Distance(myAgent.transform.position, go.transform.position);
            if (dist < closest)
            {
                closest = dist;
                indic = go.transform.position;
            }
        }
        return indic;
    }*/
}
