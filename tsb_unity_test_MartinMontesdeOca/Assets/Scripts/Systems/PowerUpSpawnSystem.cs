using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;



[AlwaysUpdateSystem]
class PowerUpSpawnSystem : ComponentSystem
{
    private Entity asteroid;
    private float m_ElapsedTime;       
    private NativeList<Entity> powerUpArray;
    float[] x_pos = new float[] { 0f, -12f, 0.5f, 15f };
    float[] y_pos = new float[] { 10f, 0f, -11f, -1f };
    string[] names = new string[] { "basic", "Medium", "Small", "Tiny" };
    private float xPos;
    private float yPos;
    private int sideCount = 0;

    protected override void OnCreate()
    {
        base.OnCreate();
        powerUpArray = new NativeList<Entity>(2, Allocator.Persistent);        
    }       

    protected override void OnUpdate()
    {
        m_ElapsedTime += Time.DeltaTime;
        if (m_ElapsedTime > 1f)
        {
            m_ElapsedTime = 0;
            if (powerUpArray.Length < 2)
            {
                CreatePowerUp();
            }
        }    

        Entities.WithAll<PowerUpTag>().ForEach((ref DestroyEntityComponent destroyEntity) =>
        {
            if (destroyEntity.entity != Entity.Null)
            {
                
                Debug.Log("PowerUp Destroy ---");
                DestroyPowerUp(destroyEntity.entity);
            }
        });
    }

private void DestroyPowerUp(Entity powerUp)
    {
        var index = powerUpArray.IndexOf<Entity, Entity>(powerUp);        
        //PostUpdateCommands.DestroyEntity(asteroidArray[index]);
        PostUpdateCommands.DestroyEntity(powerUp);
        if(index!=-1)powerUpArray.RemoveAt(index);        
    }

    private int SetForceSide(Vector3 position, Entity asteroid)
    {
        var side = 0;

        if (position.x == 0f)
            side = 0;
        else if (position.x == -12f)
            side = 1;
        else if (position.x == 0.5f)
            side = 2;
        else
            side = 3;

        return side;

    }

    private void CreatePowerUp()
    {
        var randomPos = sideCount;// UnityEngine.Random.Range(0, 4);
        var id = UnityEngine.Random.Range(0, 3);
        var randomId = UnityEngine.Random.Range(0, 10);
       
        if (randomPos == 0)
        {
            xPos = UnityEngine.Random.Range(-7.48f, 7.79f);
            yPos = -10f;// y_pos[randomPos];
        }
        if (randomPos == 1)
        {
            xPos = x_pos[randomPos];
            yPos = UnityEngine.Random.Range(4.45f, -4.07f);

        }
        if (randomPos == 2)
        {
            xPos = UnityEngine.Random.Range(-7.48f, 7.79f);
            yPos = 10f;// y_pos[randomPos];

        }
        if (randomPos == 3)
        {
            xPos = x_pos[randomPos];
            yPos = UnityEngine.Random.Range(4.45f, -4.07f);

        }

        var position = new Vector3(xPos, yPos, 0);
        id = randomId < 5 ? id = 0 : id = 1;
        asteroid = GameFactoryManager.Instance.CreateObject(GameFactoryManager.POWERUP, id.ToString(), position);
        GameManager.Instance.entityManager.SetComponentData(asteroid, new PowerUpDataComponent { id = id });
        GameManager.Instance.entityManager.SetComponentData(asteroid, new Translation { Value = position });
        GameManager.Instance.entityManager.SetComponentData(asteroid, new MoveComponent { forceSide = /*SetForceSide(position, asteroid)*/sideCount, speed = UnityEngine.Random.Range(1f, 3f) });
        powerUpArray.Add(asteroid);
        sideCount++;
        if (sideCount > 3) sideCount = 0;
        // asteroidArray.Dispose();

        Debug.Log("PowerUp Create --- asteroidArray: " + powerUpArray.Length);
    }
}
//}
