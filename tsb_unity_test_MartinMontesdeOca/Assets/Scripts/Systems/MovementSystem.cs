using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

public class MovementSystem : ComponentSystem
{   
    protected override void OnUpdate()
    {       
        Entities.WithAll<MoveComponent>().ForEach((ref Translation trans, ref Rotation rot, ref MoveComponent moveForward) =>
        {              
            if (moveForward.forceSide == 0) {                         
                trans.Value += (float3)Vector3.down * moveForward.speed * Time.DeltaTime;                
            }else if (moveForward.forceSide == 1) {              
                trans.Value += (float3)Vector3.right * moveForward.speed * Time.DeltaTime;              
            }else if (moveForward.forceSide == 2) {                
                trans.Value += (float3)Vector3.up * moveForward.speed * Time.DeltaTime;
            }else{               
                trans.Value += (float3)Vector3.left * moveForward.speed * Time.DeltaTime;
            }

            rot.Value = math.mul(rot.Value, quaternion.RotateZ(math.radians(moveForward.rotation * Time.DeltaTime)));

            var pos = trans.Value;
            var x = trans.Value.x;
            var y = trans.Value.y;

            if (x > 9.5)
            {
                pos.x = -9.5f;
                pos.y = UnityEngine.Random.Range(-1, -11);
                trans.Value = pos;
            }
            if (x < -9.5)
            {
                pos.x = 9.5f;
                pos.y = UnityEngine.Random.Range(0, 10);
                trans.Value = pos;
            }

            if (y > 5.5)
            {
                pos.x = UnityEngine.Random.Range(0,-12);
                pos.y = -5.5f;
                trans.Value = pos;
            }
            if (y < -5.5f)
            {
                pos.x = UnityEngine.Random.Range(0.5f, 15);
                pos.y = 5.5f;
                trans.Value = pos;
            }
        });
    }
}
