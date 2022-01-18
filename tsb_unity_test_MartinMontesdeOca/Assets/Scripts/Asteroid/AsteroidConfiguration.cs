using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Asteroids configuration")]
public class AsteroidConfiguration : ScriptableObject
{
    [SerializeField]
    private Asteroid[] asteroids;
    private Dictionary<int, Asteroid> asteroidsArray;

    void Awake()
    {
        asteroidsArray = new Dictionary<int, Asteroid>();
        foreach (var asteroid in asteroids)
        {
            asteroidsArray.Add(asteroid.Id, asteroid);
        }
    }

    public Asteroid GetAsteroidFromId(int id)
    {
        if (!asteroidsArray.TryGetValue(id, out var asteroid))
        {
            throw new Exception("The Asteroid id not exits");
        }

        return asteroid;
    }
}
