using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    public float radius = 2f;
    public float minSpeed = 1f;
    public float maxSpeed = 5f;
    public bool rotateObject = false;

    private Rigidbody heldObject;
    private Color lineColor = Color.yellow;
    private bool isRotating = false;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnDrawGizmos()
    {
        if (heldObject)
        {
            Gizmos.color = lineColor;
            Gizmos.DrawLine(transform.position, heldObject.transform.position);
        }
    }

    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        if (heldObject)
        {
            float distance = Vector3.Distance(transform.position, heldObject.transform.position);

            if (distance > radius || heldObject.transform.parent != null)
            {
                heldObject.useGravity = true;
                heldObject = null;
                lineColor = Color.yellow;
                isRotating = false;
            }
            else
            {
                float speed = Mathf.Lerp(minSpeed, maxSpeed, 1 - (distance / radius));
                heldObject.useGravity = false;
                Vector3 targetPosition = transform.position;
                heldObject.MovePosition(Vector3.Lerp(heldObject.position, targetPosition, Time.fixedDeltaTime * speed));

                if (distance < 0.1f)
                {
                    if (rotateObject && !isRotating)
                    {
                        isRotating = true;
                    }
                }

                if (isRotating)
                {
                    heldObject.transform.Rotate(Vector3.up, 360 * Time.fixedDeltaTime, Space.World);
                }
            }
        }
        else if (colliders.Length > 0)
        {
            Rigidbody closestRigidbody = null;
            float minDistance = float.MaxValue;

            foreach (Collider collider in colliders)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();

                if (rb != null && rb.transform.parent == null)
                {
                    float distance = Vector3.Distance(transform.position, rb.transform.position);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestRigidbody = rb;
                    }
                }
            }

            if (closestRigidbody != null)
            {
                heldObject = closestRigidbody;
                lineColor = Color.green;
                heldObject.useGravity = false;
                float speed = Mathf.Lerp(minSpeed, maxSpeed, 1 - (minDistance / radius));
                Vector3 targetPosition = transform.position;
                heldObject.MovePosition(Vector3.Lerp(heldObject.position, targetPosition,
                    Time.fixedDeltaTime * speed));
            }
        }
    }
}