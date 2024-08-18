using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public int playerSize = 1;

    public List<int> PickupsID;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void End()
    {
        StartCoroutine(ToEndScene());
    }
    
    IEnumerator ToEndScene()
    {
        Debug.Log("end");
        yield return new WaitForSeconds(4.5f);
        Debug.Log("fade");
        FindObjectOfType<FadeControl>().FadeIn();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync("EndScene");

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
