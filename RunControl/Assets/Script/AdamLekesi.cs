using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdamLekesi : MonoBehaviour
{

     IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
    /* void Start()
     {
         StartCoroutine(LekeyiKapat());
     }

     IEnumerator LekeyiKapat()
     {
         yield return new WaitForSeconds(5f);
         gameObject.SetActive(false);
     }*/
}
