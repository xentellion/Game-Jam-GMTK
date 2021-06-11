using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    HeroController player;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //Uuuh i think this should also instantiate player if needed. Ah well
        player = FindObjectOfType<HeroController>();
    }

    //Loads new scene after some walk. May add some fancy screen darkening
    public IEnumerator LoadNextScene(string nextScene)
    {
        player.controlled = false;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextScene);
        StartCoroutine(FinishLoad());
    }

    IEnumerator FinishLoad()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<HeroController>().controlled = true;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.GetComponent<Rigidbody2D>().velocity.y);
        //after that all level transitions should be enabled. Because they should be all disabled on load
    }
}
