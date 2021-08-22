using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    HeroController player;
    [HideInInspector] public string currentScene;
    [SerializeField] GameObject playerPrefab;
    Animator screen;

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
            Instantiate(playerPrefab, GameObject.FindGameObjectWithTag("Reset").transform.position, Quaternion.identity);
        }

        screen = FindObjectOfType<DetectScreen>().GetComponent<Animator>();
        currentScene = SceneManager.GetActiveScene().name;
    }

    //Loads new scene after some walk. May add some fancy screen darkening
    public IEnumerator LoadNextScene(string nextScene)
    {
        screen.SetTrigger("Start");
        player.controlled = false;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        yield return new WaitForSeconds(1f);
        var sm = FindObjectOfType<SceneController>();
        foreach (LoadScene go in sm.gates)
            go.gameObject.SetActive(false);
        SceneManager.LoadScene(nextScene);
        StartCoroutine(FinishLoad());
    }

    IEnumerator FinishLoad()
    {
        yield return new WaitForSeconds(0.1f);
        var sm = FindObjectOfType<SceneController>();
        for(int i = 0; i < sm.gates.Length; i++)
        {
            if(sm.gates[i].nextScene == currentScene)
            {
                player.transform.position = sm.gates[i].transform.position;
                player.GetComponent<Rigidbody2D>().velocity = 5 * sm.gates[i].direcionsDict[sm.gates[i].direction];
                break;
            }
        }
        screen.SetTrigger("Continue");
        yield return new WaitForSeconds(1f);

        FindObjectOfType<HeroController>().controlled = true;
        player.GetComponent<Rigidbody2D>().gravityScale = player.gravity;
        //after that all level transitions should be enabled. Because they should be all disabled on load

        foreach (LoadScene go in sm.gates)
            go.gameObject.SetActive(true);

        currentScene = SceneManager.GetActiveScene().name;
    }
}
