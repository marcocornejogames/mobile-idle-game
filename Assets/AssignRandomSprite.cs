using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignRandomSprite : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Sprite[] _sprites;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();

        if (_renderer != null &&
            _sprites.Length > 0)
        {
            int randomInt = Random.Range(0, _sprites.Length);
            _renderer.sprite = _sprites[randomInt];
        }
    }
}
