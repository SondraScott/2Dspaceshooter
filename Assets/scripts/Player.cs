﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = -1f;
    [SerializeField]
    private int _lives = 3;
    // Start is called before the first frame update
    void Start()
    {
      transform.position = new Vector3(0,0,0);  
    }
    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if(Input.GetKeyDown(KeyCode.Space)&& Time.time >  _nextFire)
        {
            FireLaser();
        }
    }
    void CalculateMovement()
    {    
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput  = Input.GetAxis("Vertical");

       transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
       transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

       if(transform.position.y >= 0)
       {
        transform.position = new Vector3(transform.position.x,0,0);
       }
       else if (transform.position.y <= -3.8f)
       {
        transform.position = new Vector3(transform.position.x, -3.8f, 0);
       }

       if (transform.position.x > 11.3f)
       {
        transform.position = new Vector3(-11.3f, transform.position.y, 0);
       }
        else if (transform.position.x < -11.3f)
        { 
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }   
    }
    void FireLaser()
    {
         _nextFire = Time.time + _fireRate;
          Instantiate(_laserPrefab, transform.position + new Vector3(0,0.8f,0), Quaternion.identity);   
    }
    public void Damage()
    {
        _lives -= 1;

        if(_lives < 1)
        {
            Destroy(this.gameObject);
        }
        
    }
}
