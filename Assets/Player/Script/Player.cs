using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private Transform _camera;

    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private float _powerUpDuration = 5f;

    private Rigidbody _rigidBody;
    private Coroutine _powerUpCoroutine;

    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        //_camera = Camera.main.transform;
        //_camera = GetComponentInChildren<Camera>().transform;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //Debug.Log("Horizontal: " + horizontal);
        //Debug.Log("Vertical: " + vertical);

        Vector3 horizontalDirection = horizontal * _camera.right;
        Vector3 verticalDirection = vertical * _camera.forward;
        verticalDirection.y = 0;
        horizontalDirection.y = 0;

        Vector3 movementDirection = horizontalDirection + verticalDirection;
        _rigidBody.velocity = movementDirection * _speed * Time.fixedDeltaTime;
    }

    public void PickPowerUp()
    {
        Debug.Log("Pick Power Up");
        if (_powerUpCoroutine != null)
        {
            StopCoroutine(_powerUpCoroutine);
        }
        _powerUpCoroutine = StartCoroutine(StartPowerUp());
    }

    private IEnumerator StartPowerUp()
    {
        Debug.Log("Start Power Up");
        OnPowerUpStart?.Invoke();

        yield return new WaitForSeconds(_powerUpDuration);

        OnPowerUpStop?.Invoke();
        Debug.Log("Stop Power Up");
    }
}
