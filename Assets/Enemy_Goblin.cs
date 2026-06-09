using UnityEngine;

public class Enemy_Goblin : Enemy
{
    private void Awake()
    {
        moveSpeed = 10;
    }



    [ContextMenu ("Steal gold!")]
    private void StealMoney()
    {
        Debug.Log(enemyName + " steal's gold from player");
    }

}
