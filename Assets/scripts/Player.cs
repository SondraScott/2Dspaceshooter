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
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    // isTripleShotActive
    // Start is called before the first frame update
    void Start()
    {
      transform.position = new Vector3(0,0,0); 
      _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

      if(_spawnManager == null)
      {
        Debug.LogError("The Spwn Manager is NULL.");

       
      }
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
       
        //if space key pressed,
        //if tripleshotactive is true 
          //fire 3 lasers (triple shot active prefab)
          //else FireLaser 1 laser
        
        //instantiate 3 lasers (triple Shot Prefab)
            if(_isTripleShotActive == true)
            {
              Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
              Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);  
            }
        }
    public void Damage()
    {
        _lives -= 1;

        if(_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
        
    }
    public void TripleShotActive()
    {
        //tripleShotActive becomes true
        //start the power down coroutine for triple shot

        _isTripleShotActive =true;
         StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return  new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
    //wait 5 seconds
    //set the triple shot to false

}
