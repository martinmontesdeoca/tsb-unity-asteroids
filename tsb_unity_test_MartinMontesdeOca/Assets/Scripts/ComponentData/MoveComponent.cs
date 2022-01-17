using Unity.Entities;
/// <summary>
/// Component data containing forward speed.
/// </summary>
[GenerateAuthoringComponent]
public struct MoveComponent : IComponentData
{   
    public float speed;
    public int forceSide;
    public float rotation;
}
