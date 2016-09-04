using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LifeStatus : MonoBehaviour {

	GameObject p1;
	GameObject p2;
	public GameObject lifeP1;
	public GameObject lifeP2;
	List <GameObject> livesP1;
	List <GameObject> livesP2;
	int lifeCount;
	GameObject tug;
	// Use this for initialization
	void Start () {
		p1 = GameObject.Find ("player1(Clone)");
		p2 = GameObject.Find ("player2(Clone)");
	
		lifeCount = p1.GetComponent<PlayerBehavior> ().lives;
		livesP1=new List<GameObject>();

		for (int i = 0; i < lifeCount; i++) 
		{	
			Vector3 pos = new Vector3 (transform.position.x, transform.localScale.x - 10*(i+1), -2.1f);
			GameObject temp=Instantiate (lifeP1, pos, Quaternion.identity) as GameObject;
			temp.transform.parent = transform;
			livesP1.Add( temp);

		}

		lifeCount = p2.GetComponent<PlayerBehavior> ().lives;
		livesP2=new List<GameObject>();

		for (int i = 0; i <lifeCount; i++) 
		{	
			Vector3 pos = new Vector3 (transform.position.x, 0 +10*(i+1), -2.1f);
			GameObject temp=Instantiate (lifeP2, pos, Quaternion.identity) as GameObject;
			temp.transform.parent = transform;
			livesP2.Add(temp);
		}


	}
	
	// Update is called once per frame
	void Update () {
		if (p1 != null) 
		{
			lifeCount = p1.GetComponent<PlayerBehavior> ().lives;
			if ((livesP1.Count - lifeCount >=1)&& (livesP1.Count>0)) {
				GameObject temp = livesP1 [livesP1.Count -1];
				livesP1.Remove (livesP1 [livesP1.Count -1]);
				Destroy (temp);
			}
				
		}
		if (p2 != null) {
			lifeCount = p2.GetComponent<PlayerBehavior> ().lives;
			if((livesP2.Count - lifeCount >=1) && (livesP2.Count>0))
			{
				GameObject temp = livesP2 [livesP2.Count -1];
				livesP2.Remove (livesP2 [livesP2.Count -1]);
				Destroy (temp);
			}
		}
	}

}
