using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;


[GenerateAuthoringComponent]
public class PlayerInputData : IComponentData
{
    public KeyCode UP;
    public KeyCode DOWN;
    public KeyCode LEFT;
    public KeyCode RIGHT;
    public KeyCode SHOOT;
}
