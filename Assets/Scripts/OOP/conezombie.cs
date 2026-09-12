using UnityEngine;

public class conezombie : Enemy
{
    public bool cone = true;

    public override void Serang()
    {
        Debug.Log("Cone Gigit");
    }
}
