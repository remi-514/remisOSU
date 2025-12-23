using UnityEngine;
using UnityEngine.InputSystem;

public class LongNoteObject : MonoBehaviour
{
    public Key keyToPress;
    private bool isHolding;
    private float noteEnterTime, noteExitTime;
    private float playerPressStartTime, playerPressEndTime;

    void Update()
    {
        if (Keyboard.current[keyToPress].wasPressedThisFrame)
        {
            isHolding = true;
            playerPressStartTime = Time.time;
        }

        if (isHolding && Keyboard.current[keyToPress].wasReleasedThisFrame)
        {
            isHolding = false;
            playerPressEndTime = Time.time;
        }

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Activator")) noteEnterTime = Time.time;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            noteExitTime = Time.time;

            float finalPressEnd = isHolding ? Time.time : playerPressEndTime;

            CalculateResult(finalPressEnd);
        }
    }

    private void CalculateResult(float pressEnd)
    {
        float targetDuration = noteExitTime - noteEnterTime;
        
        float actualPressStart = Mathf.Max(playerPressStartTime, noteEnterTime);
        float actualPressEnd = Mathf.Min(pressEnd, noteExitTime);
        
        float playerHoldDuration = Mathf.Max(0, actualPressEnd - actualPressStart);
        float accuracy = (targetDuration > 0) ? Mathf.Clamp01(playerHoldDuration / targetDuration) : 0;

        GameManager.instance.longNoteHit(accuracy);
    }
}