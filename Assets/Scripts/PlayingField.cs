using System;
using System.Collections.Generic;
using UnityEngine;

public class Connector
{
    public Connector(GameObject m, GameObject a1, GameObject a2)
    {
        this.me = m;
        this.agent1 = a1;
        this.agent2 = a2;
    }
    public void UpdatePos()
    {
        LineRenderer lr = me.GetComponent<LineRenderer>();
        lr.SetPosition(0, agent1.transform.position);
        lr.SetPosition(1, agent2.transform.position);
    }
    private GameObject me;
    private GameObject agent1;
    private GameObject agent2;

    internal void SetActive(bool v)
    {
        me.SetActive(v);
    }
}

public class PlayingField : MonoBehaviour {
    public Color[] colors;
    List<GameObject> agents;
    List<Connector> connections;
    AudioSource audioContainer;
    AudioClip joinClip, splitClip;

    void Start () {
        agents = new List<GameObject>();
        connections = new List<Connector>();
        joinClip = Resources.Load<AudioClip>("Sounds/boing1");
        splitClip = Resources.Load<AudioClip>("Sounds/wah");
    }
	
	void Update () {
		foreach (var connector in connections)
        {
            connector.UpdatePos();
        }
	}

    public void AddAgent(GameObject newAgent)
    {
        Debug.LogFormat("num agents = {0} ", agents.Count);
        agents.Add(newAgent);
        Debug.LogFormat("num agents after = {0} ", agents.Count);
    }

    public Connector AddConnection(GameObject agent1, GameObject agent2, float thickness1 = 1.0f, float thickness2 = 1.0f)
    {
        Debug.Log("Adding connection");
        if (string.Compare(agent1.name, agent2.name) < 0)
        {
            Debug.LogFormat("connection between {0} and {1} skipped", agent1.name, agent2.name);
            return null;
        }
        Debug.Log("playing sound");
        audioContainer = this.GetComponent<AudioSource>();
        audioContainer.PlayOneShot(joinClip);

        Debug.LogFormat("Connection #{0} created", connections.Count);
        var theConnectorGO = new GameObject("bond " + agent1.name + " + " + agent2.name);
        theConnectorGO.AddComponent<LineRenderer>();
        
        theConnectorGO.SetActive(true);
        var lineRenderer = theConnectorGO.GetComponent<LineRenderer>();
        lineRenderer.material = Resources.Load("LineMaterial") as Material;

        //lineRenderer.widthMultiplier = 0.5f;

        Color col = colors[connections.Count];
        col.a = 1.0f;
        lineRenderer.startColor = col;
        lineRenderer.endColor = col;
        lineRenderer.startWidth = thickness1;
        lineRenderer.endWidth = thickness2;

        theConnectorGO.transform.position = (agent1.transform.position + agent2.transform.position) / 2.0f;
        theConnectorGO.transform.localScale = -(agent1.transform.position - agent2.transform.position) / 8.0f;

        var theRealConnection = new Connector(theConnectorGO, agent1, agent2);
        connections.Add(theRealConnection);
        return theRealConnection;
    }

    public void RemoveConnection(Connector connection)
    {
        Debug.LogFormat("Connection #{0} removed", connections.Count);
        audioContainer = this.GetComponent<AudioSource>();
        audioContainer.PlayOneShot(splitClip);

        connection.SetActive(false);
        connections.Remove(connection);
    }
}
