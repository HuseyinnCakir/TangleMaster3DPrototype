using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kabloParcasi : MonoBehaviour
{
    [SerializeField] private gameManager _gameManager;
    [SerializeField] private ParticleSystem[] Kopmaefekt;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zemin") || collision.gameObject.CompareTag("soket"))
        {
            _gameManager.kaybettin();
            Kopmaefekt[0].gameObject.SetActive(true);
            Kopmaefekt[1].gameObject.SetActive(true);
            Kopmaefekt[0].Play();
            Kopmaefekt[1].Play();
        }
    }
}
