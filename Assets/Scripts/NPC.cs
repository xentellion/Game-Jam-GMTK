using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    bool talked = false;
    HeroController player;
    [SerializeField] NPCDialog dialog;
    [SerializeField] GameObject Text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       StartCoroutine(Speak(collision));
    }

    IEnumerator Speak(Collider2D collision)
    {
        player = collision.gameObject.GetComponent<HeroController>();
        if (player != null)
        {
            if (!talked)
            {
                var text = Text.GetComponent<Text>();
                player.controlled = false;
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                text.text = dialog.GetDialog[0];

                for (int i = 1; i < dialog.GetDialog.Length; i++)
                {
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                    text.text = dialog.GetDialog[i];
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                text.text = "";
                player.controlled = true;
                talked = true;
            }
        }
    }
}
