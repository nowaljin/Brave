using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_InputField chatInput;
    public TextMeshProUGUI chatLog;

    private string bridgeUrl = "http://localhost:8080";

    private void Start()
    {
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
        StartCoroutine(ListenForHermes());
    }

    public void StartConversation()
    {
        dialoguePanel.SetActive(true);
        chatInput.ActivateInputField();
    }

    public void OnSubmit(string message)
    {
        if (string.IsNullOrWhiteSpace(message)) return;
        chatLog.text += "\nİlker: " + message;
        StartCoroutine(SendToHermes(message));
        chatInput.text = "";
        chatInput.ActivateInputField();
    }

    IEnumerator SendToHermes(string message)
    {
        WWWForm form = new WWWForm();
        form.AddField("msg", message);
        using (UnityWebRequest www = UnityWebRequest.Post(bridgeUrl, form))
        {
            yield return www.SendWebRequest();
        }
    }

    IEnumerator ListenForHermes()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            using (UnityWebRequest www = UnityWebRequest.Get(bridgeUrl))
            {
                yield return www.SendWebRequest();
                if (www.result == UnityWebRequest.Result.Success)
                {
                    string response = www.downloadHandler.text;
                    if (!string.IsNullOrEmpty(response) && response != "WAIT")
                    {
                        chatLog.text += "\n<color=yellow>Hermes: </color>" + response;
                    }
                }
            }
        }
    }
}
