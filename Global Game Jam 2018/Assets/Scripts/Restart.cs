using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {
    
    public int sceneToStart = 0;

    // Use this for initialization
    void Start () {

        StartCoroutine(restart());
        
    }

    IEnumerator restart ()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }
	
}
