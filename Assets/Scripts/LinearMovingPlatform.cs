using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3[] destinations;
    [SerializeField] private int targetDestinationIndex;


    void Start()
    {
        targetDestinationIndex = 0;
    }

    void Update()
    {
        MoveToTargetDestination();
    }

    void MoveToTargetDestination()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destinations[targetDestinationIndex], step);

        // Check if the platform position is approximately equal current destination
        if (Vector3.Distance(transform.position, destinations[targetDestinationIndex]) < 0.001f)
        {
            ChangeTargetDestination();
        }
    }

    void ChangeTargetDestination()
    {
        targetDestinationIndex++;

        if (targetDestinationIndex >= destinations.Length)
        {
            targetDestinationIndex = 0;
        }
    }
}
