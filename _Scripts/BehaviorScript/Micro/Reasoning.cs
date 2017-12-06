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
    /// <param name="agent">The agent that needs his reasoning updated </param>
	public Vector3 UpdateReasoning (Agent agent) {
        Vector3 reflexeDir = Vector3.zero;
        
        // If he dies, he dies
		if(agent.Bdi.myFeelings.Fear >= agent.Settings.RatioFear){
			agent.Bdi.myIntention = null;
            Debug.Log(agent.gameObject.name + " est figé par la peur");
            // rajouter la fifolie
            return reflexeDir;
		}

        if (agent.Bdi.myPerception.FireInSight.Count > 0)
        {
            reflexeDir = DodgeObjects(agent.transform.position, agent.Bdi.myPerception.FireInSight);
        }
        else {
            agent.Bdi.myIntention = agent.Bdi.myDesire.DesiredIntention(agent); // ligne à modif pour les intentions
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
}
