using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class SonarDetectable : MonoBehaviour
{
    private float highlightTimer = 0.0f;
    private Renderer rend;
    private Color originalColour;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        originalColour = rend.material.color;
    }

    public void Update()
    {
        highlightTimer -= Time.deltaTime;

        if (highlightTimer <= 0.0f)
        {
            rend.material.color = originalColour;
        }
    }

    public void Highlight(Color colour, float duration)
    {
        rend.material.color = colour;
        highlightTimer = duration;
    }
}