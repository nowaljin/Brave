using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName = "Bob the Hero";
    public int age = 25;
    public int characterLevel = 80;
    public float moveSpeed =2.5f;
    public bool gameOver = true;

    public Rigidbody2D rb;

    private void Awake()
    {
        GetPlayerInfo();
    }

    private void Start()
    {

        GetPlayerInfo();
    }

    private void Update()
    {
        GetPlayerInfo()
    }

    private void GetPlayerInfo()
    {
        Debug.Log("Player Name: " + playerName);
        Debug.Log("Age: " + age);
        Debug.Log("Character Level: " + characterLevel);
        Debug.Log("Move Speed: " + moveSpeed);
        Debug.Log("Game Over: " + gameOver);
    }

}
