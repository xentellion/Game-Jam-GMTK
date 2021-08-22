using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public Vector2 StartingPoint;
    HeroController player;
    public LoadScene[] gates;
    public AudioClip Theme;
    AudioClip music;
    AudioSource playerSource;

    IEnumerator Start()
    {
        foreach (LoadScene go in gates)
            go.gameObject.SetActive(false);

        player = FindObjectOfType<HeroController>();
        //player.transform.position = StartingPoint;
        player.restartPoint = GameObject.FindGameObjectWithTag("Reset").transform.position;

        yield return new WaitForSeconds(1f);

        foreach (LoadScene go in gates)
            go.gameObject.SetActive(true);

        playerSource = player.GetComponent<AudioSource>();

        var newMusic = FindObjectOfType<SceneController>().Theme;
        if (newMusic != null)
        {
            if (playerSource.clip != null)
            {
                if (playerSource.clip.name != newMusic.name)
                {
                    playerSource.Stop();
                    playerSource.clip = newMusic;
                    playerSource.Play();
                }
            }
            else
            {
                playerSource.clip = newMusic;
                playerSource.Play();
            }
        }
    }
}
