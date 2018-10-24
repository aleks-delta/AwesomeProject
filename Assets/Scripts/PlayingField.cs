using System.Collections.Generic;
using UnityEngine;

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
            connector.Update();
        }
	}

    public void AddAgent(GameObject newAgent)
    {
        Debug.LogFormat("num agents {0} ", agents.Count);
        agents.Add(newAgent);
        Debug.LogFormat("num agents after = {0} ", agents.Count);
    }

    public Connector AddConnection(GameObject agent1, GameObject agent2)
    {
        if (string.Compare(agent1.name, agent2.name) < 0)
        {
            Debug.LogFormat("connection between {0} and {1} skipped", agent1.name, agent2.name);
            return null;
        }
        Debug.Log("Adding connection");
        audioContainer = GetComponent<AudioSource>();
        audioContainer.PlayOneShot(joinClip);

        Debug.LogFormat("Connection #{0} created", connections.Count);
          
        var theRealConnection = new Connector(agent1, agent2);
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
