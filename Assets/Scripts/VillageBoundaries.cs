using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class VillageBoundaries : MonoBehaviour
{
    public static VillageBoundaries Instance;

    [Header("Component References")]
    [SerializeField] private Collider2D _collider;

    private void Awake()
    {
        //Manage Instances
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        _collider = GetComponent<Collider2D>();
    }

    public bool IsInsideBounds(Vector2 target)
    {
        return _collider.bounds.Contains(new Vector3(target.x, target.y, _collider.transform.position.z));
    }
}
