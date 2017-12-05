using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belief {

	private MesoGroup myGroup = new MesoGroup();
	public MesoGroup MyGroup {
		get {
			return myGroup;
		}
	}

	// sous quelle forme stocker l'indication de la sortie ?
	private GameObject lastIndication = null;
	public GameObject LastIndication {
		get {
			return lastIndication;
		}

		set {
			lastIndication = value;
		}
	}

    private GameObject onCheckpoint = null;
    public GameObject OnCheckpoint
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

    private Dictionary<GameObject, int> checkedPoints = new Dictionary<GameObject, int>();
    public Dictionary<GameObject, int> CheckedPoints
    {
        get
        {
            return checkedPoints;
        }
    }

    private GameObject cpTarget = null;
    public GameObject CpTarget
    {
        get
        {
            return cpTarget;
        }
        set
        {
            cpTarget = value;
        }
    }

    public Belief() {}
    
	public Belief(MesoGroup mg){
		myGroup = mg;
	}

	// Update is called once per frame
	public void Update (Perception p) {
		MyGroup.UpdateGroup(p);
	}

	public void CheckPoint(GameObject go){
		checkedPoints[go]++;
	}

	public void AddCP(GameObject go){
		checkedPoints.Add(go, 1);
	}
}
