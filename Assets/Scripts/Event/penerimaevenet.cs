using UnityEngine;


public class penerimaevenet : MonoBehaviour
{
      void OnEnable()
    {
        pemancarevent.OnTekanSpasi += Reaksi;
    }

    void OnDisable()
    {
        pemancarevent.OnTekanSpasi -= Reaksi;
    }

    void Reaksi()
    {
        Debug.Log("Penerima: aku dengar event spasi!");
    }
}
