using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
   public static GameManager Instance
    {
        get 
        {
            if (_instance is null)
                Debug.LogError("gameManager is Null");
           
            return _instance;
        }
    }
   public Action OnDeath;

    [SerializeField] GameObject _PanelGameOver, _PanelPause;


    [SerializeField] float _timer = 1f;
   public bool isPausing = false;
    float currentTime;
   
    public bool _gameAlreadyStart ;

    float _timerToGameOver = 0f;
    float _couldownforGameOver = 2f;
    private void Awake()
    {
       
        if (_instance != null & _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        // Time.timeScale = 1f;
        _PanelGameOver.SetActive(false);
        _PanelPause.SetActive(false);
        this.transform.parent = null;
        DontDestroyOnLoad(this.gameObject);
    }

    public void Init()
    {
        var child = transform.GetChild(0);
    }

    private void OnEnable()
    {
        OnDeath += Death;
    }

    private void OnDisable()
    {
        OnDeath -= Death;
    }

    private void Start()
    {
        Init();


        isPausing = true;
        currentTime = 1;
       
      
    }
    private void Update()
    {
        
      /*  if (_playerController)
        {

            if (_playerController._inputValue._Pause)
            {
            
          
                 isPausing = !isPausing;             
                    SetPause(isPausing);
            }
        }
        else
        {
            _playerController = FindObjectOfType<PlayerController>();
        }
        
        */
           

      
       /* else if (!_isDead && _gameAlreadyStart)
        {
            SetGameOver(false);
            isPausing = false;
            print("test");
        }*/
      
      /* if(_gameAlreadyStart)
        {
            // sa veut dire que la page a deja ete appeler
            _menu.SetActive(false);
            _gameOver.SetActive(false);
            isPausing = false;
            
            print("start");
           
        }*/
        
      
        
       
       
    }

    public void Death()
    {
        SetGameOver(true);
        _timerToGameOver += Time.deltaTime;
        _gameAlreadyStart = false;
    }

    public void SetPause(int pause)
    {
        Time.timeScale = pause;
        isPausing = !isPausing;
    }




    public void SetGameOver(bool isdead)
    {



        if (_timerToGameOver > _couldownforGameOver)
        {


            SetPause(1);
            _PanelGameOver.SetActive(true);
        }
    
       
      
       
       
    }
    public void StartGame()
    {
        isPausing = false;
        Init();
    }

    public void Retry()
    {
        _gameAlreadyStart = true;
        isPausing = false;
       // _menu.SetActive(false);
      
      
        SceneManager.LoadSceneAsync(0);
        Init();
    }

    public void ResumeGame()
    {
        StartCoroutine(ResumeGameWithTimer());
    }

    IEnumerator ResumeGameWithTimer()
    {
       
        
        yield return new WaitForSecondsRealtime(_timer);
      
        
        SetPause(0);


    }

    IEnumerator GameOverTimer()
    {
        yield return new WaitForSeconds(2f);
       
       


    }
    public void QuitGame() {
        //permet de tester quit dans l'application 
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

   /* public void GameMenu()
    {
        _isDead = false;
        _gameOver.SetActive(false);
        _gameAlreadyStart = false;
        _menu.SetActive(true);
        isPausing = true;
        SceneManager.LoadSceneAsync(0);
        Init();
    }
   */
   

    IEnumerator ReloadGameWithTimer()
    {
        yield return new WaitForSecondsRealtime(1f);
       


    }




    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
