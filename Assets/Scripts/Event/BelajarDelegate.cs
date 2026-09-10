using System;
using UnityEngine;

public class BelajarDelegate : MonoBehaviour
{

    delegate void AksiSederhana();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ContohAction1();
        ContohAction2();
        ContohAction3();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ContohAction1()
    {
        AksiSederhana aksi = TulisHalo;
        aksi();
    }

    void ContohAction2()
    {
        AksiSederhana aksi = TulisHalo;
        aksi += TulisDunia;
        aksi();
    }

    void ContohAction3()
    {
        Action aksi = TulisHalo;
        aksi += TulisSigma;
        aksi();

    }

    void TulisDunia()
    {
        Debug.Log("Dunia");
    }

    void TulisHalo()
    {
        Debug.Log("Halo");
    }

    void TulisSigma()
    {
        Debug.Log("Sigma");
    }
}
