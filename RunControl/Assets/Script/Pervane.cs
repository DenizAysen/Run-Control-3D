using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pervane : MonoBehaviour
{
    public Animator animator;
    //Eger pervaneler farklı aralıklar calissin istiyorsan bekleme suresi public oldugu icin her pervanedeki degiskeni degistirebilirsin
    public float beklemeSüresi;
    public BoxCollider _Ruzgar;
    public void AnimasyonDurum(string durum)
    {
        if(durum == "true")
        {
            animator.SetBool("Calistir", true);
            _Ruzgar.enabled = true;
        }
        else
        {
            animator.SetBool("Calistir", false);
            StartCoroutine(AnimasyonTetikle());
            _Ruzgar.enabled = false;
        }
       
    }

    IEnumerator AnimasyonTetikle()
    {
        yield return new WaitForSeconds(beklemeSüresi);
        AnimasyonDurum("true");
    }
}
