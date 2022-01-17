using Unity.Entities;
using UnityEngine;
/// <summary>
/// Component data containing forward speed.
/// </summary>
[GenerateAuthoringComponent]
public struct DestroyEntityComponent : IComponentData
{
    public Entity entity;    
}
