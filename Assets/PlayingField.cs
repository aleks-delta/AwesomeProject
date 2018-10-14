using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingField : MonoBehaviour {

    List<GameObject> agents = new List<GameObject>();
    List<GameObject> connections = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddAgent(GameObject newAgent)
    {
        Debug.LogFormat("num agents = {0} ", agents.Count);
        agents.Add(newAgent);
        Debug.LogFormat("num agents after = {0} ", agents.Count);
    }

    public void AddConnection(GameObject agent1, GameObject agent2)
    {
        if (string.Compare(agent1.name, agent2.name) > 0)
        {
            Debug.LogFormat("connection skipped, will be created elsewhere");
            return;
        }
        Debug.Log("Connection created");
        var theConnector = new GameObject("bond " + agent1.name + " + " + agent2.name);
        theConnector.AddComponent<LineRenderer>();
        if (theConnector) {
            theConnector.SetActive(true);
            var lineRenderer = theConnector.GetComponent<LineRenderer>();
            lineRenderer.material = Resources.Load("LineMaterial") as Material;

            lineRenderer.widthMultiplier = 0.75f;

            lineRenderer.SetPosition(0, agent1.transform.position);
            lineRenderer.SetPosition(1, agent2.transform.position);

            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.magenta;
            theConnector.transform.position = (agent1.transform.position + agent2.transform.position) / 2.0f;

            theConnector.transform.localScale = -(agent1.transform.position - agent2.transform.position) / 8.0f;
            connections.Add(theConnector);
        }
        else
        {
            Debug.Log("connector NOT found");
        }
    }
}
