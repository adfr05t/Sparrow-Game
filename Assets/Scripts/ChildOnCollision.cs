using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildOnCollision : MonoBehaviour
{
    [SerializeField] LayerMask relevantLayer;

    void OnCollisionEnter(Collision collision)
    {
        if (relevantLayer == (1 << collision.gameObject.layer))
        {
            collision.transform.parent = transform;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (relevantLayer == (1 << collision.gameObject.layer))
        {
            collision.transform.parent = null;
        }
    }
}
