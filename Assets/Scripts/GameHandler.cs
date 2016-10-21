using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {
	public GameObject player1;
	public GameObject player2;
	public float screenOffset;
	public Camera main;
	public GameObject tug;
	public GameObject[] enemyTypes;
    public GameObject[] powerUps;
    public Vector3 p;
	public int countPlayer1;
	public int countPlayer2;
	public int timeSpent;
	public Text score;
	public float speed;
	public GameObject p1;
	public GameObject p2;
    public GameObject end;
    bool printedP1=false;
	bool printedP2=false;
    int p1score;
    int p2score;

	GameObject playerSpawn1;
	GameObject playerSpawn2;


	// Use this for initialization
	void Start() {
		p = main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height, main.nearClipPlane));

		placePlayers ();
		StartCoroutine (enemySpawn ());
		StartCoroutine (increaseDifficulty(5));
        GameObject.Find("Canvas").GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        end.SetActive(false);
    }

	// Update is called once per frame
    /// <summary>
    /// updates the timer and toggles the end panels for each player
    /// </summary>
	void Update () {
		timeSpent = (int)Time.realtimeSinceStartup;

		if(playerSpawn1 || playerSpawn2)
			score.text=timeSpent.ToString()+" s";

		if (playerSpawn1 == null) 
		{
			p1.SetActive(true);

			if (!printedP1) {
				p1.GetComponentInChildren<Text> ().text = "Player 1 lasted for " + score.text;
                p1score = timeSpent;
				printedP1 = true;
			}
		}
		if (playerSpawn2 == null) {
			p2.SetActive(true);
            
			if (!printedP2) {
				p2.GetComponentInChildren<Text> ().text = "Player 2 lasted for " + score.text;
                p2score = timeSpent;
				printedP2 = true;
			}
		}
        if (printedP1 & printedP2)
        {
            end.SetActive(true);
            p2.SetActive(false);
            p1.SetActive(false);
            GameObject.Find("Canvas").GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay ;
            end.GetComponentInChildren<Text>().text = (p1score > p2score) ? "Player 1 wins, lasting " + p1score.ToString() :
                                                                        "Player 2 wins, lasting " + p2score.ToString();
        }
	}

    /// <summary>
    /// places the 2 players in the right places on the sides of the screen
    /// </summary>
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

    /// <summary>
    /// Chooses randomly between the available enemies and spawns them on each side of the tug. Each player has its own number
    /// enemies to be spawned as temp1(countPlayer1) or temp2(countPlayer2). Setup this was so it's simple to add a power up 
    /// that changes the number of enemies/players. 
    /// </summary>
    /// <returns></returns>
	IEnumerator enemySpawn()
	{
		
		int temp1 = (playerSpawn1) ? countPlayer1 : 0;
		int temp2 = (playerSpawn2) ? countPlayer2 : 0;
		Vector3 pos1;
		Vector3 pos2;
		GameObject enemyTemp;
        int rand;

		while ((Mathf.Abs (temp1 - temp2) > 0) || (temp1 > 0) || (temp2 > 0)) 
		{
			pos1 = new Vector3 (tug.transform.position.x + 1, Random.Range(-77,77), tug.transform.position.z);
			pos2 = new Vector3 (tug.transform.position.x - 1,  Random.Range(-77,77), tug.transform.position.z);
			yield return new WaitForSeconds (0.15f);
            rand = (int)Random.Range(0f, 2.5f);

            
            if (temp1 > 0 & temp2 > 0) {
				
				enemyTemp = Instantiate (enemyTypes [rand], pos1, Quaternion.identity) as GameObject;
				enemyTemp.GetComponent<EnemyBehavior> ().direction = 1;
				enemyTemp = Instantiate (enemyTypes [rand], pos2, Quaternion.identity) as GameObject;
				enemyTemp.GetComponent<EnemyBehavior> ().direction = -1;
				temp2--;
				temp1--;
			} else 
				if (temp1 > 0) 
				{	
				enemyTemp = Instantiate (enemyTypes [rand], pos1, Quaternion.identity) as GameObject;
				enemyTemp.GetComponent<EnemyBehavior> ().direction = 1;
				temp1--;
				} 
				else {
				enemyTemp = Instantiate (enemyTypes [rand], pos2, Quaternion.identity) as GameObject;
				enemyTemp.GetComponent<EnemyBehavior> ().direction = -1;
				temp2--;
				}
		}

		yield return new WaitForSeconds (0.005f);
	}

    /// <summary>
    /// Adds some progression to the game: increments the number of enemies/player and reduces the timer for difficulty changes.
    /// </summary>
    /// <param name="dur"></param>
    /// <returns></returns>
	IEnumerator increaseDifficulty(float dur)
	{
        StartCoroutine(enemySpawn());
        yield return new WaitForSeconds (dur);
		countPlayer1++;
		countPlayer2++;
        powerUpSpawn();
		StartCoroutine(increaseDifficulty( ((dur-0.005f)>0.02) ? dur-0.15f : 0.02f ));
		
	}

    /// <summary>
    /// Applies a spawning similar to the enemy spawn system. It takes into account a random chance for each player. 
    /// </summary>
    void powerUpSpawn()
    {
        Vector3 pos1;
        Vector3 pos2;
        GameObject powerUpTemp;
        int rand;

        if (playerSpawn1)
        {
            rand = (int)Random.Range(0f, ((float)powerUps.Length - 0.5f));
            pos1 = new Vector3(tug.transform.position.x + 1, Random.Range(-77, 77), tug.transform.position.z);
            float chance1 = Random.Range(0f, 100f);

            if (chance1 <= 60)
            {
                powerUpTemp = Instantiate(powerUps[rand], pos1, Quaternion.identity) as GameObject;
                powerUpTemp.GetComponent<PowerUp>().direction = 1;
            }
            else if (chance1 >= 90)
            {
                powerUpTemp = Instantiate(powerUps[rand], pos1, Quaternion.identity) as GameObject;
                powerUpTemp.GetComponent<PowerUp>().direction = 1;
            }
        }

        if (playerSpawn2)
        {
            rand = (int)Random.Range(0f, ((float)powerUps.Length - 0.5f));
            float chance2 = Random.Range(0f, 100f);
            pos2 = new Vector3(tug.transform.position.x - 1, Random.Range(-77, 77), tug.transform.position.z);
            if (chance2 <= 60)
            {
                powerUpTemp = Instantiate(powerUps[2], pos2, Quaternion.identity) as GameObject;
                powerUpTemp.GetComponent<PowerUp>().direction = -1;
            }
            else if (chance2 >= 90)
            {
                powerUpTemp = Instantiate(powerUps[2], pos2, Quaternion.identity) as GameObject;
                powerUpTemp.GetComponent<PowerUp>().direction = -1;
            }
        }
    }
}
