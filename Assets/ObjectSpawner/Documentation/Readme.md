# Object spawner

Developed by Andrii Khudolii (с) 2023

This package provides 2 scripts that will help you to spawn objects or determine distance between some objects.

You can find 2 scripts in the project: `ObjectsSpawner.cs` `ObjectDistance.cs`.

***For both scripts `Collider` component are required***

## Objects Spawner script
### How to do? 

To create a spawner area, drag-n-drop the `ObjectsSpawner.cs` script to needed object.

Then, you can control its parameters:

```
- Limit
- Prefab
- Number of objects
- Min distance
```

Also the script creates visual representation of the area in which objects will be spawned. 

## Object Distance script
### How to do?

Just drug-n-drop to any object on the scene and specify 2 objects, between which distance will be calculated.

It will draw a red line between this objects. This line - a closest path between them.

### Public methods

Returns closest distance between 2 provided objects:
```
float GetClosestDistance(Transform position1, Transform position2)
```

Says if 2 provided objects are inside each other or not:

```
bool IsInside(Transform position1, Transform position2)
```
