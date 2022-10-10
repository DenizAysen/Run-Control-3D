using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Alt_Karakter : MonoBehaviour
{
    public GameManager gameManager;
    NavMeshAgent NavMesh;
    public GameObject Target;
    void Start()
    {
        NavMesh = GetComponent<NavMeshAgent>();
    }
    void LateUpdate()
    {
        NavMesh.SetDestination(Target.transform.position);
    }
    Vector3 PozisyonVer()
    {       
        return new Vector3(transform.position.x, 0.25f, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("IgneliKutu"))
        {           
            gameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
        }

        else if(other.CompareTag("Testere"))
        {
            gameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
        }

        else if(other.CompareTag("PervaneIgneler"))
        {
            gameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
        }

        else if(other.CompareTag("Balyoz"))
        {
            gameManager.YokOlmaEfektiOlustur(PozisyonVer(),true);
            gameObject.SetActive(false);
        }

        else if(other.CompareTag("Dusman"))
        {
            gameManager.YokOlmaEfektiOlustur(PozisyonVer(), false,false);           
            gameObject.SetActive(false);
        }
        
    }
}
