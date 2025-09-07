using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField]
    public PickableType pickableType;

    public Action<Pickable> OnPicked; // Fix CS1593: Use Action<Pickable> to accept one argument

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("PIckeup : " + pickableType);
            Destroy(gameObject);

            OnPicked?.Invoke(this); // Fix IDE1005: Use null-conditional and Invoke
        }
    }

}
