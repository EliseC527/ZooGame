using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ZooEconomy : MonoBehaviour
{
    public static ZooEconomy Instance;

    public int playerMoney = 0;
    public int zooLevelPoints = 0;
    public int currentLevel = 1;

    public float payoutInterval = 60f;
    private float payoutTimer = 0f;

    public TextMeshProUGUI moneyLevelText;

    public delegate void LevelUpHandler(int newLevel);
    public event LevelUpHandler OnLevelUpEvent;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Subscribe to scene load event to re-assign UI reference
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the TextMeshProUGUI component named or tagged appropriately in the new scene
        moneyLevelText = FindObjectOfType<TextMeshProUGUI>();

        // Immediately update UI to reflect current values after scene load
        UpdateUI();
    }

    void Update()
    {
        payoutTimer += Time.deltaTime;
        if (payoutTimer >= payoutInterval)
        {
            payoutTimer = 0f;
            CalculateZooIncomeAndLevel();
        }

        UpdateUI();
    }

    void CalculateZooIncomeAndLevel()
    {
        var zooAnimals = AnimalTracker.Instance.GetAnimalsInScene("ZooScene");

        int totalPoints = 0;

        foreach (var animal in zooAnimals)
        {
            if (AnimalValueTable.AnimalPointValues.TryGetValue(animal.animalType, out int value))
                totalPoints += value;
        }

        playerMoney += totalPoints;
        zooLevelPoints += totalPoints;

        int newLevel = CalculateLevelFromXP(zooLevelPoints);
        if (newLevel > currentLevel)
        {
            currentLevel = newLevel;
            OnLevelUp(newLevel);
        }

        Debug.Log($"[PAYMENT] +${totalPoints}, Money: {playerMoney}, XP: {zooLevelPoints}, Level: {currentLevel}");
    }

    void UpdateUI()
    {
        if (moneyLevelText != null)
        {
            moneyLevelText.text = $"$ {playerMoney}\nLevel {currentLevel}";
        }
    }

    int CalculateLevelFromXP(int xp)
    {
        if (xp >= 20) return 5;
        if (xp >= 10) return 4;
        if (xp >= 5) return 3;
        if (xp >= 1) return 2;
        return 1;
    }

    void OnLevelUp(int level)
    {
        Debug.Log($"[LEVEL UP] Reached Level {level}");
        OnLevelUpEvent?.Invoke(level);  // Notify subscribers safely
    }

    public bool IsDestinationUnlocked(string destination)
    {
        switch (destination)
        {
            case "Zoo":
                return true;
            case "Town":
                return currentLevel >= 2;
            case "Savannah":
                return true;
            default:
                return false;
        }
    }
}
