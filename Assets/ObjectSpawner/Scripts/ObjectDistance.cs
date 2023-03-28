using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDistance : MonoBehaviour
{
    public Transform object1;
    public Transform object2;

    public static float GetClosestDistance(Transform position1, Transform position2)
    {
        Collider collider1 = position1.GetComponent<Collider>();
        Collider collider2 = position2.GetComponent<Collider>();

        Vector3 closestPoint1 = collider1.ClosestPoint(collider2.transform.position);
        Vector3 closestPoint2 = collider2.ClosestPoint(closestPoint1);
        // Calculate the distance between the closest points of the colliders
        float distance = Vector3.Distance(closestPoint1, closestPoint2);

        return distance;
    }

    public static bool IsInside(Transform position1, Transform position2)
    {
        Collider collider1 = position1.GetComponent<Collider>();
        Collider collider2 = position2.GetComponent<Collider>();

        return collider1.bounds.Intersects(collider2.bounds);
    }

    void OnDrawGizmos()
    {
        // Get the colliders of each object
        Collider collider1 = object1.GetComponent<Collider>();
        Collider collider2 = object2.GetComponent<Collider>();

        // Get the closest points of the colliders
        Vector3 closestPoint1 = collider1.ClosestPoint(collider2.transform.position);
        Vector3 closestPoint2 = collider2.ClosestPoint(closestPoint1);

        Debug.Log("The distance between the edges of the objects is: " + GetClosestDistance(object1, object2));
        Debug.Log("Is inside: " + IsInside(object1, object2));

        // Draw a gizmo line between the closest points
        Gizmos.color = Color.red;
        Gizmos.DrawLine(closestPoint1, closestPoint2);
    }
}
