using UnityEngine;

public class Talk : MonoBehaviour
{
    [Header("Referanslar")]
    public DialogueManager dialogueManager; 
    
    private bool isPlayerInRange = false;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (dialogueManager != null)
            {
                dialogueManager.StartConversation();
            }
            else
            {
                Debug.LogError("Talk script'indeki 'Dialogue Manager' kutusu boş!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    { 
        if (other.CompareTag("Player")) isPlayerInRange = true; 
    }

    private void OnTriggerExit2D(Collider2D other) 
    { 
        if (other.CompareTag("Player")) isPlayerInRange = false; 
    }
}
