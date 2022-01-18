using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct DestroyEntityComponent : IComponentData
{
    public Entity entity;    
}
