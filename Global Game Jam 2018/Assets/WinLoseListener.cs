using UnityEngine;

public class WinLoseListener : MonoBehaviour
{
	private void Start ()
    {
        BatMovement.Instance.OnBatCollided += OnBatCollided;	
	}

    private void OnBatCollided(Vector3 point, GameObject other)
    {
        if (other.tag == "portal")
        {
            // win
        }
        else if (other.tag == "fireball")
        {
            // do something cool perhaps, then lose?
        }
        else
        {
            // lose
        }
    }
}
