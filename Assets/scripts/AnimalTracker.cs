using System.Collections.Generic;
using UnityEngine;

public class AnimalTracker : MonoBehaviour
{
    public static AnimalTracker Instance;

    private Dictionary<string, List<Animal>> sceneAnimals = new Dictionary<string, List<Animal>>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterAnimal(Animal animal, string sceneName)
    {
        if (!sceneAnimals.ContainsKey(sceneName))
            sceneAnimals[sceneName] = new List<Animal>();

        if (!sceneAnimals[sceneName].Contains(animal))
            sceneAnimals[sceneName].Add(animal);
    }

    public void UnregisterAnimal(Animal animal, string sceneName)
    {
        if (sceneAnimals.ContainsKey(sceneName))
            sceneAnimals[sceneName].Remove(animal);
    }

    public List<Animal> GetAnimalsInScene(string sceneName)
    {
        return sceneAnimals.ContainsKey(sceneName) ? sceneAnimals[sceneName] : new List<Animal>();
    }
}
