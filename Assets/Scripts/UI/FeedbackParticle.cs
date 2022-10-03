using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackParticle : MonoBehaviour
{
    [Header("Component Reference")]
    [SerializeField] private GameObject _emitterObject;
    [SerializeField] private ParticleSystem _emitter;

    public void SpawnAtLocation(MonoBehaviour subject)
    {
        _emitterObject.transform.position = subject.transform.position;
        _emitter.Play();
    }
}
