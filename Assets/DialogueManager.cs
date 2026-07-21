using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Referansları")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    private string[] currentSentences;
    private int index;

    private void Start()
    {
        // Başlangıçta paneli kapalı tut
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
    }

    // Bu metod NPC'den (Talk.cs üzerinden) çağrılacak
    public void StartDialogue(string[] sentences)
    {
        currentSentences = sentences;
        index = 0;
        dialoguePanel.SetActive(true);
        dialogueText.text = currentSentences[index];
    }

    // Oyuncu bir tuşa bastığında (örn: Space) metinler arası geçiş yapacak
    public void NextSentence()
    {
        if (index < currentSentences.Length - 1)
        {
            index++;
            dialogueText.text = currentSentences[index];
        }
        else
        {
            // Diyalog bittiğinde paneli kapat
            dialoguePanel.SetActive(false);
        }
    }
}
