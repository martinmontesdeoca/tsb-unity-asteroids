
using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct OvniShootComponent : IComponentData
{       
    public Vector3 position;
    public Quaternion rotation;
    public bool shoot;
}
