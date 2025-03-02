﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private float _mirroredXRange = 10.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        float randomX = Random.Range(-_mirroredXRange, _mirroredXRange);
        if (transform.position.y < -5.5f)
        {
            transform.position = new Vector3(randomX, 7.6f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Laser")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
        else if(other.transform.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }
    }
}