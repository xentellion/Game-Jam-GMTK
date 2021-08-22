using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Spikes : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HeroController player = collision.gameObject.GetComponent<HeroController>();
        if (player != null)
            StartCoroutine(player.OnDeath());
    }
}
