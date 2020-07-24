using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private bool levels;
    public GameObject gLevels;
    private LevelLoader LevelLoader;

    // Start is called before the first frame update
    void Start()
    {
        LevelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickNewGame()
    {
        StartCoroutine(LevelLoader.LoadLevel(1));
        //SceneManager.LoadScene(1);
    }
    public void OnClickLoadLevel()
    {
        levels = !levels;
        gLevels.SetActive(levels);
    }
    public void OnClickLevelTwo()
    {
        StartCoroutine(LevelLoader.LoadLevel(2));
    }
    public void OnClickQuit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif 
    }
}
