using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class ObjectsSpawner : MonoBehaviour
{
    public Vector3 limit;

    public GameObject prefab;
    public int numberOfObjects = 4;

    private List<Transform> _usedTransforms;
    private List<Vector3> _usedPositions;
    public float minDistance = 1.0f;
    
    void Awake()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        _usedTransforms = new List<Transform>();
        _usedPositions = new List<Vector3>();

        Vector3 prefabSize = prefab.GetComponent<Renderer>().bounds.size;

        for (int i = 0; i < numberOfObjects; i++)
        {
            Quaternion rotation = Quaternion.Euler(-90, Random.Range(0, 360), Random.Range(0, 360));

            var newObject = Instantiate(prefab, GetRandomPosition(prefabSize), rotation);

            if (!SetUniquePosition(newObject.transform, prefabSize))
                DestroyImmediate(newObject);

            _usedTransforms.Add(newObject.transform);
        }
    }

    private bool SetUniquePosition(Transform newObject, Vector3 scale)
    {
        int attempts = 0;

        do
        {
            newObject.position = GetRandomPosition(scale);

            attempts++;
            if (attempts >= 2000)
                return false;
        } while (IsTooClose(newObject));

        return true;
    }

    private bool IsTooClose(Transform position)
    {
        float limitedDistance = Math.Clamp(minDistance, .01f, Single.PositiveInfinity);
        foreach (Transform usedPosition in _usedTransforms)
        {
            if (ObjectDistance.GetClosestDistance(usedPosition, position) < limitedDistance)
                return true;
        }

        return false;
    }

    private Vector3 GetRandomPosition(Vector3 scale)
    {
        Vector3 position;

        do
        {
            position = transform.position + new Vector3(
                Random.Range(-limit.x / 2 + scale.x / 2, limit.x / 2 - scale.x / 2),
                Random.Range(-limit.y / 2 + scale.y, limit.y / 2 - scale.y / 2),
                Random.Range(-limit.z / 2 + scale.z / 2, limit.z / 2 - scale.z / 2)
            );
        } while (_usedPositions.Contains(position));

        _usedPositions.Add(position);

        return position;
    }

    void OnDrawGizmos()
    {
        var position = transform.position;

        Gizmos.color = new Color(1, 1, 1, 0.01f);
        Gizmos.DrawCube(position, limit);
        Gizmos.color = new Color(1, 1, 1, 0.4f);
        Gizmos.DrawWireCube(position, limit);
    }
}