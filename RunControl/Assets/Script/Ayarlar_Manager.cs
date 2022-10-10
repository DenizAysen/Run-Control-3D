using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Deniz;
public class Ayarlar_Manager : MonoBehaviour
{
    public AudioSource ButonSes;
    public Slider MenuSes;
    public Slider MenuFx;
    public Slider OyunSes;
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _VeriYonetim = new VeriYonetimi();
    [Header("-------DIL VERILERI")]
    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();
    List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public TextMeshProUGUI[] TextObjeleri;

    [Header("-----------DIL TERCIHI OBJELERI")]
    public TextMeshProUGUI DilText;
    public Button[] DilButonlari;
#pragma warning disable IDE0052 // Okunmamýþ özel üyeleri kaldýr
    int AktifDilIndex = 0;
#pragma warning restore IDE0052 // Okunmamýþ özel üyeleri kaldýr
    void Start()
    {
        ButonSes.volume = _BellekYonetim.VeriOku_f("MenuFx");
        MenuSes.value = _BellekYonetim.VeriOku_f("MenuSes");
        MenuFx.value = _BellekYonetim.VeriOku_f("MenuFx");
        OyunSes.value = _BellekYonetim.VeriOku_f("OyunSes");

       // _BellekYonetim.VeriKaydet_string("Dil", "EN");

        _VeriYonetim.Dil_Load();
        _DilOkunanVeriler = _VeriYonetim.DilVerileriListeyiAktar();
        _DilVerileriAnaObje.Add(_DilOkunanVeriler[4]);
        DilTercihiYonetimi();
        DilDurumunuKontrolEt();
    }
    void DilTercihiYonetimi()
    {
        if (_BellekYonetim.VeriOku_s("Dil") == "TR")
        {
            for (int i = 0; i < TextObjeleri.Length; i++)
            {
                TextObjeleri[i].text = _DilVerileriAnaObje[0]._DilVerileri_TR[i].Metin;
            }
        }
        else
        {
            for (int i = 0; i < TextObjeleri.Length; i++)
            {
                TextObjeleri[i].text = _DilVerileriAnaObje[0]._DilVerileri_EN[i].Metin;
            }
        }
    }
    public void SesAyarla(string HangiAyar)
    {
        switch (HangiAyar)
        {
            case "menuses":
                
                 _BellekYonetim.VeriKaydet_float("MenuSes", MenuSes.value);
                break;
            case "menufx":
                
                _BellekYonetim.VeriKaydet_float("MenuFx", MenuFx.value);
                break;
            case "oyunses":
              
                _BellekYonetim.VeriKaydet_float("OyunSes", OyunSes.value);
                break;
        }
    }
    public void GeriDon()
    {
        ButonSes.Play();
        SceneManager.LoadScene(0);
    }
    void DilDurumunuKontrolEt()
    {
        if(_BellekYonetim.VeriOku_s("Dil")== "TR")
        {
            AktifDilIndex = 0;
            DilButonlari[0].interactable = false;
            DilText.text = "TURKCE";
        }
        else
        {
            AktifDilIndex = 1;
            DilButonlari[1].interactable = false;
            DilText.text = "ENGLISH";
        }
    }
    public void DilDegistir(string Yon)
    {
        if(Yon == "ileri")
        {
            AktifDilIndex = 1;
            DilButonlari[1].interactable = false;
            DilText.text = "ENGLISH";
            DilButonlari[0].interactable = true;
            _BellekYonetim.VeriKaydet_string("Dil", "EN");
            DilTercihiYonetimi();
        }
        else
        {
            AktifDilIndex = 0;
            DilButonlari[0].interactable = false;
            DilText.text = "TURKCE";
            DilButonlari[1].interactable = true;
            _BellekYonetim.VeriKaydet_string("Dil", "TR");
            DilTercihiYonetimi();
        }
        ButonSes.Play();
    }
}
