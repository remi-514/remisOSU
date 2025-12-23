using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public Key keyToPress;
    
    void Update()
    {
        if (canBePressed && Keyboard.current[keyToPress].wasPressedThisFrame)
        {
            float distance = Mathf.Abs(transform.position.y + 3.3f);

            gameObject.SetActive(false);

            if (distance <= 0.1f)
            {
                GameManager.instance.PerfectHit();
            }
            else if (distance <= 0.2f)
            {
                GameManager.instance.GoodHit();
            }
            else
            {
                GameManager.instance.NormalHit();
            }
        }

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Activator") && gameObject.activeSelf)
        {
            canBePressed = false;
            GameManager.instance.NoteMissed();
        }
    }
}