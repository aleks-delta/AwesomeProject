using UnityEngine;

public class AgentController : MonoBehaviour {
    public float speedX, speedY;
    public float activateVelocityAfter = 0.0f;
    private GameObject theConnector;
    //public GameObject otherAgent;
    AudioSource boing;
    private PlayingField playingField;

	// Use this for initialization
	void Start () {
        boing = FindObjectOfType<AudioSource>();
       
        playingField = FindObjectOfType<PlayingField>();
        Debug.Log(playingField? "Found Playing field" : "Did not find playing field");
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

        this.speedX = this.speedY = 0;

        playingField.AddConnection(gameObject, collision.gameObject);
    }
}
