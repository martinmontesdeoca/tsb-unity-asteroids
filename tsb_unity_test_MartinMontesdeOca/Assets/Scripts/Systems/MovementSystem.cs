using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.Physics;
using Unity.Physics.Extensions;
/// <summary>
/// System for moving enemies or bullets forward
/// </summary>
/// 
// 1 Systems inherit from ComponentSystem (single thread) 
public class MovementSystem : ComponentSystem
{
    // 2 event-style callback that runs every frame
    protected override void OnUpdate()
    {       
        Entities.WithAll<MoveForwardComponent>().ForEach((ref Translation trans, ref Rotation rot, ref MoveForwardComponent moveForward) =>
        {           
           moveForward.forceSide = 0;
            if (moveForward.forceSide == 0) {
                moveForward.direction = 1f;                
                trans.Value += (float3)Vector3.down * moveForward.speed * Time.DeltaTime;               
            }
            else if (moveForward.forceSide == 1) {
                moveForward.direction = 1f;
                trans.Value += (float3)Vector3.right * moveForward.speed * Time.DeltaTime;              
            }
            else if (moveForward.forceSide == 2) { 
                moveForward.direction = 1f;
                trans.Value += (float3)Vector3.up * moveForward.speed * Time.DeltaTime;
            }
            else
            {
                moveForward.direction = 1f;
                trans.Value += (float3)Vector3.left * moveForward.speed * Time.DeltaTime;
            }
                
            var pos = trans.Value;
            var x = trans.Value.x;
            var y = trans.Value.y;

            if (x > 9.5)
            {
                pos.x = -9.5f;
                trans.Value = pos;
            }
            if (x < -9.5)
            {
                pos.x = 9.5f;
                trans.Value = pos;
            }

            if (y > 5.5)
            {
                pos.y = -5.5f;
                trans.Value = pos;
            }
            if (y < -5.5f)
            {
                pos.y = 5.5f;
                trans.Value = pos;
            }
        });
    }
}

// Systems can also be multithreaded by inheriting from JobComponentSystem; 
// for more info see:
// https://docs.unity3d.com/Packages/com.unity.entities@0.1/manual/job_component_system.html
