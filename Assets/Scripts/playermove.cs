using UnityEngine;
using UnityEngine.InputSystem;

public class lari : MonoBehaviour, IDamageable
{
    public float kecepatan = 5f;

    public int poinCoin = 26;

    public int damagePlayer = 20;

    public int hp = 100;

    private Vector2 arahGerak;

    public int skor = 0;

    public GameManager gameManager;

    void OnMove(InputValue value)
    {
        arahGerak = value.Get<Vector2>();
    }

    void Update()
    {
        Vector3 arah = new Vector3(
            arahGerak.x,
            arahGerak.y,
            0
        );

        transform.position += arah * kecepatan * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);

            skor += poinCoin;

            Debug.Log("Skor : " + skor);

            if (gameManager != null)
            {
                gameManager.AmbilKoin();
            }
        }

        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.KenaDamage(damagePlayer);
            }
        }
    }

    public void KenaDamage(int jumlah)
    {
        hp -= jumlah;

        Debug.Log(
            "Player kena " + jumlah +
            " damage. Sisa HP: " + hp
        );

        if (hp <= 0)
        {
            Mati();
        }
    }

    void Mati()
    {
        Debug.Log("Player mati!");

        gameObject.SetActive(false);
    }
}