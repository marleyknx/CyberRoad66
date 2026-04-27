using Player;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
     public PlayerController _playerController;

   public bool _IsDead;
    public bool IsDead
    {
        get
        {
            if(_IsDead) GameManager.Instance.OnDeath.Invoke();
            return _IsDead;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerController._inputValue._Pause) GameManager.Instance.SetPause(0);
        else if (_playerController._inputValue._Pause && Time.timeScale == 0) GameManager.Instance.SetPause(1);
    }

   

}
