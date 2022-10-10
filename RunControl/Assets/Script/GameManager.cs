using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Deniz;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static int anlikKarakterSayisi = 1; // degisken sürekli degistigi icin static yaptim
    public static int maxKarakterSayisi;
    public List<GameObject> Characters;
    public List<GameObject> OlusmaEfektleri;
    public List<GameObject> YokOlmaEfektleri;
    public List<GameObject> AdamLekesiEfektleri;
     
    [Header("LEVEL VERILERI")]
    public List<GameObject> Enemies;
    public int KacDusmanOlsun;
    public GameObject _AnaKarakter;
    public bool OyunBittiMi;
    bool sonaGeldikMi;
    public Text PuanText;
    [Header("-------SAPKALAR")]
    public GameObject[] Sapkalar;
    [Header("-------SOPALAR")]
    public GameObject[] Sopalar;
    [Header("-------MATERIALS")]
    public Material[] Materyaller;
    public SkinnedMeshRenderer _Renderer;
    public Material VarsayilanTema;
    Matematiksel_Islemler _Matematiksel_islemler = new Matematiksel_Islemler();
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _VeriYonetim = new VeriYonetimi();
    ReklamYonetim _ReklamYonetim = new ReklamYonetim();
    Scene _Scene;

    [Header("----------GENEL VERILER")]
    public AudioSource[] Sesler;
    public GameObject[] islemPanelleri;
    public Slider OyunSesiAyar;
    [Header("-------DIL VERILERI")]
    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();
    List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public Text[] TextObjeleri;
    [Header("----------LOADING EKRANI")]
    public GameObject YuklemeEkrani;
    public Slider YuklemeSlider;
    private void Awake()
    {
        anlikKarakterSayisi = 1; // Level yüklendiginde sıkıntı çıkmasın diye tekrardan değişkeni 1 yaptım.
        Sesler[0].volume = _BellekYonetim.VeriOku_f("OyunSes");
        OyunSesiAyar.value = _BellekYonetim.VeriOku_f("OyunSes");
        Sesler[1].volume = _BellekYonetim.VeriOku_f("MenuFx");
        Destroy(GameObject.FindGameObjectWithTag("MenuSes"));
        ItemleriKontrolEt();       
        maxKarakterSayisi = Characters.Count;
    }
    void Start()
    {     
        DusmanlariOlustur();
        _Scene = SceneManager.GetActiveScene();
        _VeriYonetim.Dil_Load();
        _DilOkunanVeriler = _VeriYonetim.DilVerileriListeyiAktar();
        _DilVerileriAnaObje.Add(_DilOkunanVeriler[5]);
        DilTercihiYonetimi();
        PuanText.text = _BellekYonetim.VeriOku_i("Puan").ToString();
        _ReklamYonetim.RequestInterstitial();
        _ReklamYonetim.RequestRewardedAd();
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
    public void DusmanlariOlustur()
    {
        for (int i = 0; i < KacDusmanOlsun; i++)
        {
            if (!Enemies[i].activeSelf)
            {
                Enemies[i].SetActive(true);
            }
        }
    }
    public void DusmanlariTetikle()
    {
        foreach (var item in Enemies)
        {
            if (item.activeInHierarchy)
            {
                item.GetComponent<Dusman>().AnimasyonTetikle();
            }
        }
        sonaGeldikMi = true;
        SavasDurumu();
    }
    void SavasDurumu()
    {
        if (sonaGeldikMi)
        {
            if (anlikKarakterSayisi == 1 || KacDusmanOlsun == 0)
            {
                OyunBittiMi = true;
                foreach (var item in Enemies)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Attack", false);
                    }
                }

                foreach (var item in Characters)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Attack", false);
                    }
                }

                _AnaKarakter.GetComponent<Animator>().SetBool("Attack", false);
                _AnaKarakter.GetComponent<Animator>().SetBool("Running", false);

                _ReklamYonetim.GecisReklamiGoster();

                if (anlikKarakterSayisi < KacDusmanOlsun || anlikKarakterSayisi == KacDusmanOlsun)
                {
                    islemPanelleri[3].SetActive(true);
                }
                else
                {                    
                    if (anlikKarakterSayisi > 5)
                    {
                        if (_Scene.buildIndex == _BellekYonetim.VeriOku_i("SonLevel"))
                        {
                            _BellekYonetim.VeriKaydet_int("Puan", 600 + _BellekYonetim.VeriOku_i("Puan"));
                            _BellekYonetim.VeriKaydet_int("SonLevel", _BellekYonetim.VeriOku_i("SonLevel") + 1);
                        }                      
                    }                       
                    else
                    {
                        if (_Scene.buildIndex == _BellekYonetim.VeriOku_i("SonLevel"))
                        {
                            _BellekYonetim.VeriKaydet_int("Puan", 200 + _BellekYonetim.VeriOku_i("Puan"));
                            _BellekYonetim.VeriKaydet_int("SonLevel", _BellekYonetim.VeriOku_i("SonLevel") + 1);
                        }
                    }
                    islemPanelleri[2].SetActive(true);
                }
            }
        }   
    }
    public void AdamYonetim(string islemTuru,int gelenSayi, Transform pozisyon)
    {
        switch (islemTuru)
        {
            case "Carpma":
               _Matematiksel_islemler.Carpma(gelenSayi, Characters, pozisyon,OlusmaEfektleri);
                break;
            case "Toplama":
                _Matematiksel_islemler.Toplama(gelenSayi, Characters, pozisyon,OlusmaEfektleri);
                break;
            case "Cikarma":
                _Matematiksel_islemler.Cikarma(gelenSayi, Characters,YokOlmaEfektleri);                
                break;
            case "Bolme":
                _Matematiksel_islemler.Bolme(gelenSayi, Characters,YokOlmaEfektleri);
                break;

        }
    }
    public void YokOlmaEfektiOlustur(Vector3 pozisyon,bool Balyoz = false, bool durum = false)
    {
        foreach (var item in YokOlmaEfektleri)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = pozisyon;
                item.GetComponent<ParticleSystem>().Play();
                item.GetComponent<AudioSource>().Play();
                if (!durum)
                {
                    if(anlikKarakterSayisi-1 <= 0)
                    {
                        anlikKarakterSayisi = 1;
                    }
                    else
                    {
                        anlikKarakterSayisi--;
                    }                    
                }
                else
                {
                    KacDusmanOlsun--;
                }                           
                break;
            }            
        }

        if (Balyoz)
        {
            Vector3 yeniPos = new Vector3(pozisyon.x, 0.005f, pozisyon.z);
            foreach (var item in AdamLekesiEfektleri)
            {
                if (!item.activeInHierarchy)
                {
                    item.SetActive(true);
                    item.transform.position = yeniPos;
                    break;
                }
            }
        }

        if (!OyunBittiMi)
        {
            SavasDurumu();
        }
    }
    public void ItemleriKontrolEt()
    {
        if(_BellekYonetim.VeriOku_i("AktifSapka") != -1)
        {
            Sapkalar[_BellekYonetim.VeriOku_i("AktifSapka")].SetActive(true);
        }
        if(_BellekYonetim.VeriOku_i("AktifSopa") != -1)
        {
            Sopalar[_BellekYonetim.VeriOku_i("AktifSopa")].SetActive(true);
        }
        if (_BellekYonetim.VeriOku_i("AktifTema") != -1)
        {
            Material[] mats = _Renderer.materials;
            mats[0] = Materyaller[_BellekYonetim.VeriOku_i("AktifTema")];
            _Renderer.materials = mats;
        }
        else
        {
            Material[] mats = _Renderer.materials;
            mats[0] = VarsayilanTema;
            _Renderer.materials = mats;
        }
    }
    public void OyunuBaslat()
    {
        islemPanelleri[5].SetActive(false);
        islemPanelleri[4].SetActive(true);
        _AnaKarakter.GetComponent<Karakter>().enabled = true;
        _AnaKarakter.GetComponent<Animator>().SetBool("Running", true);
    }
    public void CikisButonislem(string durum)
    {
        Sesler[1].Play();
        Time.timeScale = 0;
        if (durum == "durdur")
        {
            islemPanelleri[0].SetActive(true);
        }
        else if (durum == "devamet")
        {
            islemPanelleri[0].SetActive(false);
            Time.timeScale = 1;
        }
        else if (durum == "tekrar")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(_Scene.buildIndex);
        }
        else if (durum == "Anasayfa")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }
    public void Ayarlar(string durum)
    {
        Sesler[1].Play();
        if (durum == "ayarla")
        {
            islemPanelleri[1].SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            islemPanelleri[1].SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void SesiAyarla()
    {
        _BellekYonetim.VeriKaydet_float("OyunSes", OyunSesiAyar.value);
        Sesler[0].volume = OyunSesiAyar.value;
    }
    public void SonrakiLevel()
    {
        StartCoroutine(LoadAsync(_Scene.buildIndex + 1));
    }
    IEnumerator LoadAsync(int SceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneIndex);

        YuklemeEkrani.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / (0.9f));
            YuklemeSlider.value = progress;
            yield return null;
        }
    }
    public void OdulluReklam()
    {
        _ReklamYonetim.OdulluReklamGoster();
    }
}
