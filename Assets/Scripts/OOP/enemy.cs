using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp = 100;

    public float ms = 2f;

    [SerializeField] private int damageSaatTabrakan = 20;

    protected Transform player;

    [Header("Pengaturan State Machine")]
    [SerializeField] private float jarakDeteksi = 6f;
    [SerializeField] private float jarakSerang = 1.2f;
    [SerializeField] private float jedaSerang = 1f;

    [SerializeField] private float radiusPatrol = 3f;

    private Vector2 titikAwal;
    private Vector2 tujuanPatrol;

    public static event Action<Enemy> OnZombieMati;

    private StateZombie state = StateZombie.IDLE;
    private float waktuSerangTerakhir;

    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        titikAwal = transform.position;
        PilihTujuanPatrolBaru();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            KenaDamage(100);
        }

        PeriksaTransisi();

        switch (state)
        {
            case StateZombie.IDLE:
                PerilakuIdle();
                break;

            case StateZombie.PATROL:
                PerilakuPatrol();
                break;

            case StateZombie.CHASE:
                PerilakuChase();
                break;

            case StateZombie.ATTACK:
                PerilakuAttack();
                break;
        }
    }

    void PerilakuIdle()
    {
    }

    void PerilakuPatrol()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            tujuanPatrol,
            ms * 0.5f * Time.deltaTime
        );

        if (Vector2.Distance(
            transform.position,
            tujuanPatrol) < 0.1f)
        {
            PilihTujuanPatrolBaru();
        }
    }

    void PilihTujuanPatrolBaru()
    {
        Vector2 acak = UnityEngine.Random.insideUnitCircle * radiusPatrol;
        tujuanPatrol = titikAwal + acak;
    }

    void PerilakuChase()
    {
        Kejar();
    }

    void PerilakuAttack()
    {
        if (Time.time >= waktuSerangTerakhir + jedaSerang)
        {
            Serang();
            waktuSerangTerakhir = Time.time;
        }
    }

    public float JarakKePlayer()
    {
        if (player == null)
        {
            return Mathf.Infinity;
        }

        return Vector2.Distance(
            transform.position,
            player.position
        );
    }

    void PeriksaTransisi()
    {
        float jarak = JarakKePlayer();

        if (jarak <= jarakSerang)
        {
            state = StateZombie.ATTACK;
        }
        else if (jarak <= jarakDeteksi)
        {
            state = StateZombie.CHASE;
        }
        else
        {
            state = StateZombie.PATROL;
        }
    }

    public void Kejar()
    {
        if (player == null)
        {
            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            ms * Time.deltaTime
        );
    }

    public virtual void Serang()
    {
        Debug.Log("Enemy Menyerang");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable playerScript = other.GetComponent<IDamageable>();

            if (playerScript != null)
            {
                playerScript.KenaDamage(damageSaatTabrakan);
            }
        }
    }

    public void KenaDamage(int jumlah)
    {
        hp -= jumlah;

        Debug.Log(
            name + " kena " + jumlah +
            " damage. Sisa HP: " + hp
        );

        if (hp <= 0)
        {
            Mati();
        }
    }

    protected virtual void Mati()
    {
        Debug.Log("Zombie Mati!");

        OnZombieMati?.Invoke(this);

        Destroy(gameObject);
    }
}