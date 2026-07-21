using UnityEngine;

public class Talk : MonoBehaviour
{
    [Header("Konuşma Ayarları")]
    [SerializeField] private string npcName = "Hermes";

    public bool isPlayerInRange = false;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartConversation();
        }
    }

    private void StartConversation()
    {
        Debug.Log($"[{npcName}] ile konuşma başlatıldı!");
        // Oyun içi konuşma UI'sı burada tetiklenebilir.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log($"[{npcName}]'nin yanına geldin. E tuşuna bas.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log($"[{npcName}]'nin yanından uzaklaştın.");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
}
