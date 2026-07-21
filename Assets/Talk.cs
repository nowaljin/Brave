using UnityEngine;

public class Talk : MonoBehaviour
{
    [Header("Referanslar")]
    public DialogueManager dialogueManager; 
    
    [Header("Diyaloglar")]
    [TextArea(3, 10)]
    public string[] sentences; 

    private bool isPlayerInRange = false;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (dialogueManager != null)
            {
                dialogueManager.StartDialogue(sentences);
            }
            else
            {
                Debug.LogError("Talk script'indeki 'Dialogue Manager' kutusu boş! Lütfen o objeyi sürükleyip oraya bırak.");
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
