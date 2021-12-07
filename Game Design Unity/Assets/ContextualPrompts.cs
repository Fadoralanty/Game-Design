using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ContextualPrompts : MonoBehaviour
{
    public UnityEvent ShowPrompt;
    public UnityEvent HidePrompt;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowPrompt.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HidePrompt.Invoke();
        }
    }

}
