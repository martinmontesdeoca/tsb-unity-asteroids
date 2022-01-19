using Unity.Entities;
[GenerateAuthoringComponent]
public struct MoveComponent : IComponentData
{   
    public float speed;
    public int forceSide;
    public float rotation;
}
