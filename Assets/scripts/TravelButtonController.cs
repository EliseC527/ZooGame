using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TravelButtonController : MonoBehaviour
{
    public string destinationName;  // Set in Inspector
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void Start()
    {
        UpdateButtonVisibility();
    }

    void OnEnable()
    {
        if (ZooEconomy.Instance != null)
            ZooEconomy.Instance.OnLevelUpEvent += UpdateButtonVisibility;

        // Also update when scene changes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        if (ZooEconomy.Instance != null)
            ZooEconomy.Instance.OnLevelUpEvent -= UpdateButtonVisibility;

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateButtonVisibility();
    }

    void UpdateButtonVisibility(int unused = 0)
    {
        if (ZooEconomy.Instance == null)
        {
            button.gameObject.SetActive(false);
            return;
        }

        // Hide button if this destination is the current active scene
        if (SceneManager.GetActiveScene().name == destinationName)
        {
            button.gameObject.SetActive(false);
            return;
        }

        bool unlocked = ZooEconomy.Instance.IsDestinationUnlocked(destinationName);
        button.gameObject.SetActive(unlocked);
    }
}
