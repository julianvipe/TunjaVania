using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] AudioClip coinFX;
    [SerializeField] int coinPoints;

    bool wasCollected=false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && !wasCollected)
        { 
            wasCollected = true;
            FindAnyObjectByType<GameSession>().addToScore(coinPoints);
            AudioSource.PlayClipAtPoint(coinFX, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
