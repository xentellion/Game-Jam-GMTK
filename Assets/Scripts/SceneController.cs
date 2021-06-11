using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public Vector2 StartingPoint;
    HeroController player;
    public List<GameObject> transitor = new List<GameObject>();

    IEnumerator Start()
    {
        foreach (GameObject go in transitor)
            go.gameObject.SetActive(false);

        player = FindObjectOfType<HeroController>();
        player.transform.position = StartingPoint;

        yield return new WaitForSeconds(1f);

        foreach (GameObject go in transitor)
            go.gameObject.SetActive(true);
    }
}
