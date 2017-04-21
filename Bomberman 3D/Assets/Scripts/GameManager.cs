using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    private BoardManager boardScript;                       
    private int level = 3;
                                      

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    void InitGame()
    {
        //RegisterFactories();
        boardScript.SetupScene(level);
        
        
    }

    //void RegisterFactories()
    //{
    //    FactoryContainer container = FactoryContainer.Instance;
    //    container.Register<BombFactory>();
    //    container.Register<PlayerFactory>();
    //    container.Register<Enemy1Factory>();
    //    container.Register<Enemy2Factory>();
    //}
}