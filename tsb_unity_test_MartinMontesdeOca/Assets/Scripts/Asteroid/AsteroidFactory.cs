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

    public Asteroid Create(string id, Vector3 position, Transform container)
    {
        var asteroid = asteroidConfiguration.GetAsteroidFromId(id);
        if (!container)
            return asteroid;// Instantiate(asteroid,position,Quaternion.identity);
        else
            return asteroid;// Instantiate(asteroid, position, Quaternion.identity, container);
    }
}
