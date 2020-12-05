using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpot : MonoBehaviour
{
    public int track;
    public bool disapear;
    private AudioManager _audioManager;
    public GameObject nextAudio;

    // Start is called before the first frame update
    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            _audioManager.audioCanBePlayed = true;
            _audioManager.PlayNewTrack(track);

            if (disapear)
            {
                transform.gameObject.SetActive(false);
            }

            if (nextAudio != null && !nextAudio.activeInHierarchy)
            {
                nextAudio.SetActive(true);
                transform.gameObject.SetActive(false);
            }
        }
    }
}
