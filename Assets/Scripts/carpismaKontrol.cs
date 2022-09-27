using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carpismaKontrol : MonoBehaviour
{
    public gameManager _GameManager;
    public int carpismaIndex;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitCollider = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);
        for (int i = 0; i < hitCollider.Length; i++)
        {
            if (hitCollider[i].CompareTag("KabloParcasi"))
            {
                _GameManager.carpismayiKontrolet(carpismaIndex,false);
            }
            else
            {
                _GameManager.carpismayiKontrolet(carpismaIndex, true);
            }

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale / 2);
    }
}
