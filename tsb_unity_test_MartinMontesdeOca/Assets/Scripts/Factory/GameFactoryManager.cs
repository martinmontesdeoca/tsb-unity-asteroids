using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Unity.Physics;
//using Unity.U2D.Entities.Physics;
using Random = UnityEngine.Random;
public class GameFactoryManager : MonoBehaviour
{
    [SerializeField]
    private AsteroidConfiguration asteroidConfiguration;
    [SerializeField]
    private PowerUpConfiguration powerUpConfiguration;
    [SerializeField]
    private LaserConfiguration laserConfiguration;
    [SerializeField]
    public AsteroidSpawner asteroidSpawner;
    [SerializeField]
    private PowerUpSpawner powerUpSpawner;
    [SerializeField]
    private LaserSpawner laserSpawner;

    public const int ASTEROID = 0;
    public const int POWERUP = 1;
    public const int LASER = 2;
   
    // Entity prefab
    private Entity enemyEntityPrefab;
    private Entity objectEntity;
    private EntityManager entityManager;
    private GameObjectConversionSettings settings;
    private GameObject objectCreate;
    private GameFactory gameFactory;    
    private static GameFactoryManager _instance;
    public static GameFactoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameFactoryManager is null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        var asteroidFactory = new AsteroidFactory(Instantiate(asteroidConfiguration));
        var powerUpFactory = new PowerUpFactory(Instantiate(powerUpConfiguration));
        var laserFactory = new LaserFactory(Instantiate(laserConfiguration));
        gameFactory = new GameFactory(asteroidFactory, powerUpFactory, laserFactory);       
        //entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        //var world = World.DefaultGameObjectInjectionWorld;
        //var conversionSystem = world.GetExistingSystem<GameObjectConversionSystem>();       
        settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, new BlobAssetStore());
        random = new Unity.Mathematics.Random(56);
    }

    void Start()
    {
        

        
    }

    public Entity CreateObject(int type,string id,Vector3 position,Transform container)
    {        
        switch (type)
        {
            case ASTEROID:
                objectCreate = gameFactory.CreateAsteroid(id, position, container);
                enemyEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(objectCreate, settings);
                objectEntity = SpawnWave(position);
                break;
            case POWERUP:
                objectCreate = gameFactory.CreatePowerUp(id);
                enemyEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(objectCreate, settings);
                SpawnPowerUp();
                break;
            case LASER:
                objectCreate = gameFactory.CreateLaser(id, position, container);
                enemyEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(objectCreate, settings);
                SpawnLaser();
                break;
        }

        return objectEntity;
    }

    private void SpawnPowerUp()
    {
        
        var powerUpEntity = GameManager.Instance.entityManager.Instantiate(enemyEntityPrefab);
        GameManager.Instance.entityManager.SetComponentData(powerUpEntity, new Translation { Value = new float3(random.NextFloat(-5f, 5f), random.NextFloat(-5f, 5f), 0) });
    }

    float[] x_pos = new float[] { 0f, -12f, 0.5f, 15f };
    float[] y_pos = new float[] { 10f, 0f, -11f, -1f };
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
        //entityManager.SetComponentData(asteroid, new MoveForwardComponent { forceSide = side });
    }

    private Unity.Mathematics.Random random;
    private Entity SpawnWave(Vector3 position)
    {                      
        return GameManager.Instance.entityManager.Instantiate(enemyEntityPrefab);
        //GameManager.Instance.entityManager.SetComponentData(asteroidEntity, new Translation { Value = position });    
    }    
   
    private void SpawnLaser()
    {      
        NativeArray<Entity> enemyArray = new NativeArray<Entity>(1, Allocator.Temp);

        // 2 loop through NativeArray
        for (int i = 0; i < enemyArray.Length; i++)
        {           
            enemyArray[i] = GameManager.Instance.entityManager.Instantiate(enemyEntityPrefab);

            GameManager.Instance.entityManager.SetComponentData(enemyArray[i], new Translation { Value = GameManager.Instance.GetPlayerTransform().position });
            GameManager.Instance.entityManager.SetComponentData(enemyArray[i], new Rotation { Value = Quaternion.Euler(GameManager.Instance.GetPlayerTransform().rotation.eulerAngles) }); ;

            GameManager.Instance.entityManager.SetComponentData(enemyArray[i], new MoveLaserComponent { speed = Random.Range(10f, 12f) });
            GameManager.Instance.entityManager.SetComponentData(enemyArray[i], new MoveLaserComponent { playerPosition = GameManager.Instance.GetPlayerTransform().position });            
        }
       
        enemyArray.Dispose();
    }    
}
