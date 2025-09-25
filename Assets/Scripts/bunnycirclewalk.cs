using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class bunnycirclewalk : MonoBehaviour
{


    public float speed = 1f;
    public float moveverticaltime = 1f;
    private float timeelapsed = 0f;
    private float distpertile = 0.31775f;  //  1.271/4  1.271 is position x delta
    [SerializeField]
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
      
        
    }

    // Update is called once per frame
    void Update()
    {


        if (timeelapsed < distpertile * 4)
        {
            animator.Play("bunup");
            float movedistance = speed * Time.deltaTime;
            transform.position += Vector3.up * movedistance;
            timeelapsed += Time.deltaTime;
            

        }

        if( timeelapsed > distpertile * 4 && timeelapsed < distpertile * 9)
        {
            animator.Play("bunright");
            transform.position += Vector3.right * (speed * Time.deltaTime);
            timeelapsed += Time.deltaTime;

        }

        if(timeelapsed > distpertile * 9 && timeelapsed < distpertile * 13)
        {
            animator.Play("bundown");
            transform.position += Vector3.down * (speed * Time.deltaTime);
            timeelapsed += Time.deltaTime;
        }

        if(timeelapsed > distpertile * 13 &&  timeelapsed < distpertile * 18)
        {
            animator.Play("bunleft");
            transform.position += Vector3.left * (speed * Time.deltaTime);
            timeelapsed += Time.deltaTime; 
        }


        if(timeelapsed >= distpertile * 18)
        {
            timeelapsed = 0f; 
        }


    }



}
