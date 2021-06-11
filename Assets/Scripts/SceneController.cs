using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public Vector2 StartingPoint;
    HeroController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<HeroController>();
        player.transform.position = StartingPoint;
    }

    // Update is called once per frame
    void Update()
    {
        print(player.controlled);
    }
}
