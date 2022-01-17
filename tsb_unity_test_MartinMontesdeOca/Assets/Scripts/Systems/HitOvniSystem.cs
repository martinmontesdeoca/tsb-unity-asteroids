using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

/// <summary>
/// System for destroying Enemy Entities and the Player
/// </summary>
public class HitOvniSystem : ComponentSystem
{  
    float minDistance = 0.6f;   
    protected override void OnCreate()
    {
        base.OnCreate();        
    }

    protected override void OnUpdate()
    {
        if (GameManager.Instance.IsResetGame) return;

        float3 playerPosition = (float3)PlayerManager.Instance.GetPlayerTransform().position;
        Entities.WithAll<OvniTag>().ForEach((Entity ovni, ref Translation ovniPos) =>
        {         
            if (math.distance(ovniPos.Value, playerPosition) <= minDistance)
            {
                GameManager.Instance.entityManager.SetComponentData(ovni, new DestroyEntityComponent { entity = ovni });
                GameManager.Instance.ResetGame();                
            }
            
            float3 enemyPosition = ovniPos.Value;
            Entities.WithAll<BulletTag>().ForEach((Entity bullet, ref Translation bulletPos) =>
            {               
                if (math.distance(enemyPosition, bulletPos.Value) <= minDistance)
                {
                    GameManager.Instance.entityManager.SetComponentData(ovni, new DestroyEntityComponent { entity = ovni });                    
                }
            }); 
        });
    }
}
