using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    public GameObject ChestClosed, ChestOpen, player, findItemScene;

    void Start()
    {
        ChestClosed.SetActive(true);
        ChestOpen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.position = findItemScene.transform.position;

            SoundManagerScript.PlaySound("Discovery");
            ChestClosed.SetActive(false);
            ChestOpen.SetActive(true);
        }
    }
        
}
