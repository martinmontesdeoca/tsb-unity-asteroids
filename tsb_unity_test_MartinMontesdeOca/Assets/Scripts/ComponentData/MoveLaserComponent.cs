using Unity.Entities;
using UnityEngine;
/// <summary>
/// Component data containing forward speed.
/// </summary>
[GenerateAuthoringComponent]
public struct MoveLaserComponent : IComponentData
{
    public float speed;
    public float direction;
    public Vector3 playerPosition;
}
