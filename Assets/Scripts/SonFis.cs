using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonFis : MonoBehaviour
{
     public GameObject MevcutSoket;
    public string SoketRengi;
    [SerializeField] private gameManager _gameManager;
    bool PozDegis, Secildi, SoketeOtur;
    GameObject hareketPozisyonu;
    GameObject SoketinKendisi;
    
    public void HareketEt(string islem,GameObject soket,GameObject gidilecekObje=null)
    {
        switch (islem)
        {
            case "secim":
                hareketPozisyonu = gidilecekObje;
                Secildi = true;
                break;
            case "pozisyonDegis":
                SoketinKendisi = soket;
                hareketPozisyonu = gidilecekObje;
                PozDegis = true;
                break;
            case "soketeOtur":
                SoketinKendisi = soket;
                SoketeOtur = true;
                break;
        }
    }
    
    void Update()
    {
        if (Secildi)
        {
            transform.position = Vector3.Lerp(transform.position, hareketPozisyonu.transform.position, 0.040f);
            if (Vector3.Distance(transform.position, hareketPozisyonu.transform.position) < 0.010f)
            {
                Secildi = false;
            }
        }
        if (PozDegis)
        {
            transform.position = Vector3.Lerp(transform.position, hareketPozisyonu.transform.position, 0.040f);
            if (Vector3.Distance(transform.position, hareketPozisyonu.transform.position) < 0.010f)
            {
                PozDegis = false;
                SoketeOtur = true;
            }
        }
        if (SoketeOtur)
        {
            transform.position = Vector3.Lerp(transform.position, SoketinKendisi.transform.position, 0.040f);
            if (Vector3.Distance(transform.position, SoketinKendisi.transform.position) < 0.010f)
            {
                
                _gameManager.hareketvar = false;
                MevcutSoket = SoketinKendisi;
                _gameManager.fisleriKontrolEt();
                SoketeOtur = false;
                
            }
        }
    }
}
