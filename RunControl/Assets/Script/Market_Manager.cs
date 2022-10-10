using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deniz;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;

public class Market_Manager : MonoBehaviour , IStoreListener
{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_ExtensionProvider;

    private static string Puan_250 = "puan250";
    private static string Puan_500 = "puan500";
    private static string Puan_750 = "puan750";
    private static string Puan_1000 = "puan1000";

    [Header("-------DIL VERILERI")]
    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();
    List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public Text[] TextObjeleri;
    VeriYonetimi _VeriYonetim = new VeriYonetimi();
    BellekYonetim _BellekYonetim = new BellekYonetim();
    void Start()
    {
        _VeriYonetim.Dil_Load();
        _DilOkunanVeriler = _VeriYonetim.DilVerileriListeyiAktar();
        _DilVerileriAnaObje.Add(_DilOkunanVeriler[3]);
        DilTercihiYonetimi();

        if(m_StoreController == null)
        {
            InitializePurchasing();
        }
    }
    public void InitializePurchasing()
    {
        if (isInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(Puan_250, ProductType.Consumable);
        builder.AddProduct(Puan_500, ProductType.Consumable);
        builder.AddProduct(Puan_750, ProductType.Consumable);
        builder.AddProduct(Puan_1000, ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
    }
    public void UrunAl_250()
    {
        BuyProductID(Puan_250);
    }
    public void UrunAl_500()
    {
        BuyProductID(Puan_500);
    }
    public void UrunAl_750()
    {
        BuyProductID(Puan_750);
    }
    public void UrunAl_1000()
    {
        BuyProductID(Puan_1000);
    }
    /*yada
     * public void UrunAl(string id)
    {
        BuyProductID(id);
    }
     
     */
    void BuyProductID(string productID)
    {
        if (isInitialized())
        {
            Product product = m_StoreController.products.WithID(productID);

            if(product != null && product.availableToPurchase)
            {
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("Satin alirken hata olustu");
            }
        }
        else
        {
            Debug.Log("Urun cagirilamiyor");
        }
    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        if (string.Equals(purchaseEvent.purchasedProduct.definition.id, Puan_250,System.StringComparison.Ordinal))
        {
            _BellekYonetim.VeriKaydet_int("Puan", _BellekYonetim.VeriOku_i("Puan") + 250);
        }
        else if (string.Equals(purchaseEvent.purchasedProduct.definition.id, Puan_500, System.StringComparison.Ordinal))
        {
            _BellekYonetim.VeriKaydet_int("Puan", _BellekYonetim.VeriOku_i("Puan") + 500);
        }
        else if (string.Equals(purchaseEvent.purchasedProduct.definition.id, Puan_750, System.StringComparison.Ordinal))
        {
            _BellekYonetim.VeriKaydet_int("Puan", _BellekYonetim.VeriOku_i("Puan") + 750);
        }
        else if (string.Equals(purchaseEvent.purchasedProduct.definition.id, Puan_1000, System.StringComparison.Ordinal))
        {
            _BellekYonetim.VeriKaydet_int("Puan", _BellekYonetim.VeriOku_i("Puan") + 1000);
        }
        return PurchaseProcessingResult.Complete;
    }
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }    
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        m_ExtensionProvider = extensions;
    }
    private bool isInitialized()
    {
        return m_StoreController != null && m_ExtensionProvider != null;
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
    public void GeriDon()
    {
        SceneManager.LoadScene(0);
    }
}
