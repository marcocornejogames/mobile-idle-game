using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headstone : MonoBehaviour
{
    [Header("Customization")]
    [SerializeField] private float _lifeSpan = 600f;


    private void Awake()
    {
        Invoke("Die", _lifeSpan);
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
