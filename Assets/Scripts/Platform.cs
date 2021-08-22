using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Platform : MonoBehaviour
{
    [HideInInspector] public Vector3[] handles = new Vector3[2] { new Vector3(1, 0), new Vector3(-1, 0) };
    [SerializeField] GameObject left;
    [SerializeField] GameObject right;

    [SerializeField] bool goingRight = true;
    bool isWaiting = false;
    [SerializeField] float velocity = 5f;
    Rigidbody2D rb;

    private void Start()
    {
        //transform.position = goingRight ? left.transform.position : right.transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        if (!isWaiting)
        {
            if(transform.position.x > right.transform.position.x)
            {
                goingRight = false;
            }
            if (transform.position.x < left.transform.position.x)
            {
                goingRight = true;
            }

            if (goingRight)
                transform.position = new Vector2(transform.position.x + velocity * Time.deltaTime, transform.position.y);
            else
                transform.position = new Vector2(transform.position.x - velocity * Time.deltaTime, transform.position.y);

        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        isWaiting = false;
        goingRight = !goingRight;
        yield return new WaitForSeconds(1f);
    }
}
