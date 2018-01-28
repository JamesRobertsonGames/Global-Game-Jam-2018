using System.Collections.Generic;
using UnityEngine;

public struct ParticleDecalData
{
    public Vector3 position;
    public float size;
    public Vector3 rotation;
    public Color colour;
}

public class ParticleDecalPool
{
    private ParticleSystem decalParticleSystem;
    private Color decalColour;
    private float decalSize;
    private int maxDecals;
    private int decalIndex;
    private ParticleDecalData[] particleData;
    private ParticleSystem.Particle[] particles;

    public ParticleDecalPool(ParticleSystem decalParticleSystem, Color decalColour, float decalSize, int maxDecals)
    {
        this.decalParticleSystem = decalParticleSystem;
        this.decalColour = decalColour;
        this.decalSize = decalSize;
        this.maxDecals = maxDecals;
        this.particles = new ParticleSystem.Particle[maxDecals];
        this.particleData = new ParticleDecalData[maxDecals];
    }

    public void ParticleHit(ParticleCollisionEvent collisionEvent)
    {
        if (decalIndex >= maxDecals)
        {
            decalIndex = 0;
        }

        particleData[decalIndex].position = collisionEvent.intersection;
        Vector3 particleRotationEuler = Quaternion.LookRotation(collisionEvent.normal).eulerAngles;
        //particleRotationEuler.z = Random.Range(0, 360); Not necessary?
        particleData[decalIndex].rotation = particleRotationEuler;
        particleData[decalIndex].size = decalSize;
        particleData[decalIndex].colour = decalColour;

        decalIndex += 1;

        UpdateParticles();
    }

    private void UpdateParticles()
    {
        for (int i = 0; i < maxDecals; i++)
        {
            particles[i].position = particleData[i].position;
            particles[i].startSize = particleData[i].size;
            particles[i].rotation3D = particleData[i].rotation;
            particles[i].startColor = particleData[i].colour;
        }

        decalParticleSystem.SetParticles(particles, particles.Length);
    }
}

public class SonarParticleSystem : SingletonMonoBehaviour<SonarParticleSystem>
{
    public ParticleSystem transitSonar;
    public ParticleSystem rippleSonar;
    public ParticleSystem decalSonar;
    public int maxDecals = 100;
    public int decalSize = 1;
    public Color decalColour;

    private ParticleDecalPool decalPool;

    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private void Start()
    {
        decalPool = new ParticleDecalPool(decalSonar, decalColour, decalSize, maxDecals);
    }

    public void EmitSonar(Vector3 origin, Vector3 direction)
    {
        transitSonar.transform.position = origin;
        transitSonar.transform.rotation = Quaternion.LookRotation(direction);
        transitSonar.Emit(1);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Sonar"))
        {
            EmitSonar(transform.position, transform.forward);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(transitSonar, other, collisionEvents);
        for (int i = 0; i < collisionEvents.Count; i++)
        {
            EmitRippleAt(collisionEvents[i]);
            decalPool.ParticleHit(collisionEvents[i]);
        }
    }

    private void EmitRippleAt(ParticleCollisionEvent collisionEvent)
    {
        rippleSonar.transform.position = collisionEvent.intersection;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, collisionEvent.intersection - transform.position, out hit, Mathf.Infinity))
        {
            rippleSonar.transform.rotation = Quaternion.LookRotation(hit.normal);
            rippleSonar.Emit(rippleSonar.maxParticles); // and what??? eh???
        }
    }
}
