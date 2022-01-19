using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

[AlwaysUpdateSystem]
public class HitOvniSystem : ComponentSystem
{  
    float minDistance = 1f;
    float minDistanceToShoot = 50f;
    private float m_ElapsedTime = 0;
    float minDistanceShield = 1.5f;
    protected override void OnCreate()
    {
        base.OnCreate();        
    }

    protected override void OnUpdate()
    {
        if (GameManager.Instance.IsResetGame) return;

        float3 playerPosition = (float3)PlayerManager.Instance.GetPlayerTransform().position;
        Entities.WithAll<OvniTag>().ForEach((Entity ovni, ref Translation position, ref Rotation rotation) =>
        {
            if (math.distance(position.Value, playerPosition) <= minDistanceToShoot)
            {
                m_ElapsedTime += Time.DeltaTime;
                if (m_ElapsedTime > 2f)
                {
                    m_ElapsedTime = 0;
                    GameManager.Instance.entityManager.SetComponentData(ovni, new OvniShootComponent { shoot = true, position = position.Value, rotation = rotation.Value });

                }else
                    GameManager.Instance.entityManager.SetComponentData(ovni, new OvniShootComponent { shoot = false, position = position.Value, rotation = rotation.Value });
            }
            
            if (math.distance(position.Value, playerPosition) <= minDistance)
            {
                GameManager.Instance.entityManager.SetComponentData(ovni, new DestroyEntityComponent { entity = ovni });
                GameManager.Instance.ResetGame();                
            }
            
            float3 ovniPosition = position.Value;
            Entities.WithAll<BulletTag>().ForEach((Entity bullet, ref Translation bulletPos) =>
            {               
                if (math.distance(ovniPosition, bulletPos.Value) <= minDistance)
                {
                    GameManager.Instance.entityManager.SetComponentData(ovni, new DestroyEntityComponent { entity = ovni });                    
                }
            });

            Entities.WithAll<ShieldTag>().ForEach((Entity bullet, ref Translation shield) =>
            {
                if (math.distance(ovniPosition, shield.Value) <= minDistanceShield)
                {
                    GameManager.Instance.entityManager.SetComponentData(ovni, new DestroyEntityComponent { entity = ovni });
                }
            });
        });
        
        Entities.WithAll<OvniLaserTag>().ForEach((Entity bullet, ref Translation bulletPos) =>
        {
            if (math.distance(playerPosition, bulletPos.Value) <= minDistance)
            {
                GameManager.Instance.entityManager.SetComponentData(bullet, new DestroyEntityComponent { entity = bullet });
                GameManager.Instance.ResetGame();
            }
        });
    }
}
