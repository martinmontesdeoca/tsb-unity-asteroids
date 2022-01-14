using Unity.Entities;
/// <summary>
/// Component data containing forward speed.
/// </summary>
[GenerateAuthoringComponent]
public struct MoveForwardComponent : IComponentData
{
    public float direction;
    public float speed;
    public int forceSide;
}
