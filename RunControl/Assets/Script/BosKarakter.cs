using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BosKarakter : MonoBehaviour
{
    public SkinnedMeshRenderer meshRenderer;
    public Material AtanacakMateryal;
    public NavMeshAgent NavMesh;
    [SerializeField] Animator _animator;
    public GameObject Target;
    [SerializeField] GameManager gameManager;
    bool TemasEttiMi;
   //bool OyuncuyaDokunduMu;
    void LateUpdate()
    {
        if (TemasEttiMi)
        {
            NavMesh.SetDestination(Target.transform.position);
        }      
    }
    void MateryalDegistirveAnimasyonTetikle()
    {
        Material[] mats = meshRenderer.materials;
        mats[0] = AtanacakMateryal;
        meshRenderer.materials = mats;
        _animator.SetBool("Attack", true);      
        GameManager.anlikKarakterSayisi++;
        gameObject.tag = "AltKarakter";
        gameManager.Characters.Add(gameObject);
        GameManager.maxKarakterSayisi++;
    }
    Vector3 PozisyonVer()
    {
        return new Vector3(transform.position.x, 0.25f, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("AltKarakter") || other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("BosKarakter"))
            {
                TemasEttiMi = true;
                MateryalDegistirveAnimasyonTetikle();
                GetComponent<AudioSource>().Play();
            }
        }
              
        else if (other.CompareTag("Dusman"))
        {
            gameManager.YokOlmaEfektiOlustur(PozisyonVer(), false, false);
            gameObject.SetActive(false);
            gameManager.Characters.Remove(gameObject);
        }
        else if (other.CompareTag("IgneliKutu"))
        {
            gameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
            gameManager.Characters.Remove(gameObject);
        }

        else if (other.CompareTag("Testere"))
        {
            gameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
            gameManager.Characters.Remove(gameObject);
        }

        else if (other.CompareTag("PervaneIgneler"))
        {
            gameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
            gameManager.Characters.Remove(gameObject);
        }

        else if (other.CompareTag("Balyoz"))
        {
            gameManager.YokOlmaEfektiOlustur(PozisyonVer(), true);
            gameObject.SetActive(false);
            gameManager.Characters.Remove(gameObject);
        }
    }

    private void OnDisable()
    {
        GameManager.maxKarakterSayisi--;
        gameManager.Characters.Remove(gameObject);
    }
}
