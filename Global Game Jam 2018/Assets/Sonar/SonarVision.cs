using System.Linq;
using UnityEngine;

public class SonarVision : SingletonMonoBehaviour<SonarVision>
{
    public float radius = 1.0f;
    public Color highlightColour = Color.red;
    public float highlightDuration = 5.0f;

    private Material mat;
    private int posId;
    private int dirId;

    protected override void Awake()
    {
        base.Awake();
        mat = FindObjectOfType<SonarDetectable>().GetComponent<Renderer>().sharedMaterial;
        posId = Shader.PropertyToID("sonarOrigin");
        dirId = Shader.PropertyToID("sonarDirection");
    }

    private void Update()
    {
        mat.SetVector(posId, transform.position);
        mat.SetVector(dirId, transform.forward);
    }

    private void FixedUpdate()
    {
        //RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, transform.forward);
        //if (hits != null)
        //{
        //    var detectables = hits.Select(hit => hit.collider.GetComponent<SonarDetectable>());
        //    foreach (var detectable in detectables.Where(detectable => detectable != null))
        //    {
        //        detectable.Highlight(highlightColour, highlightDuration);   
        //    }
        //}
    }
}
