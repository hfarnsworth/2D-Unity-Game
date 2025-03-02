using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]  // 0 = TripleShot, 1 = Speed, 2 = Shield
    private int powerUpID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.5f)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {

                switch (powerUpID)
                {
                    case 0:
                        player.TripleShotPowerup();
                        break;
                    case 1:
                        player.SpeedPowerup();
                        break;
                    case 2:
                        player.ShieldPowerup();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
        }
    }
}
