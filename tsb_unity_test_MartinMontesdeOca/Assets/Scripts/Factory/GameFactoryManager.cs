using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Unity.Physics;

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
    }    
    
    public Entity CreateObject(int type,int id,Vector3 position)
    {        
        switch (type)
        {
            case ASTEROID:
                objectCreate = gameFactory.CreateAsteroid(id, position);                
                break;
            case POWERUP:                
                objectCreate = gameFactory.CreatePowerUp(id);                
                break;
            case LASER:
                objectCreate = gameFactory.CreateLaser(id, position);                
                break;
            case OVNI:                
                objectCreate = gameFactory.CreateOvni(id, position);               
                break;
            case SHIELD:
                SoundManager.Instance.PlaySound("sfx_shieldUp");
                objectCreate = gameFactory.CreateShield(id, position);               
                break;
        }

        enemyEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(objectCreate, settings);
        return GameManager.Instance.entityManager.Instantiate(enemyEntityPrefab);
    }

    public GameObject CreateObject2(int type, int id, Vector3 position)
    {
        switch (type)
        {
            case ASTEROID:
                objectCreate = gameFactory.CreateAsteroid(id, position);
                break;
            case POWERUP:
                objectCreate = gameFactory.CreatePowerUp(id);
                break;
            case LASER:
                objectCreate = gameFactory.CreateLaser(id, position);
                break;
            case OVNI:
                objectCreate = gameFactory.CreateOvni(id, position);
                break;
            case SHIELD:
                objectCreate = gameFactory.CreateShield(id, position);
                break;
        }

        
        return objectCreate;
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
}
