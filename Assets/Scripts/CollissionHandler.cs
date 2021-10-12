using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollissionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    private AudioSource _audioSource;

    private bool _isTransitioning = false;
    private bool _collisionDisabled = false;

    private void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        ProcessCheats();
    }

    private void OnCollisionEnter(Collision other) {
        if (_isTransitioning || _collisionDisabled) { return; }

        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                Debug.Log("Finish");
                StartSuccessSequence();
                break;
            default:
                Debug.Log("Explode");
                StartCrashSequence();
                break;
        }
    }

    void ProcessCheats()
    {
        if (Input.GetKey(KeyCode.L))
        {
            NextLevel();
        } 
        else if (Input.GetKey(KeyCode.C))
        {
            _collisionDisabled = !_collisionDisabled;
        }
    }


    private void StartSuccessSequence()
    {
        _isTransitioning = true;
        // TODO: add particle effect
        GetComponent<Movement>().enabled = false;
        
        _audioSource.Stop();
        _audioSource.PlayOneShot(successSound);
        successParticles.Play();

        Invoke("NextLevel", levelLoadDelay);
    }

    private void StartCrashSequence()
    {
        _isTransitioning = true;

        // TODO: add particle effect
        GetComponent<Movement>().enabled = false;

        _audioSource.Stop();
        _audioSource.PlayOneShot(crashSound);
        crashParticles.Play();

        Invoke("ReloadLevel", levelLoadDelay);
    }

    private void ReloadLevel()
    {
        int _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(_currentSceneIndex);
    }

    private void NextLevel()
    {
        int _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int _sceneCount = SceneManager.sceneCountInBuildSettings;

        if (_currentSceneIndex < _sceneCount - 1)
        {
            SceneManager.LoadScene(_currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
