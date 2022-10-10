using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GoogleMobileAds.Api;

namespace Deniz
{
    public class Matematiksel_Islemler 
    {
        public void Carpma(int gelenSayi,List<GameObject> Characters,Transform pozisyon, List<GameObject> OlusturmaEfektleri)
        {
            //(anlikKarakterSayisi*gelensayi)- anlikKarakterSayisi
            //5 * 4  - 5 = 15
            //20*3 - 20 = 40
            int donguSayisi = (GameManager.anlikKarakterSayisi * gelenSayi) - GameManager.anlikKarakterSayisi;
            int sayi = 0;
            foreach (GameObject gmo in Characters)
            {
                if (sayi < donguSayisi)
                {
                    if (!gmo.activeInHierarchy)
                    {
                        foreach (var item in OlusturmaEfektleri)
                        {
                            if (!item.activeInHierarchy)
                            {                               
                                item.SetActive(true);
                                item.transform.position = pozisyon.position;
                                item.GetComponent<ParticleSystem>().Play();
                                item.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        gmo.transform.position = pozisyon.position - new Vector3(0, 0, 0.2f);
                        gmo.SetActive(true);
                        sayi++;

                    }
                }
                else
                {
                    sayi = 0;
                    break;
                }
            }
            Debug.Log("Çarpılmadan önceki anlık karakter sayısı:" + GameManager.anlikKarakterSayisi);
            if(GameManager.anlikKarakterSayisi *gelenSayi > GameManager.maxKarakterSayisi)
            {
                GameManager.anlikKarakterSayisi = GameManager.maxKarakterSayisi;
            }
            else
            {
                GameManager.anlikKarakterSayisi *= gelenSayi;
            }        
            Debug.Log("Çarpıldıktan sonraki anlık karakter sayısı:" + GameManager.anlikKarakterSayisi);
        }
        public void Toplama(int gelenSayi, List<GameObject> Characters, Transform pozisyon, List<GameObject> OlusturmaEfektleri)
        {
            int sayi2 = 0;
            foreach (GameObject gmo in Characters)
            {
                if (sayi2 < gelenSayi)
                {
                    if (!gmo.activeInHierarchy)
                    {
                        foreach (var item in OlusturmaEfektleri)
                        {
                            if (!item.activeInHierarchy)
                            {
                                item.SetActive(true);
                                item.transform.position = pozisyon.position;
                                item.GetComponent<ParticleSystem>().Play();
                                item.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }

                        gmo.transform.position = pozisyon.position - new Vector3(0, 0, 0.15f);
                        gmo.SetActive(true);
                        sayi2++;

                    }
                }
                else
                {
                    sayi2 = 0;
                    break;
                }
            }
            Debug.Log("Toplamadan önce karakter sayısı" + GameManager.anlikKarakterSayisi);
            if (GameManager.anlikKarakterSayisi + gelenSayi > GameManager.maxKarakterSayisi)
            {
                GameManager.anlikKarakterSayisi = GameManager.maxKarakterSayisi;
            }
            else
            {
                GameManager.anlikKarakterSayisi += gelenSayi;
            }
            Debug.Log("Toplamadan sonraki karakter sayısı" + GameManager.anlikKarakterSayisi);
        }
        public void Cikarma(int gelenSayi, List<GameObject> Characters, List<GameObject> YokOlmaEfektleri)
        {
            if (GameManager.anlikKarakterSayisi < gelenSayi)
            {
                for(int i = 0; i < Characters.Count; i++)
                {
                    foreach (var item in YokOlmaEfektleri)
                    {
                        if (!item.activeInHierarchy)
                        {
                            //Eger blokta olusmasini istiyorsan z de 1.5f ekle
                            Vector3 yeniPos = new Vector3(Characters[i].transform.position.x, 0.25f, Characters[i].transform.position.z);
                            item.SetActive(true);
                            item.transform.position = yeniPos;
                            item.GetComponent<ParticleSystem>().Play();
                            item.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }
                    if (Characters[i].activeInHierarchy)
                    {
                        Characters[i].transform.position = Vector3.zero;
                        Characters[i].SetActive(false);
                    }
                }//En kötü eski kodlara döner bakarsın videodan               
                Debug.Log("Eksilmeden önceki anlık karakter sayısı:" + GameManager.anlikKarakterSayisi);
                GameManager.anlikKarakterSayisi = 1;
                Debug.Log("Eksilmeden önceki anlık karakter sayısı:" + GameManager.anlikKarakterSayisi);
            }

            else
            {
                int sayi3 = 0;
                foreach (GameObject gmo in Characters)
                {
                    if (sayi3 < gelenSayi)
                    {
                        if (gmo.activeInHierarchy)
                        {
                            foreach (var item in YokOlmaEfektleri)
                            {
                                if (!item.activeInHierarchy)
                                {
                                    //Eger blokta olusmasini istiyorsan z de 1.5f ekle
                                    Vector3 yeniPos = new Vector3(gmo.transform.position.x, 0.25f, gmo.transform.position.z);
                                    item.SetActive(true);
                                    item.transform.position =yeniPos;
                                    item.GetComponent<ParticleSystem>().Play();
                                    item.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }

                            gmo.transform.position = Vector3.zero;
                            gmo.SetActive(false);
                            sayi3++;

                        }
                    }
                    else
                    {
                        sayi3 = 0;
                        break;
                    }
                }
                Debug.Log("Eksilmeden önceki anlık karakter sayısı:" + GameManager.anlikKarakterSayisi);
                GameManager.anlikKarakterSayisi -= gelenSayi;
                Debug.Log("Eksilmeden önceki anlık karakter sayısı:" + GameManager.anlikKarakterSayisi);
            }
        }
        public void Bolme(int gelenSayi, List<GameObject> Characters, List<GameObject> YokOlmaEfektleri)
        {
            if (GameManager.anlikKarakterSayisi <= gelenSayi)
            {               
                for (int i = 0; i < Characters.Count; i++)
                {
                    foreach (var item in YokOlmaEfektleri)
                    {
                        if (!item.activeInHierarchy)
                        {
                            //Eger blokta olusmasini istiyorsan z de 1.5f ekle
                            Vector3 yeniPos = new Vector3(Characters[i].transform.position.x, 0.25f, Characters[i].transform.position.z);
                            item.SetActive(true);
                            item.transform.position = yeniPos;
                            item.GetComponent<ParticleSystem>().Play();
                            item.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }
                    if (Characters[i].activeInHierarchy)
                    {
                        Characters[i].transform.position = Vector3.zero;
                        Characters[i].SetActive(false);
                    }
                }
                Debug.Log("Bölünmeden önceki anlık karakter sayısı:" + GameManager.anlikKarakterSayisi);
                GameManager.anlikKarakterSayisi = 1;
                Debug.Log("Bölünmeden sonraki anlık karakter sayısı:" + GameManager.anlikKarakterSayisi);
            }

            else
            {
                // int bolen = GameManager.anlikKarakterSayisi / gelenSayi;
                int bolum = 0;
                int islemSayisi = 0;
                int sayi3 = 0;
                if(GameManager.anlikKarakterSayisi %gelenSayi == 0)
                {
                    bolum = GameManager.anlikKarakterSayisi / gelenSayi;
                    islemSayisi = GameManager.anlikKarakterSayisi - bolum;
                    foreach (GameObject gmo in Characters)
                    {
                        if (sayi3 < islemSayisi)
                        {
                            if (gmo.activeInHierarchy)
                            {
                                foreach (var item in YokOlmaEfektleri)
                                {
                                    if (!item.activeInHierarchy)
                                    {
                                        //Eger blokta olusmasini istiyorsan z de 1.5f ekle
                                        Vector3 yeniPos = new Vector3(gmo.transform.position.x, 0.25f, gmo.transform.position.z);
                                        item.SetActive(true);
                                        item.transform.position = yeniPos;
                                        item.GetComponent<ParticleSystem>().Play();
                                        item.GetComponent<AudioSource>().Play();
                                        break;
                                    }
                                }

                                gmo.transform.position = Vector3.zero;
                                gmo.SetActive(false);
                                sayi3++;

                            }
                        }
                        else
                        {
                            sayi3 = 0;
                            break;
                        }
                    }
                    Debug.Log("Bölünmeden önceki anlık karakter sayısı:" + GameManager.anlikKarakterSayisi);
                    GameManager.anlikKarakterSayisi /= gelenSayi;
                    Debug.Log("Bölünmeden sonraki anlık karakter sayısı:" + GameManager.anlikKarakterSayisi);
                }
                else
                {
                    int kalan = GameManager.anlikKarakterSayisi % gelenSayi;
                    if (kalan == 1)
                    {
                        bolum = GameManager.anlikKarakterSayisi / gelenSayi;
                        islemSayisi = GameManager.anlikKarakterSayisi - bolum ;
                        foreach (GameObject gmo in Characters)
                        {
                            if (sayi3 < islemSayisi)
                            {
                                if (gmo.activeInHierarchy)
                                {
                                    foreach (var item in YokOlmaEfektleri)
                                    {
                                        if (!item.activeInHierarchy)
                                        {
                                            //Eger blokta olusmasini istiyorsan z de 1.5f ekle
                                            Vector3 yeniPos = new Vector3(gmo.transform.position.x, 0.25f, gmo.transform.position.z);
                                            item.SetActive(true);
                                            item.transform.position = yeniPos;
                                            item.GetComponent<ParticleSystem>().Play();
                                            item.GetComponent<AudioSource>().Play();
                                            break;
                                        }
                                    }

                                    gmo.transform.position = Vector3.zero;
                                    gmo.SetActive(false);
                                    sayi3++;

                                }
                            }
                            else
                            {
                                sayi3 = 0;
                                break;
                            }
                        }
                        Debug.Log("Bölünmeden önceki anlık karakter sayısı:" + GameManager.anlikKarakterSayisi);
                        GameManager.anlikKarakterSayisi /= gelenSayi;
                        GameManager.anlikKarakterSayisi += 1;
                        Debug.Log("Bölünmeden sonraki anlık karakter sayısı:" + GameManager.anlikKarakterSayisi);
                    }
                    else if(kalan == 2)
                    {
                        bolum = GameManager.anlikKarakterSayisi / gelenSayi;
                        islemSayisi = GameManager.anlikKarakterSayisi - (bolum + 1);//Ana karakter hiçbir zaman yok olmucağı için +1 her karakter yok olabilseydi -2 olurdu
                        foreach (GameObject gmo in Characters)
                        {
                            if (sayi3 < islemSayisi)
                            {
                                if (gmo.activeInHierarchy)
                                {
                                    foreach (var item in YokOlmaEfektleri)
                                    {
                                        if (!item.activeInHierarchy)
                                        {
                                            //Eger blokta olusmasini istiyorsan z de 1.5f ekle
                                            Vector3 yeniPos = new Vector3(gmo.transform.position.x, 0.25f, gmo.transform.position.z);
                                            item.SetActive(true);
                                            item.transform.position = yeniPos;
                                            item.GetComponent<ParticleSystem>().Play();
                                            item.GetComponent<AudioSource>().Play();
                                            break;
                                        }
                                    }

                                    gmo.transform.position = Vector3.zero;
                                    gmo.SetActive(false);
                                    sayi3++;

                                }
                            }
                            else
                            {
                                sayi3 = 0;
                                break;
                            }
                        }
                        Debug.Log("Bölünmeden önceki anlık karakter sayısı:" + GameManager.anlikKarakterSayisi);
                        GameManager.anlikKarakterSayisi /= gelenSayi;
                        GameManager.anlikKarakterSayisi += 2;
                        Debug.Log("Bölünmeden sonraki anlık karakter sayısı:" + GameManager.anlikKarakterSayisi);
                    }
                    else if(kalan == 3)
                    {
                        bolum = GameManager.anlikKarakterSayisi / gelenSayi;
                        islemSayisi = GameManager.anlikKarakterSayisi - (bolum + 2);
                        foreach (GameObject gmo in Characters)
                        {
                            if (sayi3 < islemSayisi)
                            {
                                if (gmo.activeInHierarchy)
                                {
                                    foreach (var item in YokOlmaEfektleri)
                                    {
                                        if (!item.activeInHierarchy)
                                        {
                                            //Eger blokta olusmasini istiyorsan z de 1.5f ekle
                                            Vector3 yeniPos = new Vector3(gmo.transform.position.x, 0.25f, gmo.transform.position.z);
                                            item.SetActive(true);
                                            item.transform.position = yeniPos;
                                            item.GetComponent<ParticleSystem>().Play();
                                            item.GetComponent<AudioSource>().Play();
                                            break;
                                        }
                                    }

                                    gmo.transform.position = Vector3.zero;
                                    gmo.SetActive(false);
                                    sayi3++;

                                }
                            }
                            else
                            {
                                sayi3 = 0;
                                break;
                            }
                        }
                        Debug.Log("Bölünmeden önceki anlık karakter sayısı:" + GameManager.anlikKarakterSayisi);
                        GameManager.anlikKarakterSayisi /= gelenSayi;
                        GameManager.anlikKarakterSayisi += 3;
                        Debug.Log("Bölünmeden sonraki anlık karakter sayısı:" + GameManager.anlikKarakterSayisi);
                    }
                }                  
            }
            /*foreach (GameObject gmo in Characters)
            {
                if (sayi3 < bolen)
                {
                    if (gmo.activeInHierarchy)
                    {
                        foreach (var item in YokOlmaEfektleri)
                        {
                            if (!item.activeInHierarchy)
                            {
                                //Eger blokta olusmasini istiyorsan z de 1.5f ekle
                                Vector3 yeniPos = new Vector3(gmo.transform.position.x, 0.25f, gmo.transform.position.z );
                                item.SetActive(true);
                                item.transform.position = yeniPos;
                                item.GetComponent<ParticleSystem>().Play();
                                item.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }

                        gmo.transform.position = Vector3.zero;
                        gmo.SetActive(false);
                        sayi3++;

                    }
                }
                else
                {
                    sayi3 = 0;
                    break;
                }
             }
            Debug.Log("Bölünmeden önceki anlık karakter sayısı:" + GameManager.anlikKarakterSayisi);
            if (GameManager.anlikKarakterSayisi % gelenSayi == 0)
            {
                GameManager.anlikKarakterSayisi /= gelenSayi;
            }

            else if(GameManager.anlikKarakterSayisi % gelenSayi == 1)
            {
                GameManager.anlikKarakterSayisi /= gelenSayi;
                GameManager.anlikKarakterSayisi += 1;
            }

            else if (GameManager.anlikKarakterSayisi % gelenSayi == 2)
            {
                GameManager.anlikKarakterSayisi /= gelenSayi;
                GameManager.anlikKarakterSayisi += 2;
            }
            else if (GameManager.anlikKarakterSayisi % gelenSayi == 3)
            {
                GameManager.anlikKarakterSayisi /= gelenSayi;
                GameManager.anlikKarakterSayisi += 3;
            }                
            Debug.Log("Bölünmeden sonraki anlık karakter sayısı:" + GameManager.anlikKarakterSayisi);*/
        }
    }   
    public class BellekYonetim
    {
        public void VeriKaydet_string(string Key, string value)
        {
            PlayerPrefs.SetString(Key, value);
            PlayerPrefs.Save();
        }

        public void VeriKaydet_int(string Key, int value)
        {
            PlayerPrefs.SetInt(Key, value);
            PlayerPrefs.Save();
           
        }

        public void VeriKaydet_float(string Key, float value)
        {
            PlayerPrefs.SetFloat(Key, value);
            PlayerPrefs.Save();
        }

        public string VeriOku_s(string Key)
        {
            return PlayerPrefs.GetString(Key);
        }
        public int VeriOku_i(string Key)
        {
            return PlayerPrefs.GetInt(Key);
        }

        public float VeriOku_f(string Key)
        {
            return PlayerPrefs.GetFloat(Key);
        }

        public void KontrolEtveTanimla()
        {
            if (!PlayerPrefs.HasKey("SonLevel"))
            {
                PlayerPrefs.SetInt("SonLevel", 5);
                PlayerPrefs.SetInt("Puan", 100);
                PlayerPrefs.SetInt("AktifSapka", -1);
                PlayerPrefs.SetInt("AktifSopa", -1);
                PlayerPrefs.SetInt("AktifTema", -1);
                PlayerPrefs.SetFloat("MenuSes", 1f);
                PlayerPrefs.SetFloat("MenuFx", 1f);
                PlayerPrefs.SetFloat("OyunSes", 1f);
                PlayerPrefs.SetString("Dil", "TR");
                PlayerPrefs.SetInt("Gecisreklamisayisi", 1);
            }
        }
    }

    [Serializable]
    public class ItemBilgileri
    {
        public int GrupIndex;
        public int Item_Index;
        public string Item_Ad;
        public int puan;
        public bool SatinAlmaDurumu;
    }
    public class VeriYonetimi
    {
        public void Save(List<ItemBilgileri> _ItemBilgileri)
        {
            // _ItemBilgileri[1].SatinAlmaDurumu = false;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemVerileri.gd");           
            binaryFormatter.Serialize(file, _ItemBilgileri);
            file.Close();           
        }
        
        List<ItemBilgileri> _ItemicListe;
        public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/ItemVerileri.gd"))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemVerileri.gd", FileMode.Open);
                _ItemicListe = (List<ItemBilgileri>)binaryFormatter.Deserialize(file);
                file.Close();
            }
        }
        public List<ItemBilgileri> ListeyiAktar()
        {
            return _ItemicListe;
        }
        public void ilkKurulumDosyaOlusturma(List<ItemBilgileri> _ItemBilgileri, List<DilVerileriAnaObje> _DilVerileri)
        {
            if (!File.Exists(Application.persistentDataPath + "/ItemVerileri.gd"))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemVerileri.gd");
                binaryFormatter.Serialize(file, _ItemBilgileri);
                file.Close();
            }

            if (!File.Exists(Application.persistentDataPath + "/DilVerileri.gd"))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();               
                FileStream file = File.Create(Application.persistentDataPath + "/DilVerileri.gd");
                binaryFormatter.Serialize(file, _DilVerileri);
                file.Close();
            }
        }

        //-------------------

        List<DilVerileriAnaObje> _DilVerileriicListe;
        public void Dil_Load()
        {
            if (File.Exists(Application.persistentDataPath + "/DilVerileri.gd"))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                //Debug.Log(Application.persistentDataPath);
                FileStream file = File.Open(Application.persistentDataPath + "/DilVerileri.gd", FileMode.Open);
                _DilVerileriicListe = (List<DilVerileriAnaObje>)binaryFormatter.Deserialize(file);
                file.Close();

            }
        }
        public List<DilVerileriAnaObje> DilVerileriListeyiAktar()
        {
            return _DilVerileriicListe;
        }
    }

