using System;
using UnityEngine;

public class AgentController : MonoBehaviour {
    public float speedX, speedY;
    public float afterSpeedX; 
    public float activateVelocityAfter = 0.0f;
    public float killConnectionsAfter = 0.0f;
    public float thickness1 = 1.0f;
    public float thickness2 = 1.0f;
    private Connector theConnector = null;
    
    private PlayingField playingField;
    private DateTime connectionTime;

	// Use this for initialization
	void Start () {
        playingField = FindObjectOfType<PlayingField>();
        Debug.Log(playingField? "Found Playing field" : "Did not find playing field");
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.realtimeSinceStartup < activateVelocityAfter)
            return;

        transform.Translate(speedX * Time.deltaTime, speedY * Time.deltaTime, 0);
        if (killConnectionsAfter > 0 && theConnector != null)
        {
            if ((DateTime.Now - connectionTime).TotalSeconds > killConnectionsAfter)
            {
                playingField.RemoveConnection(theConnector);
                theConnector = null;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("colliding!");
        this.speedX = afterSpeedX;
        this.speedY = 0;
        if (theConnector == null)
        {
            Debug.Log("Making connections yo!");
            this.connectionTime = DateTime.Now;
            theConnector = playingField.AddConnection(gameObject, collision.gameObject, thickness1, thickness2);
        }
    }
}
