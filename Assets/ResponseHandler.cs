using UnityEngine;
using TMPro;

public class ResponseHandler : MonoBehaviour
{
    [Header("UI Referansları")]
    public TextMeshProUGUI chatLog;

    // Benim sana vereceğim yanıtları buraya kopyalayıp yapıştırarak veya 
    // bir metin dosyası üzerinden okuyarak oyun içine aktarabilirsin.
    public void SetHermesResponse(string response)
    {
        if (chatLog != null)
        {
            chatLog.text += "\n<color=yellow>Hermes: </color>" + response;
        }
        else
        {
            Debug.LogError("ChatLog referansı boş! Lütfen Inspector'dan atayın.");
        }
    }
}