    // -----DIL YONETIMI

    [Serializable]
    public class DilVerileriAnaObje
    {       
        public List<DilVerileri_TR> _DilVerileri_TR = new List<DilVerileri_TR>();
        public List<DilVerileri_TR> _DilVerileri_EN = new List<DilVerileri_TR>();
    }

    [Serializable]
    public class DilVerileri_TR
    {
        public string Metin;
    }

    // -----REKLAM YONETIMI

    public class ReklamYonetim
    {
        private InterstitialAd interstitial;
        private RewardedAd _RewardedAd;
        //GECIS REKLAMI
        public void RequestInterstitial()
        {
            string AdUnitId;
                   #if UNITY_ANDROID
                               AdUnitId = "ca-app-pub-8355987540181604/6186824462";
#elif UNITY_IPHONE
                                        AdUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
                                         AdUnitId = "unexpected_platform";
#endif

            interstitial = new InterstitialAd(AdUnitId);
            AdRequest request = new AdRequest.Builder().Build();
            interstitial.LoadAd(request);

            interstitial.OnAdClosed += GecisReklamiKapatildi;
        }
        void GecisReklamiKapatildi(object sender , EventArgs args)
        {
            interstitial.Destroy();
            RequestInterstitial();
        }
        public void GecisReklamiGoster()
        {

            if(PlayerPrefs.GetInt("Gecisreklamisayisi")== 2)
            {
                if (interstitial.IsLoaded())
                {
                    PlayerPrefs.SetInt("Gecisreklamisayisi", 1);
                    interstitial.Show();
                }
                else
                {
                    interstitial.Destroy();
                    RequestInterstitial();
                }
            }
            else
            {
                PlayerPrefs.SetInt("Gecisreklamisayisi", PlayerPrefs.GetInt("Gecisreklamisayisi")+1);
            }
            
        }

