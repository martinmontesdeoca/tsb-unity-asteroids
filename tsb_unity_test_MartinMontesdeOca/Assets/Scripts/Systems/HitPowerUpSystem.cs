using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

/// <summary>
/// System for destroying Enemy Entities and the Player
/// </summary>
public class HitPowerUpSystem : ComponentSystem
{  
    float minDistance = 0.8f;   
    protected override void OnCreate()
    {
        base.OnCreate();        
    }

    protected override void OnUpdate()
    {
        if (GameManager.Instance.IsResetGame) return;

        float3 playerPosition = (float3)PlayerManager.Instance.GetPlayerTransform().position;
        Entities.WithAll<PowerUpTag>().ForEach((Entity powerUp, ref Translation powerUpPos,ref PowerUpDataComponent powerUpTag) =>
        {         
            if (math.distance(powerUpPos.Value, playerPosition) <= minDistance)
            {
                if(powerUpTag.id == 0)
                    PlayerManager.Instance.SetLaserType();
                else
                    GameFactoryManager.Instance.CreateObject(GameFactoryManager.SHIELD, 0.ToString(), PlayerManager.Instance.GetPlayerTransform().position);

                GameManager.Instance.entityManager.SetComponentData(powerUp, new DestroyEntityComponent { entity = powerUp });               
            }                       
        });       
    }

}
