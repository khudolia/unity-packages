# Scriptable outline

Developed by Andrii Khudolii (с) 2022


### How to do? 

To add an outline effect to an object, drag-n-drop the `OutlineInstance.cs` script to needed object.

It will create `OutlineController.cs` with which is possible to control outline parameters, such as:

```
- Width
- Color
- Visibility status
- Animation speed
```

Then if need to remove outline - just remove\disable `OutlineInstance.cs` script.

***DO NOT REMOVE MATERIALS OR `OutlineController.cs` MANUALLY!***

This package allows to enable\disable outline with animation.
To control it - `isVisible` parameter should be changed.
To control speed of in\out animation - change `animationDuration` parameter.

The changing of the `outlineColor` also goes with animation. So, all what will be changed - will be changed smoothly.

### Examples

Add:
```
if(gameObject.GetComponent<OutlineInstance>() == null)
    gameObject.AddComponent<OutlineInstance>();
```

Remove:

```
Destroy(gameObject.GetComponent<OutlineInstance>());
```

Change parameters:

```
private OutlineController _controller;

_controller.isVisible = true;
_controller.outlineColor = Color.white;
```
