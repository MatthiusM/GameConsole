using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCleanup : MonoBehaviour
{
    private void Start()
    {
        if (TryGetComponent(out ParticleSystem gunShotParticle))
        {
            gunShotParticle.Play();
            StartCoroutine(StopParticleSystemAfterDelay(gunShotParticle, 0.1f));
        }
    }

    private IEnumerator StopParticleSystemAfterDelay(ParticleSystem particleSystem, float delay)
    {
        yield return new WaitForSeconds(delay);
        particleSystem.Stop();
        Destroy(particleSystem.gameObject, particleSystem.main.startLifetime.constantMax);
    }
}
