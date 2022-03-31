using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    private ParticleSystem explosion;

    private void Awake()
    {
        explosion = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (!explosion.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
