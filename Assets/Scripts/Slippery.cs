using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slippery : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float slippery = 10;

    private void Start()
    {
        //rb = FindObjectOfType<HeroController>().GetComponent<Rigidbody2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        float f = Input.GetAxisRaw("Horizontal");
        print(f);
        //collision.rigidbody.velocity = new Vector2(f * slippery, 0);
        //collision.rigidbody.in
    }
}
