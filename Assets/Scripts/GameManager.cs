using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int totalKoin;
    private int koinTerkumpul = 0;

    [SerializeField] private int skor = 0;

   


    void Start()
    {
        // TODO: hitung jumlah koin di scene saat mulai
        totalKoin = GameObject.FindGameObjectsWithTag("Coin").Length;
        Debug.Log("Total koin yang harus dikumpulkan: " + totalKoin);
    }

    public void AmbilKoin()
    {
        koinTerkumpul++;

        // TODO: jika koinTerkumpul == totalKoin, panggil Menang()
        if (koinTerkumpul == totalKoin)
        {
            Menang();
        }
    }

    void Menang()
    {

        Debug.Log($"<color=green>KAMU MENANG!</color>");
    }
     void OnEnable()
    {
        Enemy.OnZombieMati += TambahSkorSaatZombieMati;
    }

    void OnDisable()
    {
        Enemy.OnZombieMati -= TambahSkorSaatZombieMati;
    }

    void TambahSkorSaatZombieMati(Enemy zombieYangMati)
    {
        skor += 10;
        Debug.Log("Skor: " + skor);
    }
}