        //ODULLU REKLAM
        public void RequestRewardedAd()
        {
            string AdUnitId;
#if UNITY_ANDROID
            AdUnitId = "ca-app-pub-8355987540181604/2389930124";
#elif UNITY_IPHONE
                                        AdUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
                                         AdUnitId = "unexpected_platform";
#endif

            _RewardedAd = new RewardedAd(AdUnitId);
            AdRequest request = new AdRequest.Builder().Build();
            _RewardedAd.LoadAd(request);

            _RewardedAd.OnUserEarnedReward += OdulluReklamTamamlandi;
            _RewardedAd.OnAdClosed += OdulluReklamKapatildi;
            _RewardedAd.OnAdLoaded+= OdulluReklamYuklendi;
        }

        private void OdulluReklamTamamlandi(object sender, Reward e)
        {
            string type = e.Type;
            double amaount = e.Amount;
            Debug.Log("Odul alinsin :"+type+" "+amaount);
        }
        private void OdulluReklamKapatildi(object sender, EventArgs e)
        {
            Debug.Log("Reklam kapatildi");
            RequestRewardedAd();
        }
        private void OdulluReklamYuklendi(object sender, EventArgs e)
        {
            Debug.Log("Reklam yuklendi");
        }
            
        public void OdulluReklamGoster()
        {
            if (_RewardedAd.IsLoaded())
            {
                _RewardedAd.Show();
            }
        }
    }
}

