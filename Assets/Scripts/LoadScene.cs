using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
    public int sceneNumber;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void loadNextScene()
    {
        //Application.LoadLevel(1);
        SceneManager.LoadScene(sceneNumber);
    }
}