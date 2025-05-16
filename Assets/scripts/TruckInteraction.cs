using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class TruckInteraction : MonoBehaviour
{
    public Button townButton;
    public Button savannahButton;
    public Button zooButton;

    public GameObject panel;

    private bool isInRange = false;

    private readonly Dictionary<string, string> destinationToScene = new Dictionary<string, string>()
    {
        { "Town", "TownScene" },
        { "Savannah", "SavannahScene" },
        { "Zoo", "ZooScene" }
    };

    void Start()
    {
        if (panel == null)
        {
            Debug.LogError("Panel is not assigned in TruckInteraction!");
            return;
        }
        panel.SetActive(false);

        if (townButton == null) Debug.LogError("townButton not assigned in Inspector!");
        else townButton.onClick.AddListener(() => StartCoroutine(LoadSceneDelayed(destinationToScene["Town"])));

        if (savannahButton == null) Debug.LogError("savannahButton not assigned in Inspector!");
        else savannahButton.onClick.AddListener(() => StartCoroutine(LoadSceneDelayed(destinationToScene["Savannah"])));

        if (zooButton == null) Debug.LogError("zooButton not assigned in Inspector!");
        else zooButton.onClick.AddListener(() => StartCoroutine(LoadSceneDelayed(destinationToScene["Zoo"])));
    }

    IEnumerator LoadSceneDelayed(string sceneName)
    {
        Debug.Log("Preparing to load scene: " + sceneName);
        yield return null; // Wait one frame to allow PersistentHolder to register changes
        Debug.Log("Loading scene now: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void ShowMenu()
    {
        UpdateButtonVisibility();
        panel.SetActive(true);
        Debug.Log("Menu shown");
    }

    public void HideMenu()
    {
        panel.SetActive(false);
        Debug.Log("Menu hidden");
    }

    void UpdateButtonVisibility()
    {
        if (ZooEconomy.Instance == null)
        {
            Debug.LogError("ZooEconomy.Instance is null! Cannot update buttons.");
            SetAllButtonsActive(false);
            return;
        }

        if (townButton == null || savannahButton == null || zooButton == null)
        {
            Debug.LogError("One or more buttons are null! Check Inspector assignments.");
            return;
        }

        string currentScene = SceneManager.GetActiveScene().name;

        townButton.gameObject.SetActive(
            ZooEconomy.Instance.IsDestinationUnlocked("Town") &&
            currentScene != destinationToScene["Town"]);

        savannahButton.gameObject.SetActive(
            ZooEconomy.Instance.IsDestinationUnlocked("Savannah") &&
            currentScene != destinationToScene["Savannah"]);

        zooButton.gameObject.SetActive(
            currentScene != destinationToScene["Zoo"]);
    }

    private void SetAllButtonsActive(bool active)
    {
        if (townButton != null) townButton.gameObject.SetActive(active);
        if (savannahButton != null) savannahButton.gameObject.SetActive(active);
        if (zooButton != null) zooButton.gameObject.SetActive(active);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player entered truck range");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isInRange = false;
            HideMenu();
            Debug.Log("Player left truck range");
        }
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player pressed 'E' near truck");
            ShowMenu();
        }
    }
}
