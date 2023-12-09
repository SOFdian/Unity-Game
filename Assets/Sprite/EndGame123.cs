using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame123 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void FixedUpdate()
    {
        
    }
    public void OpenReScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }
    public void OverScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }
}
