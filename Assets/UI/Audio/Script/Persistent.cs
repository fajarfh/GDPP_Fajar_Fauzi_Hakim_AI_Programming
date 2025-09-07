/*
 * Class untuk GameObject persistent
 * Yang digunakan lintas scene
 * Hanya untuk memutar musik
 */


using System.Collections;
using UnityEngine;

public class Persistent : MonoBehaviour
{
    [SerializeField] private AudioSource musik;
    [SerializeField] private float loop_delay;
    //private AudioListener audioListener;

    private void Awake()
    {
        
        // Ensure this GameObject persists across scenes
        DontDestroyOnLoad(gameObject);

        // Optional: Prevent duplicates if multiple instances exist
        if (FindObjectsOfType<Persistent>().Length > 1)
        {
            Destroy(gameObject);
        }

        if ((musik != null) && (!musik.isPlaying) && (musik.enabled))
        {
            // Menggunakan fungsi khusus untuk mengulang-ulang musik dengan delay
            StartCoroutine(PlayMusicWithDelay());
        }

        //audioListener = GetComponent<AudioListener>();
        //if (FindObjectsOfType<AudioListener>().Length > 1)
        //{
        //    audioListener.gameObject.SetActive(false);
        //}
        //else
        //{
        //    audioListener.gameObject.SetActive(true);
        //}


    }

    private IEnumerator PlayMusicWithDelay()
    {
        while (true)
        {
            musik.Play(); 
            yield return new WaitForSeconds(musik.clip.length + loop_delay);
        }
    }

}
