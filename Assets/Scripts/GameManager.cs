using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    HeroController player;
    public string currentScene;

    void Awake()
    {
        //singletone
        GameManager[] ghosts = FindObjectsOfType<GameManager>();
        if (ghosts.Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        
        player = FindObjectOfType<HeroController>();
        
        if(player == null)
        {
            //Instantiate(PlayerPrfab, );
        }

        //I have to load a prefab in here
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
        yield return new WaitForSeconds(0.1f);
        print(SceneManager.GetActiveScene().name);
        var sm = FindObjectOfType<SceneController>();
        for(int i = 0; i < sm.transitor.Count; i++)
        {
            if(sm.transitor[i].GetComponent<LoadScene>().nextScene == currentScene)
            {
                player.transform.position = sm.transitor[i].transform.position;
                break;
            }
        }

        yield return new WaitForSeconds(1f);

        FindObjectOfType<HeroController>().controlled = true;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.GetComponent<Rigidbody2D>().velocity.y);
        //after that all level transitions should be enabled. Because they should be all disabled on load

        foreach (GameObject go in sm.transitor)
            go.gameObject.SetActive(true);

        currentScene = SceneManager.GetActiveScene().name;
    }
}
