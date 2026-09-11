using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IDamageable
{
    [Header("Pergerakan")]
    public float kecepatan = 5f;
    private Vector2 arahGerak;

    [Header("Sistem Skor")]
    public int skor = 0;

    [Header("Health")]
    public int hp = 100;
    public float waktuKebalDetik = 0.5f; // jeda kebal setelah kena damage
    private float waktuKenaTerakhir = -999f;

    [Header("Attack")]
    public int damageSerang = 30;
    public float jarakSerang = 1.5f;

    void OnMove(InputValue value)
    {
        arahGerak = value.Get<Vector2>();
    }

    void OnFire()
    {
        Serang();
    }

    void Serang()
    {
        Debug.Log("<color=green>Player Menyerang!</color>");

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, jarakSerang);

        foreach (Collider2D hit in hits)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable != null && hit.CompareTag("Enemy"))
            {
                damageable.KenaDamage(damageSerang);
                Debug.Log("<color=green>Kena Enemy!</color>");
            }
        }
    }

    void Update()
    {
        Vector3 arah = new Vector3(arahGerak.x, arahGerak.y, 0);
        transform.position += arah * kecepatan * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            skor++;
            Debug.Log($"<color=yellow>Skor Kamu Saat Ini: {skor}</color>");

            GameManager gm = Object.FindFirstObjectByType<GameManager>();
            if (gm != null)
            {
                gm.AmbilKoin();
            }
        }
    }

    public void KenaDamage(int jumlah)
    {
        // JANGAN proses damage kalau masih dalam periode kebal
        if (Time.time < waktuKenaTerakhir + waktuKebalDetik)
        {
            return;
        }

        waktuKenaTerakhir = Time.time;
        hp -= jumlah;
        Debug.Log($"<color=red>Player kena {jumlah} damage! HP: {hp}</color>");

        if (hp <= 0)
        {
            Debug.Log("<color=red>GAME OVER!</color>");
            Destroy(gameObject);
        }
    }
}