using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class LoadScene : MonoBehaviour
{
    public string nextScene = "";
    BoxCollider2D box;
    GameManager gm;
    [HideInInspector] public enum Direction { Left, Right, Up, Down };
    public Direction direction;

    [HideInInspector]
    public Dictionary<Direction, Vector2> direcionsDict = new Dictionary<Direction, Vector2>
    {
        { Direction.Left, Vector2.left },
        {Direction.Right, Vector2.right },
        {Direction.Up, Vector2.up },
        {Direction.Down, Vector2.down }
    };

    //Basic script, loads next scene on collider touch. 
    //can be improved via adding some animation and translating hero data
    //so basically drag it on an empty object and set up colliders.

    void Awake()
    {
        //Failsafe. Just in case we forget to set it as trigger
        box = GetComponent<BoxCollider2D>();
        gm = FindObjectOfType<GameManager>();
        box.isTrigger = true;

        //set Z to 0
        transform.position -= new Vector3(0, 0, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HeroController>() != null)
        {
            StartCoroutine(gm.LoadNextScene(nextScene));
        }
    }
}
