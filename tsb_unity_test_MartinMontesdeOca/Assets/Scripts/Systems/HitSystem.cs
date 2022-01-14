using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Assets.Scripts.Systems;
/// <summary>
/// System for destroying Enemy Entities and the Player
/// </summary>
public class HitSystem : ComponentSystem
{  
    float minDistance = 0.4f;
   
   

    protected override void OnCreate()
    {
        base.OnCreate();        
    }

    protected override void OnUpdate()
    {      
        if (GameManager.IsGameOver())
        {
            return;
        }
       
        float3 playerPosition = (float3)GameManager.Instance.GetPlayerTransform().position;
        Entities.WithAll<EnemyTag>().ForEach((Entity asteroid, ref Translation asteroidPos) =>
        {         
            if (math.distance(asteroidPos.Value, playerPosition) <= minDistance)
            {
                // create an explosion at the enemy
                // FXManager.Instance.CreateExplosion(enemyPos.Value);

                // create an explosion at the player (comment this out for stress test)
                // FXManager.Instance.CreateExplosion(playerPosition);

                // end the game (comment this out for stress test)
                //   GameManager.EndGame();

                // 7 safely remove the enemy Enity using an EntityCommandBuffer
                GameFactoryManager.Instance.asteroidSpawner.DestroyAsteroid(asteroid);
                AsteroidSpawnSystem.DestroyAsteroid(asteroid);
                PostUpdateCommands.DestroyEntity(asteroid);
            }
            
            float3 enemyPosition = asteroidPos.Value;
            Entities.WithAll<BulletTag>().ForEach((Entity bullet, ref Translation bulletPos) =>
            {               
                if (math.distance(enemyPosition, bulletPos.Value) <= minDistance)
                {                   
                    PostUpdateCommands.DestroyEntity(asteroid);
                    PostUpdateCommands.DestroyEntity(bullet);
                }
            }); 
        });
    }
}
