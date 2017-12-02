using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System;

/// <Summary>
/// States the agent wants to see realized and their ratio of priorities
/// One the desires are fixed they have no purpose of changing
/// Only their priority may change depending of the agent belief
/// </Summary>
public class Desire {
	private Dictionary<Intention, float> desires = new Dictionary<Intention, float>();

	/// <Summary>
	/// Gets all Intentions suitable for the given type of Agent
	/// </Summary>
	/// <param name="typeOfAgent"> Type of the agent : Adult, Elder, Kid, Disabled, Worker </param>
	public Desire (Type typeOfAgent) {
		System.Random rand = new System.Random(); // random priority will be set for every desire
		foreach (Type type in 
            Assembly.GetAssembly(typeof(Intention)).GetTypes() // get all types of Intention
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Intention)))) // checks that type is a non abstract subclass of Intention
        {
            desires.Add((Intention)Activator.CreateInstance(type), (float) rand.Next(0, 10) / (float)10.0); // update dictionnary with this possible desire
        }
	}
	
	/// <Summary>
	/// Returns the agent intention if he has one, the suitable intention based on his fear if he has none
	/// </Summary>
	/// <param name="fear"> the agent ratio of fear </param>
	/// <param name="agentIntention"> the agent current intention </param>
	public Intention Update (float fear, Intention agentIntention) {
		if(agentIntention != null){ return agentIntention; }
		Intention ind = null;
		foreach(Intention i in desires.Keys){
			if(desires[i] >= fear){
				if(ind == null){ind = i;}
				if(desires[i] < desires[ind]){
					ind = i;
				}
			}
		}
		return ind;
		// ajuster la priorité et renvoyer l'intention avec la plus haute priorité
		// priorité de chercher un membre du méso : + s'il manque quelqu'un, - si tout le monde est autour de moi
		// priorité pour sortir : + s'il y a un danger ou si je traine trop, - lorsque je suis dehors
		// priorité pour chercher le point de rassemblement : + lorsque je suis sorti, - si je suis toujours dans le bâtiment
	}
}
