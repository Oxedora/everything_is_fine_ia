using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
	private List<GameObject> checkpoints;
	public List<GameObject> Checkpoints {
		get {
			return checkpoints;
		}
	}

    [SerializeField]
    private List<Node> linkedNodes;
    public List<Node> LinkedNodes
    {
        get
        {
            return linkedNodes;
        }
    }

	// Use this for initialization
	void Start () {
        checkpoints = new List<GameObject>();

        foreach (Transform child in transform)
        {
            checkpoints.Add(child.gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
