using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;



[AlwaysUpdateSystem]
class AsteroidSpawnSystem : ComponentSystem
{
    private Entity asteroid;
    private float m_ElapsedTime;       
    private NativeList<Entity> asteroidArray;
    float[] x_pos = new float[] { 0f, -12f, 0.5f, 15f };
    float[] y_pos = new float[] { 10f, 0f, -11f, -1f };
    string[] names = new string[] { "Big", "Medium", "Small", "Tiny" };
    private float xPos;
    private float yPos;
    private int sideCount = 0;

    protected override void OnCreate()
    {
        base.OnCreate();
        asteroidArray = new NativeList<Entity>(10, Allocator.Persistent);        
    }       

    protected override void OnUpdate()
    {
        m_ElapsedTime += Time.DeltaTime;
        if (m_ElapsedTime > 2f)
        {
            m_ElapsedTime = 0;
            if (asteroidArray.Length < 10)
            {
                CreateAsteroid();
            }
        }    

        Entities.WithAll<DestroyEntityComponent>().ForEach((ref Translation trans, ref DestroyEntityComponent destroyEntity) =>
        {
            if (destroyEntity.entity != Entity.Null)
            {
                Debug.Log("Asteroid Destroy ---");
                DestroyAsteroid(destroyEntity.entity, trans.Value);
            }
        });
    }

    private void DestroyAsteroid(Entity asteroid, float3 position)
    {
        GameFactoryManager.Instance.CreateNotEntityObject(GameFactoryManager.EXPLOSION, 0, new Vector3(position.x,position.y));
        var index = asteroidArray.IndexOf<Entity, Entity>(asteroid);        
        //PostUpdateCommands.DestroyEntity(asteroidArray[index]);
        PostUpdateCommands.DestroyEntity(asteroid);
        if (index != -1)asteroidArray.RemoveAt(index);

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

    private void CreateAsteroid()
    {
        var randomPos = sideCount;// UnityEngine.Random.Range(0, 4);
        var randomType = UnityEngine.Random.Range(0, 3);
        var randomId = UnityEngine.Random.Range(1, 2);
        if (randomType == 0)
            randomId = UnityEngine.Random.Range(1, 4);

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
        asteroid = GameFactoryManager.Instance.CreateObject(GameFactoryManager.ASTEROID, names[randomType] + randomId, position);
        GameManager.Instance.entityManager.SetComponentData(asteroid, new Translation { Value = position });
        GameManager.Instance.entityManager.SetComponentData(asteroid, new MoveComponent { forceSide = /*SetForceSide(position, asteroid)*/sideCount, speed = UnityEngine.Random.Range(1f, 3f) });
        asteroidArray.Add(asteroid);
        sideCount++;
        if (sideCount > 3) sideCount = 0;
        // asteroidArray.Dispose();

        Debug.Log("Asteroid Create --- asteroidArray: " + asteroidArray.Length);
    }
}
//}
