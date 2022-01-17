using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using System.Numerics;

/// <summary>
/// System for destroying Enemy Entities and the Player
/// </summary>
public class HitAsteroidSystem : ComponentSystem
{  
    float minDistance = 0.5f;   
    protected override void OnCreate()
    {
        base.OnCreate();        
    }

    protected override void OnUpdate()
    {      
        if (GameManager.Instance.IsResetGame) return;

        float3 playerPosition = (float3)PlayerManager.Instance.GetPlayerTransform().position;
        Entities.WithAll<AsteroidTag>().ForEach((Entity asteroid, ref Translation asteroidPos) =>
        {         
            if (math.distance(asteroidPos.Value, playerPosition) <= minDistance)
            {                
                GameManager.Instance.entityManager.SetComponentData(asteroid, new DestroyEntityComponent { entity = asteroid });
                GameManager.Instance.ResetGame();                
            }
            
            float3 enemyPosition = asteroidPos.Value;
            Entities.WithAll<BulletTag>().ForEach((Entity bullet, ref Translation bulletPos) =>
            {               
                if (math.distance(enemyPosition, bulletPos.Value) <= minDistance)
                {
                    GameManager.Instance.entityManager.SetComponentData(asteroid, new DestroyEntityComponent { entity = asteroid });                    
                }
            });

            Entities.WithAll<ShieldTag>().ForEach((Entity bullet, ref Translation shield) =>
            {
                if (math.distance(enemyPosition, shield.Value) <= minDistance)
                {
                    GameManager.Instance.entityManager.SetComponentData(asteroid, new DestroyEntityComponent { entity = asteroid });                   
                }
            });
        });
    }
}
