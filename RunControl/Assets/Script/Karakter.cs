using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Karakter : MonoBehaviour
{
    [Header("----------GENEL VERILER")]
    public GameManager gameManager;
    public Kamera _Kamera;
    public bool _isFinished;
    public GameObject gidecegiYer;
    public Slider slider;
    public GameObject GecisNoktasi;
    public float hareketHizi;
    private void Start()
    {
        float fark = Vector3.Distance(transform.position, GecisNoktasi.transform.position);
        slider.maxValue = fark;
    }
    void FixedUpdate()
    {
        if (!_isFinished)
        {
            transform.Translate(Vector3.forward * hareketHizi * Time.deltaTime);
        }
       
    }
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (_isFinished)
            {
                transform.position = Vector3.Lerp(transform.position, gidecegiYer.transform.position, .05f);
                if (slider.value != 0)
                {
                    slider.value -= 0.01f;
                }               
            }

            else
            {
                float fark = Vector3.Distance(transform.position, GecisNoktasi.transform.position);
                slider.value = fark;

                if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (Input.GetAxis("Mouse X") < 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - .1f
                            , transform.position.y, transform.position.z), 0.3f);
                    }

                    if (Input.GetAxis("Mouse X") > 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + .1f
                            , transform.position.y, transform.position.z), 0.3f);
                    }

                }
            }
        }                 
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Carpma"))
        {
            gameManager.AdamYonetim(other.tag,int.Parse(other.name), other.transform);
        }

        else if (other.CompareTag("Bolme"))
        {
            gameManager.AdamYonetim(other.tag, int.Parse(other.name), other.transform);
        }

        else if(other.CompareTag("Toplama"))
        {
            gameManager.AdamYonetim(other.tag, int.Parse(other.name), other.transform);
        }

        else if (other.CompareTag("Cikarma"))
        {
            gameManager.AdamYonetim(other.tag, int.Parse(other.name), other.transform);
        }

        else if(other.CompareTag("Bitis"))
        {
            _Kamera.isFinished = true;
            gameManager.DusmanlariTetikle();
            _isFinished = true;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Direk")|| collision.gameObject.CompareTag("IgneliKutu")||
            collision.gameObject.CompareTag("PervaneIgneler"))
        {
            if(transform.position.x >0)
            transform.position = new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z);
            else
            transform.position = new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z);
        }
    }
}
