using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 direction = (collision.transform.position - transform.position).normalized;
        //collision.gameObject.GetComponent<Rigidbody2D>().velocity += direction * speed;
        float grav = collision.gameObject.GetComponent<Rigidbody2D>().gravityScale;
        collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
        collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = grav;
    }
}
