using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	public int lives=3;
    public float speed;

	// Use this for initialization
	void Start () {

	}
	
	// remove the player icon if the player has no more lives
	void Update () {


		if (lives <= 0) {
			Destroy (gameObject);
		}

	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.name == "Tug")
            Destroy(gameObject);
    }
}
