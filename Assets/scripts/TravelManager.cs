using UnityEngine;

public class TravelManager : MonoBehaviour
{
    public void TravelTo(string destination)
    {
        if (ZooEconomy.Instance == null)
        {
            Debug.LogError("ZooEconomy instance not found!");
            return;
        }

        if (ZooEconomy.Instance.IsDestinationUnlocked(destination))
        {
            Debug.Log($"Traveling to {destination}...");
            SceneTransitionManager.Instance.LoadScene(destination, Vector2.zero); // You can customize spawn pos
        }
        else
        {
            Debug.Log($"Destination {destination} is locked! Reach a higher level to unlock.");
            // Show locked UI message here if you want
        }
    }
}
