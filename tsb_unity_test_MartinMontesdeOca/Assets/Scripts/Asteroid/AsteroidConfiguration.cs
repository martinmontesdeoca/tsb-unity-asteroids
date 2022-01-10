using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Asteroids configuration")]
public class AsteroidConfiguration : ScriptableObject
{
    [SerializeField]
    private Asteroid[] asteroids;
    private Dictionary<string, Asteroid> asteroidsArray;

    void Awake()
    {
        asteroidsArray = new Dictionary<string, Asteroid>();
        foreach (var asteroid in asteroids)
        {
            asteroidsArray.Add(asteroid.Id, asteroid);
        }
    }

    public Asteroid GetAsteroidFromId(string id)
    {
        if (!asteroidsArray.TryGetValue(id, out var asteroid))
        {
            throw new Exception("The Asteroid id not exits");
        }

        return asteroid;
    }
}
