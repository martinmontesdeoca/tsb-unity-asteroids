using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

public class ShieldMovementSystem : ComponentSystem
{   
    protected override void OnUpdate()
    {       
        Entities.WithAll<ShieldTag>().ForEach((ref Translation trans, ref Rotation rot) =>
        {
            rot.Value = Quaternion.Euler(PlayerManager.Instance.GetPlayerTransform().rotation.eulerAngles);
            trans.Value = PlayerManager.Instance.GetPlayerTransform().position;
        });
    }
}
