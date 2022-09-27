using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameManager : MonoBehaviour
{
    GameObject SeciliObje;
    GameObject SeciliSoket;
    public bool hareketvar;
    [Header("LEVEL AYARLARI")]
    [SerializeField] private GameObject[] CarpismaKontrolObjeleri;
    [SerializeField] private GameObject[] Fisler;
    [SerializeField] private int hedefSoketSayisi;
    [SerializeField] private List<bool> carpismaDurumlari;
    [SerializeField] private int HamleHakki;
    [SerializeField] private HingeJoint[] KabloKopmaNoktalari;
    [Header("DÝÐER OBJELER")]
    [SerializeField] private GameObject[] Isýklar;
    [Header("UI AYARLARI")]
    [SerializeField] private GameObject kontrolPaneli;
    [SerializeField] private TextMeshProUGUI kontrolText;
    [SerializeField] private TextMeshProUGUI HamleHakkiText;
    int tamamlanmaSayisi;
    SonFis sonfis;
    int carpmaSayisi;
    void Start()
    {
        HamleHakkiText.text = "MOVES : "+HamleHakki.ToString();
        for (int i = 0; i < hedefSoketSayisi-1; i++)
        {
            carpismaDurumlari.Add(false);
        }
        
        Invoke("baglantiNoktlariniAyarla", 2f);
    }
    void baglantiNoktlariniAyarla()
    {
        foreach (var item in KabloKopmaNoktalari)
        {
            item.breakForce = 600;
            item.breakTorque = 500;
        }
    }
    public void fisleriKontrolEt()
    {
        foreach (var item in Fisler)
        {
            if(item.GetComponent<SonFis>().MevcutSoket.name== item.GetComponent<SonFis>().SoketRengi)
            {
                tamamlanmaSayisi++;
                Debug.Log(tamamlanmaSayisi);
            }
        }
        if (tamamlanmaSayisi == hedefSoketSayisi)
        {
            Debug.Log("eþleþme yapýldý");
            foreach (var item in CarpismaKontrolObjeleri)
            {
                item.SetActive(true);
            }
            StartCoroutine(carpismaVarmi());
        }
        else
        {
            if(HamleHakki<= 0)
            {
                Debug.Log("hamle hakki bitti"); 
            }
        }
        tamamlanmaSayisi = 0;
    }
    IEnumerator carpismaVarmi()
    {
        Isýklar[0].SetActive(false);
        Isýklar[1].SetActive(true);
       
        kontrolPaneli.SetActive(true);
        kontrolText.text = "KONTROL EDÝLÝYOR...";
        yield return new WaitForSeconds(4f);
        foreach (var item in carpismaDurumlari)
        {
            if (item)
            {
                carpmaSayisi++;
            }
        }
        if (carpmaSayisi == carpismaDurumlari.Count)
        {
            Isýklar[1].SetActive(false);
            Isýklar[2].SetActive(true);
            kontrolText.text = "KAZANDIN";
        }
        else
        {
            Isýklar[1].SetActive(false);
            Isýklar[0].SetActive(true);
            kontrolText.text = "ÇARPIÞMA VAR";
            Invoke("paneliKapat", 2f);
            foreach (var item in CarpismaKontrolObjeleri)
            {
                item.SetActive(false);
            }
            if (HamleHakki <= 0)
            {
                Debug.Log("hamle hakki bitti");
            }
        }
        carpmaSayisi = 0;

    }
    void paneliKapat()
    {
        kontrolPaneli.SetActive(false);
    }
    public void kaybettin()
    {
        Debug.Log("kaybettin");
    }

    public void carpismayiKontrolet(int CarpismaIndex,bool durum)
    {
        carpismaDurumlari[CarpismaIndex] = durum;
   
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out RaycastHit hit,100))
            {
                if (hit.collider != null)
                {
                    if(SeciliObje==null && !hareketvar)
                    {
                        if (hit.collider.CompareTag("SariFis") || hit.collider.CompareTag("MaviFis") || hit.collider.CompareTag("KirmiziFis"))
                        {
                             sonfis = hit.collider.GetComponent<SonFis>();
                            sonfis.HareketEt("secim"
                                , sonfis.MevcutSoket, sonfis.MevcutSoket.GetComponent<soket>().hareketPozisyonu);
                            SeciliObje = hit.collider.gameObject;
                            SeciliSoket = sonfis.MevcutSoket;
                            hareketvar = true;
                            

                        }
                    }
                    if (hit.collider.CompareTag("soket"))
                    {
                        if (SeciliObje != null && !hit.collider.GetComponent<soket>().doluluk && SeciliSoket!= hit.collider.gameObject)
                        {
                            SeciliSoket.GetComponent<soket>().doluluk = false;
                            soket _soket = hit.collider.GetComponent<soket>();
                            sonfis.HareketEt("pozisyonDegis", hit.collider.gameObject,_soket.hareketPozisyonu);
                            _soket.doluluk = true;
                            SeciliObje =null;
                            SeciliSoket = null;
                            HamleHakki--;
                            HamleHakkiText.text = "MOVES : " + HamleHakki.ToString();

                        }
                        else if(SeciliSoket== hit.collider.gameObject)
                        {
                            sonfis.HareketEt("soketeOtur",hit.collider.gameObject);
                            SeciliObje = null;
                            SeciliSoket = null;
                            hareketvar = true;
                        }
                    }
                }

            }
        }
    }
}
