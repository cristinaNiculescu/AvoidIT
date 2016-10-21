using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public int lifePowerUp;
    public float speedPowerUp;
    public int direction;
    public int speed;
    Rigidbody2D rb2d;
    public int pushTug;

	// Use this for initialization
	void Start () {
        GameObject gameMaster = GameObject.Find("GameMaster");
        transform.parent = gameMaster.transform;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.right * direction * speed;
    }
	
	// kill it, if too far away
	void Update () {
        if (Mathf.Abs(transform.position.x) > 160)
            Destroy(gameObject, 2f);
    }

    /// <summary>
    /// If it hits the player, do the appropriate thing. 
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.name.StartsWith("player"))
        {
            if (transform.name.StartsWith("Ulti"))
            {
                Debug.Log(transform.name);
                GameObject tug = GameObject.Find("Tug");
                tug.transform.Translate(Vector2.right * pushTug * ((col.transform.name == "player1(Clone)") ? -1 : 1));
                Destroy(gameObject);
            }
            else
            {
                col.GetComponent<PlayerBehavior>().lives += lifePowerUp;
                col.GetComponent<PlayerBehavior>().speed += speedPowerUp;
                Destroy(gameObject);
            }

        }
        
    }
}
