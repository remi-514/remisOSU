using UnityEngine;
using UnityEngine.InputSystem;

public class BeatScroller : MonoBehaviour
{
    public float Speed = 5f;
    public bool hasStarted;
    
    void Update()
    {
        if (hasStarted)
        {
            transform.position -= new Vector3(0f, Speed * Time.deltaTime, 0f);
        }
    }
}