using UnityEngine;

public class flagzombie : Enemy
{
    public bool flag = true;

    public override void Serang()
    {
        Debug.Log("FlagZombie Gigit");
    }
}