using UnityEngine;

public class BucketHeadZombie : Enemy
{
   public bool bucket = true;

    public override void Serang()
    {
        Debug.Log("Buckethead Gigit");
    }
}
