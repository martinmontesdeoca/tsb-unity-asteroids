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
    private OvniConfiguration ovniConfiguration;
    [SerializeField]
    private ExplosionConfiguration explosionConfiguration;
    [SerializeField]
    private ShieldConfiguration shieldConfiguration;

    public const int ASTEROID = 0;
    public const int POWERUP = 1;
    public const int LASER = 2;
    public const int OVNI = 3;
    public const int EXPLOSION = 4;
    public const int SHIELD = 5;

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
        var ovniFactory = new OvniFactory(Instantiate(ovniConfiguration));
        var explosionFactory = new ExplosionFactory(Instantiate(explosionConfiguration));
        var shieldFactory = new ShieldFactory(Instantiate(shieldConfiguration));
        gameFactory = new GameFactory(asteroidFactory, powerUpFactory, laserFactory, ovniFactory, explosionFactory, shieldFactory);                   
        settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, new BlobAssetStore());
        random = new Unity.Mathematics.Random(56);
    }    
    
    public Entity CreateObject(int type,string id,Vector3 position)
    {        
        switch (type)
        {
            case ASTEROID:
                objectCreate = gameFactory.CreateAsteroid(id, position);
                enemyEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(objectCreate, settings);
                objectEntity = SpawnWave(position);
                break;
            case POWERUP:
                objectCreate = gameFactory.CreatePowerUp(id);
                enemyEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(objectCreate, settings);
                objectEntity = SpawnPowerUp();
                break;
            case LASER:
                objectCreate = gameFactory.CreateLaser(id, position);
                enemyEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(objectCreate, settings);
                SpawnLaser();
                break;
            case OVNI:
                objectCreate = gameFactory.CreateOvni(id, position);
                enemyEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(objectCreate, settings);
                objectEntity = SpawnOvni();
                break;
            case SHIELD:
                objectCreate = gameFactory.CreateShield(int.Parse(id), position);
                enemyEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(objectCreate, settings);
                objectEntity = SpawnShield();
                break;
        }

        return objectEntity;
    }

    public GameObject CreateNotEntityObject(int type, int id, Vector3 position)
    {
        switch (type)
        {            
            case EXPLOSION:
                objectCreate = gameFactory.CreateExplosion(id, position);                
                break;
        }

        return objectCreate;
    }

    private Entity SpawnPowerUp()
    {

        return GameManager.Instance.entityManager.Instantiate(enemyEntityPrefab);
    }

    private Entity SpawnExplosion()
    {

        return GameManager.Instance.entityManager.Instantiate(enemyEntityPrefab);
    }

    private Entity SpawnOvni()
    {

        return GameManager.Instance.entityManager.Instantiate(enemyEntityPrefab);
    }

    private Entity SpawnShield()
    {

        return GameManager.Instance.entityManager.Instantiate(enemyEntityPrefab);
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

            GameManager.Instance.entityManager.SetComponentData(enemyArray[i], new Translation { Value = PlayerManager.Instance.GetPlayerTransform().position });
            GameManager.Instance.entityManager.SetComponentData(enemyArray[i], new Rotation { Value = Quaternion.Euler(PlayerManager.Instance.GetPlayerTransform().rotation.eulerAngles) }); ;

            GameManager.Instance.entityManager.SetComponentData(enemyArray[i], new MoveLaserComponent { speed = Random.Range(10f, 12f) });
            GameManager.Instance.entityManager.SetComponentData(enemyArray[i], new MoveLaserComponent { playerPosition = PlayerManager.Instance.GetPlayerTransform().position });            
        }
       
        enemyArray.Dispose();
    }    
}
