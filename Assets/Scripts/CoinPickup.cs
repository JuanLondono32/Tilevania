using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip pickupSound;
    [SerializeField] int pointsPerCoin = 150;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSession>().PickupCoin(pointsPerCoin);
        AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        Destroy(gameObject);
    }

}
