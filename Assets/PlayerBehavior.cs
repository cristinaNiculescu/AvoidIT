using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	float h;
	public float speed;
	Rigidbody2D rb2d;
	public string axis;
	public int lives=3;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		h = Input.GetAxis (axis);
		if (h != 0) {
			rb2d.velocity = h * Vector2.up* speed;
		}
		if (lives <= 0) {
			Destroy (gameObject);
		}

	}
}
