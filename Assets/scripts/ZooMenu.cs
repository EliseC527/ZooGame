using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZooMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public Transform player;
    public GameObject lionEnclosure;
    public GameObject tigerEnclosure;
    public GameObject elephantEnclosure;

    private bool isMenuOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleMenu();
        }

        if (isMenuOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMenu();
        }
    }

    void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        menuPanel.SetActive(isMenuOpen);
    }

    public void SpawnLionEnclosure()
    {
        SpawnEnclosure(lionEnclosure);
    }

    public void SpawnTigerEnclosure()
    {
        SpawnEnclosure(tigerEnclosure);
    }

    public void SpawnElephantEnclosure()
    {
        SpawnEnclosure(elephantEnclosure);
    }

    void SpawnEnclosure(GameObject enclosurePrefab)
    {
        if (enclosurePrefab == null || player == null)
        {
            return;
        }

        Vector3 spawnPosition = player.position + player.forward * 35f;
        Quaternion spawnRotation = Quaternion.LookRotation(player.forward);

        Instantiate(enclosurePrefab, spawnPosition, spawnRotation);
        CloseMenu();
    }

    void CloseMenu()
    {
        isMenuOpen = false;
        menuPanel.SetActive(false);
    }
}
