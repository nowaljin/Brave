using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Referansları")]
    public GameObject dialoguePanel;
    public TMP_InputField chatInput;
    public TextMeshProUGUI chatLog;

    private void Start()
    {
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
    }

    public void StartConversation()
    {
        dialoguePanel.SetActive(true);
        chatInput.ActivateInputField();
    }

    public void OnSubmit(string message)
    {
        if (string.IsNullOrEmpty(message)) return;

        // Konsola yaz ki ben buradan görebileyim
        Debug.Log("MESSAGE_TO_HERMES: " + message);
        
        chatLog.text += "\nİlker: " + message;
        
        chatInput.text = "";
        chatInput.ActivateInputField();
    }
}
