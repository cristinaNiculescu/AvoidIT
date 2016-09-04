using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnPlayers : MonoBehaviour {
	public GameObject player1;
	public GameObject player2;
	public float screenOffset;
	public Camera main;
	public GameObject tug;
	public GameObject[] enemyTypes;
	public Vector3 p;
	public int countPlayer1;
	public int countPlayer2;
	public int timeSpent;
	public Text score;
	public float speed;
	public GameObject p1;
	public GameObject p2;
	bool printedP1=false;
	bool printedP2=false;

	GameObject playerSpawn1;
	GameObject playerSpawn2;
	// Use this for initialization
	void Start() {
		p = main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height, main.nearClipPlane));

		placePlayers ();
		StartCoroutine (enemySpawn ());
		StartCoroutine (increaseDifficulty(10));

	}

	// Update is called once per frame
	void Update () {
		timeSpent = (int)Time.realtimeSinceStartup;

		if(playerSpawn1 || playerSpawn2)
			score.text=timeSpent.ToString()+" s";

		if (playerSpawn1 == null) 
		{
			p1.SetActive(true);

			if (!printedP1) {
				p1.GetComponentInChildren<Text> ().text = "Player 1 lasted for " + score.text;
				printedP1 = true;
			}
		}
		if (playerSpawn2 == null) {
			p2.SetActive(true);
			if (!printedP2) {
				p2.GetComponentInChildren<Text> ().text = "Player 2 lasted for " + score.text;
				printedP2 = true;
			}
		}
	}

	public void placePlayers()
	{
		p = main.ScreenToWorldPoint(new Vector3 (Screen.width-screenOffset, Screen.height/2, main.nearClipPlane));
		p.z = -2.1f;

		playerSpawn1=Instantiate (player1, p, Quaternion.Euler(new Vector3(0, 0, 90))) as GameObject;
		playerSpawn1.transform.parent = transform;

		p = main.ScreenToWorldPoint(new Vector3 (screenOffset, Screen.height/2, main.nearClipPlane));
		p.z = -2.1f;
		playerSpawn2=Instantiate (player2, p,Quaternion.Euler(new Vector3(0, 0, -90))) as GameObject;
		playerSpawn2.transform.parent = transform;
	}

	IEnumerator enemySpawn()
	{
		
		int temp1 = (playerSpawn1) ? countPlayer1 : 0;
		int temp2 = (playerSpawn2) ? countPlayer2 : 0;
		Vector3 pos1;
		Vector3 pos2;
		GameObject enemyTemp;

		while ((Mathf.Abs (temp1 - temp2) > 0) || (temp1 > 0) || (temp2 > 0)) 
		{
			pos1 = new Vector3 (tug.transform.position.x + 1, Random.Range(-77,77), tug.transform.position.z);

			pos2 = new Vector3 (tug.transform.position.x - 1,  Random.Range(-77,77), tug.transform.position.z);
			yield return new WaitForSeconds (0.1f);
			if (temp1 > 0 & temp2 > 0) {
				
				enemyTemp = Instantiate (enemyTypes [0], pos1, Quaternion.identity) as GameObject;
				enemyTemp.GetComponent<EnemyBehavior> ().direction = 1;
				enemyTemp = Instantiate (enemyTypes [0], pos2, Quaternion.identity) as GameObject;
				enemyTemp.GetComponent<EnemyBehavior> ().direction = -1;
				temp2--;
				temp1--;
			} else 
				if (temp1 > 0) 
				{	
				enemyTemp = Instantiate (enemyTypes [0], pos1, Quaternion.identity) as GameObject;
				enemyTemp.GetComponent<EnemyBehavior> ().direction = 1;
				temp1--;
				} 
				else {
				enemyTemp = Instantiate (enemyTypes [0], pos2, Quaternion.identity) as GameObject;
				enemyTemp.GetComponent<EnemyBehavior> ().direction = -1;
				temp2--;
				}
		}

		yield return new WaitForSeconds (5f);
		StartCoroutine (enemySpawn ());
	}

	IEnumerator increaseDifficulty(float dur)
	{
		yield return new WaitForSeconds (dur);
		countPlayer1++;
		countPlayer2++;
		StartCoroutine(increaseDifficulty( ((dur-0.15f)>2) ? dur-0.15f : 2 ));
	//	StartCoroutine (enemySpawn());
	}

}
