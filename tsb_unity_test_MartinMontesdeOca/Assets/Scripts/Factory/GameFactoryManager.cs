using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFactoryManager : MonoBehaviour
{
    [SerializeField]
    private AsteroidConfiguration asteroidConfiguration;
    [SerializeField]
    private PowerUpConfiguration powerUpConfiguration;
    [SerializeField]
    private LaserConfiguration laserConfiguration;
    [SerializeField]
    private AsteroidSpawner asteroidSpawner;
    [SerializeField]
    private PowerUpSpawner powerUpSpawner;
    [SerializeField]
    private LaserSpawner laserSpawner;

    public const int ASTEROID = 0;
    public const int POWERUP = 1;
    public const int LASER = 2;

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
    }

    void Start()
    {
        
    }

    public void CreateObject(int type,string id,Vector3 position,Transform container)
    {
        switch (type)
        {
            case ASTEROID:
                gameFactory.CreateAsteroid(id, position, container);
                break;
            case POWERUP:
                gameFactory.CreatePowerUp(id);
                break;
            case LASER:
                gameFactory.CreateLaser(id, position, container);
                break;
        }
    }
}
