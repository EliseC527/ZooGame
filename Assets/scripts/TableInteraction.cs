using UnityEngine;

public class TableInteraction : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject TilemapCamera;
    public GameObject uiMenu;  // your tile selection UI

    private bool inEditMode = false;

    void OnMouseDown()
    {
        if (!inEditMode)
            EnterEditMode();
        else
            ExitEditMode();
    }

    void EnterEditMode()
    {
        inEditMode = true;
        MainCamera.SetActive(false);
        TilemapCamera.SetActive(true);
        uiMenu.SetActive(true);
        // Lock player movement, disable 3D controls here if needed
    }

    void ExitEditMode()
    {
        inEditMode = false;
        MainCamera.SetActive(true);
        TilemapCamera.SetActive(false);
        uiMenu.SetActive(false);
        // Enable 3D controls here
    }
}
