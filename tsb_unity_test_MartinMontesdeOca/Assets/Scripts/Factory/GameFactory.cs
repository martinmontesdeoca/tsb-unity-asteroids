using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
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

    public Entity CreateAsteroid(int id, Vector3 position)
    {
        return asteroidFactory.Create(id, position);
    }

    public Entity CreatePowerUp(int id)
    {
        return powerUpFactory.Create(id);
    }

    public Entity CreateLaser(int id, Vector3 position)
    {
        return laserFactory.Create(id, position);
    }

    public Entity CreateOvni(int id, Vector3 position)
    {
        return ovniFactory.Create(id);
    }

    public GameObject CreateExplosion(int id, Vector3 position)
    {
        return explosionFactory.Create(id, position).gameObject;
    }

    public Entity CreateShield(int id, Vector3 position)
    {
        return shieldFactory.Create(id, position);
    }
}
