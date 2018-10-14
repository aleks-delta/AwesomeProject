using UnityEngine;

public class AgentControllerRival : MonoBehaviour {
    public float speedX, speedY;
    private GameObject theConnector;
    private GameObject otherAgent;
    AudioSource boing;
    private const float activateVelocityAfter = 3.0f;

	// Use this for initialization
	void Start () {
        boing = FindObjectOfType<AudioSource>();
        theConnector = new GameObject("myConnector");
        theConnector.AddComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.realtimeSinceStartup < activateVelocityAfter)
            return;
       
        transform.Translate(speedX * Time.deltaTime, speedY * Time.deltaTime, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        boing.Play();
        if (!otherAgent)
            otherAgent = collision.gameObject;
        this.speedX = this.speedY = 0;
        //replace a hardcoded agent with the agent that comes from the collision
        if (!otherAgent)
            return;

        //Debug.DrawLine(this.transform.position, otherAgent.transform.position, Color.green);
        Debug.LogFormat("drawing line: this pos = {0}, other pos = {1}", this.transform.position, otherAgent.transform.position);
        
        if (theConnector) {
            Debug.LogFormat("OMG! connector = {0}", theConnector);
            theConnector.SetActive(true);
            var lineRenderer = theConnector.GetComponent<LineRenderer>();
            lineRenderer.material = Resources.Load("LineMaterial") as Material;
           
            lineRenderer.widthMultiplier = 0.75f;

            Debug.LogFormat("num points = {0}, material = {1}", lineRenderer.positionCount, lineRenderer.material);
            //lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, this.transform.position);
            lineRenderer.SetPosition(1, otherAgent.transform.position);
            
             lineRenderer.startColor = Color.magenta;
            lineRenderer.endColor = Color.magenta;

            Debug.LogFormat("this pos = {0}, other pos = {1}", this.transform.position, otherAgent.transform.position);
            Debug.LogFormat("diff = {0}, average={1}, localScale={2}", this.transform.position - otherAgent.transform.position, 
                                                (this.transform.position+otherAgent.transform.position)/2.0f,
                                                theConnector.transform.localScale);
            theConnector.transform.position = (this.transform.position + otherAgent.transform.position) / 2.0f;

            theConnector.transform.localScale = -(this.transform.position - otherAgent.transform.position) / 8.0f;
            Debug.LogFormat("local scale after = {0}", theConnector.transform.localScale);
        }
        else
        {
            Debug.Log("connector NOT found");
        } 
    }
}
