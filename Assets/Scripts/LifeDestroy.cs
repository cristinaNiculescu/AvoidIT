using UnityEngine;
using System.Collections;

public class LifeDestroy : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// kill it if the life is no longer displayed on the tug
	void Update () {

        if (transform.position.x != GameObject.Find("Tug").transform.position.x)
        {
            Destroy(gameObject);
        }
	}
}
