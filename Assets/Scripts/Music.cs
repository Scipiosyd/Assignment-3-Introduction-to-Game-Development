using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip introsong;
    public AudioClip normalghostsong;
    private AudioSource audiosource;
    


    // Start is called before the first frame update
    void Start()
    {

        audiosource = GetComponent<AudioSource>();
        if (audiosource == null)
        {
            audiosource = gameObject.AddComponent<AudioSource>();
        }


        audiosource.clip = introsong;
        audiosource.loop = false;
        audiosource.Play();

        StartCoroutine(Playmusic(3f));

       

        
    }

    private IEnumerator Playmusic(float songlength)
    {


        yield return new WaitForSeconds(3f);
        

        audiosource.clip = normalghostsong;
        audiosource.volume = 0.1f;
        audiosource.loop = true;
        audiosource.Play();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
