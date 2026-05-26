using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer sr;

    private float redColourDuration = 1;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage()
    {

        sr.color = Color.red;
        Invoke(nameof(TurnWhite), redColourDuration);

    }

    private void TurnWhite()
    { 
        sr.color = Color.white;
    }
}
