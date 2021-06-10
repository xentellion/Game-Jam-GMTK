using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class LoadScene : MonoBehaviour
{
    [SerializeField] string nextScene = "";
    BoxCollider2D box;

    //Basic script, loads next scene on collider touch. 
    //can be improved via adding some animation and translating hero data
    //so basically drag it on an empty object and set up colliders.

    void Awake()
    {
        //Failsafe. Just in case we forget to set it as trigger
        box = GetComponent<BoxCollider2D>();
        box.isTrigger = true;

        //set Z to 0
        transform.position -= new Vector3(0, 0, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HeroController>() != null)
        {
            //freeze input and keep it running. Also 
            try
            {
                SceneManager.LoadScene(nextScene);
            }
            catch
            {
                print("Scene not found");
            }
        }
    }
}
