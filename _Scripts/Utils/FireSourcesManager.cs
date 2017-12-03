using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSourcesManager : MonoBehaviour {

    private List<GameObject> fireSources;
    [SerializeField]
    private float cooldown = 5.0f;
    private bool fireActivated = false;

	// Use this for initialization
	void Start () {
        fireSources = new List<GameObject>();

        foreach(Transform child in transform)
        {
            if(child.tag.Equals("FireSource"))
            {
                fireSources.Add(child.gameObject);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!fireActivated)
        {
            cooldown -= Time.deltaTime;

            if (cooldown < 0)
            {
                FireSourceRandomActivator();
                fireActivated = true;
            }
        }
	}

    private List<GameObject> GetDisableFireSources()
    {
        List<GameObject> disabledFireSources = new List<GameObject>();

        foreach(GameObject fs in fireSources)
        {
            if(!fs.activeInHierarchy)
            {
                disabledFireSources.Add(fs);
            }
        }

        return disabledFireSources;
    }

    private void FireSourceRandomActivator()
    {
        List<GameObject> disabledFireSource = GetDisableFireSources();

        if(disabledFireSource.Count > 0)
        {
            int fireSelected = Random.Range(0, disabledFireSource.Count);
            disabledFireSource[fireSelected].SetActive(true);

            Debug.Log("Activated " + disabledFireSource[fireSelected].name);
        }
    }
}
