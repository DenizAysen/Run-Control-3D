using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Dusman : MonoBehaviour
{
    public GameObject Saldiri_Hedefi;
    public NavMeshAgent navMesh;
    public Animator animator;
    public GameManager _Gamemanager;
    bool Saldiri_Basladimi;
    public void AnimasyonTetikle()
    {
        animator.SetBool("Attack", true);
        Saldiri_Basladimi = true;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (Saldiri_Basladimi)
        {
            navMesh.SetDestination(Saldiri_Hedefi.transform.position);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AltKarakter"))
        {
            Vector3 yeniPos = new Vector3(transform.position.x, 0.25f, transform.position.z);
            _Gamemanager.YokOlmaEfektiOlustur(yeniPos,false,true);
            gameObject.SetActive(false);
        }
    }
}
