using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class AsteroidFactory : MonoBehaviour
{
    private readonly AsteroidConfiguration asteroidConfiguration;
    public AsteroidFactory(AsteroidConfiguration asteroidConfiguration)
    {
        this.asteroidConfiguration = asteroidConfiguration;
    }

    public Entity Create(int id, Vector3 position)
    {        
        return asteroidConfiguration.GetAsteroidFromId(id);
    }
}
