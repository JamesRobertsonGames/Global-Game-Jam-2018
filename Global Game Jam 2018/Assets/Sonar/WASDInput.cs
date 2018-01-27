using UnityEngine;

public class WASDInput : MonoBehaviour
{
    public float speed = 10.0f;

	private void Update ()
    {
        var input = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")).normalized;
        input = transform.TransformDirection(input);
        transform.localPosition += input * speed * Time.deltaTime;
	}
}
