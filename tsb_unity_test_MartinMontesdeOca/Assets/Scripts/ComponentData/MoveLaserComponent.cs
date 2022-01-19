using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct MoveLaserComponent : IComponentData
{
    public float speed;
    public float direction;
    public Vector3 playerPosition;
}
