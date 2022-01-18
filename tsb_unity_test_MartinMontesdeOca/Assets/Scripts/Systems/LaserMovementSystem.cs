using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

public class LaserMovementSystem : ComponentSystem
{   
    protected override void OnUpdate()
    {       
        Entities.WithAll<MoveLaserComponent>().ForEach((ref Translation trans, ref Rotation rot, ref MoveLaserComponent moveForward) =>
        {                  
            moveForward.direction+=1;            
            var pos = float3.zero;            
            pos.y += 15f  *  Time.DeltaTime;
            trans.Value += math.mul(rot.Value, pos);                   
        });
    }
}

