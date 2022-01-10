using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public class PlayerMovementData : IComponentData
{
    public Vector2 move;
    public Vector2 horizontal;
    public Vector2 vertical;
    public float aceleration;
}
