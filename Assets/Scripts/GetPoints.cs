using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPoints : MonoBehaviour
{
    public float newpoints = 20;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        PointsCounter points = collision.gameObject.GetComponent<PointsCounter>();


        points.GetPoints(newpoints);
        Destroy(gameObject);
    }
}