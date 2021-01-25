using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellParticles : MonoBehaviour
{
    public static ParticleSystem.EmissionModule shellParticleEmisson;
    public static ParticleSystem shellParticleSystem;

    void Awake()
    {
        shellParticleEmisson = GameObject.Find("ShellParticleSystem").GetComponent<ParticleSystem>().emission;
        shellParticleEmisson.enabled = false;

        shellParticleSystem = GameObject.Find("ShellParticleSystem").GetComponent<ParticleSystem>();
    }


    public static void StartShellEmission()
    {
        shellParticleSystem.Play();
        shellParticleEmisson.enabled = true;
        shellParticleEmisson.rateOverTime = Mathf.Pow(PlayerStats.firingSpeed, 5) * 20 + 0.5f;
    }


    public static void StopShellEmission()
    {
        shellParticleEmisson.enabled = false;
        shellParticleSystem.Stop();
    }

}
