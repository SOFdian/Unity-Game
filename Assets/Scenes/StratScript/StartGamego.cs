using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGamego : MonoBehaviour
{
    public GameObject GameCanve;
    public GameObject GameCanve1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ColseGame()
    {
        Application.Quit();
    }
    public void ColseCanve()
    {
        GameCanve.SetActive(false);
    }
    public  void OpenCanve()
    {
        GameCanve.SetActive(true);
    }
    public void ColseCanve1()
    {
        GameCanve1.SetActive(false);
    }
    public void OpenCanve1()
    {
        GameCanve1.SetActive(true);
    }
}
