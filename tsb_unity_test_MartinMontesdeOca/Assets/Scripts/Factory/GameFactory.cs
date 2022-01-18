using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFactory
{
    private readonly AsteroidFactory asteroidFactory;
    private readonly PowerUpFactory powerUpFactory;
    private readonly LaserFactory laserFactory;
    private readonly OvniFactory ovniFactory;
    private readonly ExplosionFactory explosionFactory;
    private readonly ShieldFactory shieldFactory;
    public GameFactory(AsteroidFactory asteroidFactory, PowerUpFactory powerUpFactory, LaserFactory laserFactory, OvniFactory ovniFactory, ExplosionFactory explosionFactory, ShieldFactory shieldFactory)
    {
        this.asteroidFactory = asteroidFactory;
        this.powerUpFactory = powerUpFactory;
        this.laserFactory = laserFactory;
        this.ovniFactory = ovniFactory;
        this.explosionFactory = explosionFactory;
        this.shieldFactory = shieldFactory;
    }

    public GameObject CreateAsteroid(int id, Vector3 position)
    {
        return asteroidFactory.Create(id, position).gameObject;
    }

    public GameObject CreatePowerUp(int id)
    {
        return powerUpFactory.Create(id).gameObject;
    }

    public GameObject CreateLaser(int id, Vector3 position)
    {
        return laserFactory.Create(id, position).gameObject;
    }

    public GameObject CreateOvni(int id, Vector3 position)
    {
        return ovniFactory.Create(id).gameObject;
    }

    public GameObject CreateExplosion(int id, Vector3 position)
    {
        return explosionFactory.Create(id, position).gameObject;
    }

    public GameObject CreateShield(int id, Vector3 position)
    {
        return shieldFactory.Create(id, position).gameObject;
    }
}
