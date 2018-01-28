using System.Linq;
using UnityEngine;

public class SonarVision : SingletonMonoBehaviour<SonarVision>
{
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
}
