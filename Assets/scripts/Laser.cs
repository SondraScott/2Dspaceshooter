﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;

    // Update is called once per frame
    void Update()
    {
       transform.Translate(Vector3.up * _speed * Time.deltaTime); 

       if(transform.position.y > 8f)
       {
        //check if this object has a parant
        //and if it does 
        //destroy parent too
        if(transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
         Destroy(this.gameObject);
       }
    }
}
