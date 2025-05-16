using UnityEngine;

public class Animal : MonoBehaviour
{
    public string animalType = "Elephant"; // Can be "Ostrich", "Zebra", etc.

    void Start()
    {
        AnimalTracker.Instance.RegisterAnimal(this, "ZooScene"); // Fixed scene name
    }

    void OnDestroy()
    {
        AnimalTracker.Instance.UnregisterAnimal(this, "ZooScene");
    }
}
