using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFactory
{
    private readonly AsteroidFactory asteroidFactory;
    private readonly PowerUpFactory powerUpFactory;
    private readonly LaserFactory laserFactory;
    public GameFactory(AsteroidFactory asteroidFactory, PowerUpFactory powerUpFactory, LaserFactory laserFactory)
    {
        this.asteroidFactory = asteroidFactory;
        this.powerUpFactory = powerUpFactory;
        this.laserFactory = laserFactory;
    }

    public Asteroid CreateAsteroid(string id, Vector3 position, Transform container)
    {
        return asteroidFactory.Create(id, position, container);
    }

    public PowerUp CreatePowerUp(string id)
    {
        return powerUpFactory.Create(id);
    }

    public Laser CreateLaser(string id, Vector3 position, Transform container)
    {
        return laserFactory.Create(id, position, container);
    }
}
