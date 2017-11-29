using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

public class Desire {
	private Dictionary<Intention, float> desires = new Dictionary<Intention, float>();

	// Use this for initialization
	public Desire (Agent agent) {
		// pour chaque intention
			// je l'ajoute dans le dictionnaire avec une valeur de priorité
	}
	
	// Update is called once per frame
	void Update () {
		// ajuster la priorité et renvoyer l'intention avec la plus haute priorité
		// priorité de chercher un membre du méso : + s'il manque quelqu'un, - si tout le monde est autour de moi
		// priorité pour sortir : + s'il y a un danger ou si je traine trop, - lorsque je suis dehors
		// priorité pour chercher le point de rassemblement : + lorsque je suis sorti, - si je suis toujours dans le bâtiment
	}
}
