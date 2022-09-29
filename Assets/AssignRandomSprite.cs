using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignRandomSprite : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private bool _randomizeFlip = true;
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        if (_renderer == null) return;

        if ( _sprites.Length > 0)
        {
            int randomInt = Random.Range(0, _sprites.Length);
            _renderer.sprite = _sprites[randomInt];
        }

        if (_randomizeFlip)
        {
            int randomInt = Random.Range(0, 2);
            _renderer.flipX = randomInt == 0;

        }

    }
}
