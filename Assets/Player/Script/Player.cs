using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField]
    private Transform _camera;

    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private float _powerUpDuration = 5f;

    [SerializeField]
    private Transform _respawnPoint;

    [SerializeField]
    private int _health = 3;
    private int _maxHealth;

    [SerializeField]
    private TMP_Text _healthText;

    private Rigidbody _rigidBody;
    private Coroutine _powerUpCoroutine;
    private bool _isPowerUpActive;
    private AudioSource _audioSource;

    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _maxHealth = _health;
        _audioSource = GetComponent<AudioSource>();
        //_camera = Camera.main.transform;
        //_camera = GetComponentInChildren<Camera>().transform;
    }

    private void Start()
    {
        UpdateUI();
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
        _isPowerUpActive = true;

        yield return new WaitForSeconds(_powerUpDuration);

        OnPowerUpStop?.Invoke();
        _isPowerUpActive = false;
        Debug.Log("Stop Power Up");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isPowerUpActive)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Dead();
            }
        }
    }

    private void UpdateUI()
    {
        _healthText.text = "Health: " + _health + " / " + _maxHealth;
    }

    public void Dead()
    {
        _health -= 1;

        if (_health > 0)
        {
            transform.position = _respawnPoint.position;
        }
        else
        {
            _health = 0;
            Debug.Log("Lose");
            SceneManager.LoadScene("LoseScreen");
        }

        _audioSource?.Play();

        UpdateUI();
    }

}
