// GridManager.cs
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float cellSize = 1f;
    public Color lineColor = Color.gray;

    private void OnDrawGizmos()
    {
        Gizmos.color = lineColor;
        for (int x = 0; x <= width; x++)
        {
            Vector3 from = transform.position + new Vector3(x * cellSize, 0, 0);
            Vector3 to = from + new Vector3(0, 0, height * cellSize);
            Gizmos.DrawLine(from, to);
        }

        for (int z = 0; z <= height; z++)
        {
            Vector3 from = transform.position + new Vector3(0, 0, z * cellSize);
            Vector3 to = from + new Vector3(width * cellSize, 0, 0);
            Gizmos.DrawLine(from, to);
        }
    }
}
