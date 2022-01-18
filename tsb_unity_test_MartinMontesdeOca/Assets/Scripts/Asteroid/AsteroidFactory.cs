using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFactory : MonoBehaviour
{
    private readonly AsteroidConfiguration asteroidConfiguration;
    public AsteroidFactory(AsteroidConfiguration asteroidConfiguration)
    {
        this.asteroidConfiguration = asteroidConfiguration;
    }

    public Asteroid Create(int id, Vector3 position)
    {        
        return asteroidConfiguration.GetAsteroidFromId(id);
    }
}
