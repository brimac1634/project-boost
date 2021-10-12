using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationSpeed = 150f;
    [SerializeField] AudioClip mainEngineSound;
    [SerializeField] ParticleSystem mainParticles;
    [SerializeField] ParticleSystem leftParticles;
    [SerializeField] ParticleSystem rightParticles;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = true;

        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

        void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    private void StartThrusting()
    {
        _rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(mainEngineSound);
        }
        if (!mainParticles.isPlaying)
        {
            mainParticles.Play();
        }
    }

    private void StopThrusting()
    {
        _audioSource.Stop();
        mainParticles.Stop();
    }

    

    private void RotateLeft()
    {
        ApplyRotation(rotationSpeed);
        if (!rightParticles.isPlaying)
        {
            rightParticles.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationSpeed);
        if (!leftParticles.isPlaying)
        {
            leftParticles.Play();
        }
    }

    private void StopRotating()
    {
        leftParticles.Stop();
        rightParticles.Stop();
    }


    private void ApplyRotation(float rotationThisFrame)
    {
        _rigidbody.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        _rigidbody.freezeRotation = false;
    }

}
