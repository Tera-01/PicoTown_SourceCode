using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioClip _bgmStart;
    [SerializeField] private AudioClip _bgmInGame;
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioClip _walk;

    private AudioSource BGM;
    private AudioSource Sound;

    private bool _isWalking = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        BGM = gameObject.AddComponent<AudioSource>();
        BGM.volume = 0.5f;
        BGM.loop = true;

        Sound = gameObject.AddComponent<AudioSource>();
        Sound.volume = 0.8f;

        SceneManager.sceneLoaded += CheckStartScene;

        BGM.clip = _bgmStart;
        BGM.Play();
    }
    
    private void Update()
    {
        if(_isWalking)
        {
            if(!Sound.isPlaying)
            {
                Sound.clip = _walk;
                Sound.pitch = 1.8f;
                Sound.Play();
            }
        }
    }
    
    void CheckStartScene(Scene changed, LoadSceneMode loadSceneMode)
    {
        if (changed.buildIndex == 0)
        {
            BGM.clip = _bgmStart;
            BGM.Play();
        }
        else
        {
            SetBGM_InGame();
        }
    }
    
    public void SetBGM_InGame()
    {
        BGM.clip = _bgmInGame;
        BGM.Play();
    }
    
    public void PlayButtonClickSound(bool isOn)
    {
        if(isOn)
        {
            Sound.clip = _buttonClick;
            Sound.Play();
        }
    }
    
    public void SetWalkSound(int num)
    {
        _isWalking = true;
    }
    
    public void EndWalkSound()
    {
        _isWalking = false;
    }
}
