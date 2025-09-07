using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickableManager : MonoBehaviour
{
    private List<Pickable> _pickableList = new List<Pickable>();

    [SerializeField]
    private Player _player;

    [SerializeField]
    private ScoreManager _scoreManager;

    private void Start()
    {
        InitPickableList();
    }

    private void InitPickableList()
    {
        _pickableList.Clear();

        Pickable[] pickableObjects = GameObject.FindObjectsOfType<Pickable>();
        for (int i = 0; i < pickableObjects.Length; i++)
        {
            _pickableList.Add(pickableObjects[i]);
            pickableObjects[i].OnPicked += OnPickablePicked;
        }
        Debug.Log("Pickable List: " + _pickableList.Count);
        _scoreManager?.SetMaxScore(_pickableList.Count);
    }

    private void OnPickablePicked(Pickable pickable)
    {
        _pickableList.Remove(pickable);
        Destroy(pickable.gameObject);
        Debug.Log("Pickable List: " + _pickableList.Count);

        _scoreManager?.AddScore(1);


        if (pickable.pickableType == PickableType.PowerUp)
        {
            _player?.PickPowerUp();
        }

        if (_pickableList.Count <= 0)
        {
            Debug.Log("Win");
            SceneManager.LoadScene("WinScreen");
        }
    }
}
