using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 4f;

    private Vector3 _startingPosition;
    private float _movementFactor;


    void Start()
    {
        _startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; //continually growing over time
        const float tau = Mathf.PI * 2; //constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); //going from -1 to 1

        _movementFactor = (rawSinWave + 1f) / 2; //recalculate to go from 0 to 1

        Vector3 offset = movementVector * _movementFactor;
        transform.position = _startingPosition + offset;
    }
}
