﻿using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	public float speed;
	Rigidbody2D rb2d;
	public int direction;
	// Use this for initialization
	void Start () {
		GameObject gameMaster = GameObject.Find ("GameMaster");
		transform.parent =gameMaster.transform;
		rb2d =	GetComponent<Rigidbody2D> ();
		rb2d.velocity = Vector2.right* direction * speed;

	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs (transform.position.x) > 160 )
			Destroy (gameObject, 2f);

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.transform.name == "player1(Clone)" || col.transform.name == "player2(Clone)") {
			col.GetComponent<PlayerBehavior> ().lives--;

		}
	}
}
