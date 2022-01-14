using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public float time;
    //public GameObject[] prefabs;
    //public Transform[] container;  
    float[] x_pos = new float[] { 0f, -12f, 0.5f, 15f };
    float[] y_pos = new float[] { 10f, 0f, -11f, -1f };
    string[] names = new string[] { "Big", "Medium", "Small", "Tiny" };
    private float xPos;
    private float yPos;
    private NativeList<Entity> asteroidArray;
    private bool enableSpaw = true;
    private Entity asteroid;
    //private NativeArray<Entity> asteroidArray;
    
    private int arrayIndex = 0;

    void Start()
    {
        asteroidArray = new NativeList<Entity>();
        //StartCoroutine(FireContinuously());
    }

    IEnumerator FireContinuously()
    {
        // When started, this coroutine enters the below while loop...
        while (true)
        {
            Debug.Log("FireContinuously");
            //enableSpaw = false;
            var randomPos = 0;// Random.Range(0, 4);
            var randomType = 0;// = Random.Range(0, 3);
            var randomId = 0;//Random.Range(1, 2);
            if (randomType == 0)
                randomId = 0;//Random.Range(1, 4);

            //Instantiate(prefabs[Random.Range(0, prefabs.Length)], new Vector3(x_pos[randomPos], y_pos[randomPos], 0), Quaternion.identity);
            if (randomPos == 0)
            {
                xPos = Random.Range(-7.48f, 7.79f);
                yPos = 10f;// y_pos[randomPos];
            }
            if (randomPos == 1)
            {
                xPos = x_pos[randomPos];
                yPos = Random.Range(4.45f, -4.07f);

            }

            //asteroid = GameFactoryManager.Instance.CreateObject(GameFactoryManager.ASTEROID, names[randomType] + randomId, new Vector3(xPos, yPos, 0), null);

            //GameFactoryManager.Instance.CreateObject(GameFactoryManager.POWERUP, "ray", new Vector3(xPos, yPos, 0), null);
            asteroid = GameFactoryManager.Instance.CreateObject(GameFactoryManager.ASTEROID, names[randomType] + randomId, new Vector3(xPos, yPos, 0), null);
            GameManager.Instance.entityManager.SetComponentData(asteroid, new Translation { Value = new Vector3(xPos, yPos, 0) });
            asteroidArray.Add(asteroid);
            yield return new WaitForSeconds(time);
            //CreateAsteroid(randomType, randomId, new Vector3(xPos, yPos, 0));
        }
    }

    public void CreateNewAsteroid()
    {
        if (asteroidArray.IsEmpty || asteroidArray.Length < 10)
        {
            //enableSpaw = false;
            var randomPos = 0;// Random.Range(0, 4);
            var randomType = 0;// = Random.Range(0, 3);
            var randomId = 0;//Random.Range(1, 2);
            if (randomType == 0)
                randomId = 0;//Random.Range(1, 4);

            //Instantiate(prefabs[Random.Range(0, prefabs.Length)], new Vector3(x_pos[randomPos], y_pos[randomPos], 0), Quaternion.identity);
            if (randomPos == 0)
            {
                xPos = Random.Range(-7.48f, 7.79f);
                yPos = 10f;// y_pos[randomPos];
            }
            if (randomPos == 1)
            {
                xPos = x_pos[randomPos];
                yPos = Random.Range(4.45f, -4.07f);

            }

            //asteroid = GameFactoryManager.Instance.CreateObject(GameFactoryManager.ASTEROID, names[randomType] + randomId, new Vector3(xPos, yPos, 0), null);
            //CreateAsteroid(randomType, randomId, new Vector3(xPos, yPos, 0));
            //GameFactoryManager.Instance.CreateObject(GameFactoryManager.POWERUP, "ray", new Vector3(xPos, yPos, 0), null);            
        }
    }

    public Entity CreateAsteroid()
    {
        var randomPos = 0;// Random.Range(0, 4);
        var randomType = 0;// = Random.Range(0, 3);
        var randomId = 0;//Random.Range(1, 2);
        if (randomType == 0)
            randomId = 0;//Random.Range(1, 4);

        //Instantiate(prefabs[Random.Range(0, prefabs.Length)], new Vector3(x_pos[randomPos], y_pos[randomPos], 0), Quaternion.identity);
        if (randomPos == 0)
        {
            xPos = UnityEngine.Random.Range(-7.48f, 7.79f);
            yPos = 10f;// y_pos[randomPos];
        }
        if (randomPos == 1)
        {
            xPos = x_pos[randomPos];
            yPos = UnityEngine.Random.Range(4.45f, -4.07f);

        }

        asteroid = GameFactoryManager.Instance.CreateObject(GameFactoryManager.ASTEROID, names[randomType] + randomId, new Vector3(xPos, yPos, 0), null);
        GameManager.Instance.entityManager.SetComponentData(asteroid, new Translation { Value = new Vector3(xPos, yPos, 0) });
        //asteroidArray.Add(asteroid);
        return asteroid;
    }

    public void DestroyAsteroid(Entity asteroid)
    {
        var index = asteroidArray.IndexOf<Entity, Entity>(asteroid);
        asteroidArray.RemoveAt(index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
