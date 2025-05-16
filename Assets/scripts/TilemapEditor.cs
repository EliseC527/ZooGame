using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapEditor : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase pathTile;  // assign your path tile here

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // left click
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = tilemap.WorldToCell(mouseWorldPos);
            tilemap.SetTile(gridPosition, pathTile);
        }
    }
}
