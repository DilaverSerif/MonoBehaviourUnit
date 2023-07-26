# MonoBehaviourUnit
Unity Visual Scripting Unit class but with MonoBehaviour

I don't know about its performance
Just inherit from MonoBehaviourUnit class instead of unit class
```csharp
using UnityEngine;
using VSExtension.Nodes;

public class MoveUnit : MonoBehaviourUnit
{
    protected override void Definition()
    {
    }
    
    public override void Update()
    {
        Move();
    }

    public override void LateUpdate()
    {
        Debug.Log("LateUpdate");
    }

    public override void Awake()
    {
        Debug.Log("Awake");
    }

    private void Move()
    {
        transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
    }
    
}

```
