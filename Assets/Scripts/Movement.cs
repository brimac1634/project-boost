using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        else
        {
            _audioSource.Stop();
            mainParticles.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
            rightParticles.Play();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
            leftParticles.Play();
        }
        else{
            leftParticles.Stop();
            rightParticles.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        _rigidbody.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        _rigidbody.freezeRotation = false;
    }

}
