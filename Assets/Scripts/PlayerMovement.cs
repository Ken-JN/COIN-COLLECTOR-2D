using UnityEngine;
using UnityEngine.InputSystem; // WAJIB untuk Input System

public class PlayerMovement : MonoBehaviour, IDamageable
{
    [Header("Pergerakan")]
    public float kecepatan = 5f;
    private Vector2 arahGerak; // Nilai dari action "Move"

    [Header("Sistem Skor")]
    public int skor = 0;

    [Header("Health")]
    public int hp = 100;

    [Header("Attack")]
    public int damageSerang = 30;
    public float jarakSerang = 1.5f;

    // Dipanggil OTOMATIS oleh komponen Player Input
    // saat action "Move" pada asset InputSystem_Actions aktif.
    void OnMove(InputValue value)
    {
        // TODO: ambil nilai Vector2 dari input, simpan ke arahGerak
        arahGerak = value.Get<Vector2>();
    }

    // Dipanggil saat Player melakukan action "Fire" di Input System
    void OnFire()
    {
        Serang();
    }

    void Serang()
    {
        Debug.Log("<color=green>Player Menyerang!</color>");
        
        // Cari semua Enemy dalam radius serang
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
        // TODO: gerakkan objek memakai arahGerak.
        // Ingat kalikan kecepatan DAN Time.deltaTime!

        Vector3 arah = new Vector3(arahGerak.x, arahGerak.y, 0);
        transform.position += arah * kecepatan * Time.deltaTime;
    }

    // Dipanggil otomatis saat Player menyentuh objek ber-Trigger (Tugas 3)
    void OnTriggerEnter2D(Collider2D other)
    {
     // TODO: cek apakah yang disentuh punya tag "Coin"
        if (other.CompareTag("Coin"))
        {
            // TODO: Hancurkan koin yang tersentuh
            Destroy(other.gameObject);

            // TODO: Tambah skor sebanyak 1
            skor++;

            // TODO: Tampilkan skor ke Console
            Debug.Log($"<color=yellow>Skor Kamu Saat Ini: {skor}</color>");

            // Beritahu GameManager bahwa koin telah diambil
            GameManager gm = Object.FindFirstObjectByType<GameManager>();
            if (gm != null)
           {
                gm.AmbilKoin();
            }
        }
    }

    public void KenaDamage(int jumlah)
    {
        hp -= jumlah;
        Debug.Log($"<color=red>Player kena {jumlah} damage! HP: {hp}</color>");

        if (hp <= 0)
        {
            Debug.Log("<color=red>GAME OVER!</color>");
            Destroy(gameObject);
        }
    }
}