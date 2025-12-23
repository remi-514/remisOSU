using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonManager : MonoBehaviour
{
    public Key keyToPress;

    private SpriteRenderer sr;

    [Range(0f, 1f)]
    public float idleAlpha = 0.3f;

    [Range(0f, 1f)]
    public float pressedAlpha = 1f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        SetAlpha(idleAlpha);
    }

    void Update()
    {
        if (Keyboard.current[keyToPress].isPressed)
        {
            SetAlpha(pressedAlpha);
        }
        else
        {
            SetAlpha(idleAlpha);
        }
    }

    void SetAlpha(float a)
    {
        Color c = sr.color;
        c.a = a;
        sr.color = c;
    }
}
