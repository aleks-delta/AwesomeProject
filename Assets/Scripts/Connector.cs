using UnityEngine;

public class Connector
{
    public Connector(GameObject a1, GameObject a2)
    {
        //this.me = m;
        this.agent1 = a1;
        this.agent2 = a2;
        me = CreateConnectorGO(agent1, agent2);
    }

    public void Update()
    {
        LineRenderer lr = me.GetComponent<LineRenderer>();
        lr.SetPosition(0, agent1.transform.position);
        lr.SetPosition(1, agent2.transform.position);
    }

    private GameObject CreateConnectorGO(GameObject agent1, GameObject agent2)
    {
        var theConnectorGO = new GameObject("bond " + agent1.name + " + " + agent2.name);
        theConnectorGO.AddComponent<LineRenderer>();

        theConnectorGO.SetActive(true);
        var lineRenderer = theConnectorGO.GetComponent<LineRenderer>();
        lineRenderer.material = Resources.Load("LineMaterial") as Material;

        Color col = new Color(1, 1, 1) {a = 1.0f}; // colors[connections.Count];
        lineRenderer.startColor = col;
        lineRenderer.endColor = col;
        const float MAX_HOTNESS = 10.0f;
        //thickness proportional to the OTHER person's hotness
        float hotness1 = agent1.GetComponent<AgentController>().hotness;
        float hotness2 = agent2.GetComponent<AgentController>().hotness;
        float thickness1 = hotness2 / MAX_HOTNESS; 
        float thickness2 = hotness1 / MAX_HOTNESS;
        lineRenderer.startWidth = thickness1;
        lineRenderer.endWidth = thickness2;
        var velocityAfter = (hotness1 - hotness2);
        agent1.GetComponent<AgentController>().speedX = velocityAfter;
        agent2.GetComponent<AgentController>().speedX = velocityAfter;
        theConnectorGO.transform.position = (agent1.transform.position + agent2.transform.position) / 2.0f;
        theConnectorGO.transform.localScale = -(agent1.transform.position - agent2.transform.position) / 8.0f;
        return theConnectorGO;
    }

    private GameObject me;
    private GameObject agent1;
    private GameObject agent2;

    internal void SetActive(bool v)
    {
        me.SetActive(v);
    }
}
