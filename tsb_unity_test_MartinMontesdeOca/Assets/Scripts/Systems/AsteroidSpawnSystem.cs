using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;


namespace Assets.Scripts.Systems
{
    [UpdateBefore(typeof(TransformSystemGroup))]
    class AsteroidSpawnSystem : ComponentSystem
    {
        private float m_ElapsedTime;
        private float xPos;
        private float yPos;
        private NativeList<Entity> asteroidArray;

        protected override void OnCreate()
        {
            base.OnCreate();
            asteroidArray = new NativeList<Entity>(0, Allocator.Persistent);
            
        }

        protected override void OnUpdate()
        {
            
            m_ElapsedTime += Time.DeltaTime;
            if (m_ElapsedTime > 2f)
            {
                m_ElapsedTime = 0;
                if (asteroidArray.Length < 10)
                {
                    //enableSpaw = false;


                    //asteroid = GameFactoryManager.Instance.CreateObject(GameFactoryManager.ASTEROID, names[randomType] + randomId, new Vector3(xPos, yPos, 0), null);
                    var asteroid = GameFactoryManager.Instance.asteroidSpawner.CreateAsteroid();
                    asteroidArray.Add(asteroid);
                    Debug.Log("AsteroidSpawnSystem: " + asteroidArray.Length);
                    //GameFactoryManager.Instance.CreateObject(GameFactoryManager.POWERUP, "ray", new Vector3(xPos, yPos, 0), null);            
                }

            }
        }

        public static void DestroyAsteroid(Entity asteroid)
        {
            //asteroidArray.RemoveAt(asteroid);
            GameFactoryManager.Instance.asteroidSpawner.DestroyAsteroid(asteroid);
        }
    }
}
