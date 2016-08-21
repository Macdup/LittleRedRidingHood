using UnityEngine;
using System.Collections;

public class Brush : MonoBehaviour
{

    public Vector2 brushSize;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, brushSize);
    }

}
