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

        livesP1 = new List<GameObject>();
        livesP2 = new List<GameObject>();

        updateLifeDisplay(p1, livesP1, lifeP1,0);
        updateLifeDisplay(p2, livesP2, lifeP2,0);

 
    }

    /// <summary>
    /// Put a life icon for each life assigned to the players.
    /// </summary>
    /// <param name="p"></param>
    /// <param name="livesP"></param> 
    /// <param name="lifeDisplay"></param>
    /// <param name="startCount"></param> 
    void updateLifeDisplay(GameObject p, List <GameObject> livesP, GameObject lifeDisplay, int startCount)
    {
        lifeCount = p.GetComponent<PlayerBehavior>().lives;
       

        for (int i = startCount; i < lifeCount; i++)
        {
            Vector3 pos = new Vector3(transform.position.x, 0 + 10 * (i + 1) * ((p.transform.name == "player1(Clone)") ? -1 : 1), -2.1f);
            GameObject temp = Instantiate(lifeDisplay, pos, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            livesP.Add(temp);
        }
    }
	
	/// <summary>
    /// updates the display, removing or calling the update function to display the right amount of lives. 
    /// </summary>
	void Update () {
		if (p1 != null) 
		{
			lifeCount = p1.GetComponent<PlayerBehavior> ().lives;
			if ((livesP1.Count - lifeCount >=1)&& (livesP1.Count>0)) {
				GameObject temp = livesP1 [livesP1.Count -1];
				livesP1.Remove (livesP1 [livesP1.Count -1]);
				Destroy (temp);
			}
            else if (livesP1.Count - lifeCount <0)
                updateLifeDisplay(p1, livesP1, lifeP1, lifeCount-1);

        }
		if (p2 != null) {
			lifeCount = p2.GetComponent<PlayerBehavior> ().lives;
			if((livesP2.Count - lifeCount >=1) && (livesP2.Count>0))
			{
				GameObject temp = livesP2 [livesP2.Count -1];
				livesP2.Remove (livesP2 [livesP2.Count -1]);
				Destroy (temp);
			}
            else if ((livesP2.Count - lifeCount <0))
                updateLifeDisplay(p2, livesP2, lifeP2, lifeCount-1);
        }
	}

}
