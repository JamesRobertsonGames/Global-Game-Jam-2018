using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseListener : MonoBehaviour
{
    public int levelNumber;
	private void Start ()
    {
        BatMovement.Instance.OnBatCollided += OnBatCollided;	
	}

    private void OnBatCollided(Vector3 point, GameObject other)
    {
        if (other.tag == "portal")
        {
            if (levelNumber == 1)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                // win
                SceneManager.LoadScene(0);
            }

        }
        else if (other.tag == "fireball")
        {
            // do something cool perhaps, then lose?
        }
        else
        {
            // lose
            SceneManager.LoadScene(0);
        }
    }
}
