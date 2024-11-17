using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private int _ammoClip = 6;
    [SerializeField]
    private float _reloadTime = 1.2f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;


    private bool _laserCanFire = true;
    private int _shotCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
            Debug.LogError("The Spawn Manager is NULL.");

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && _laserCanFire)
            ShootLaser();
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Time.deltaTime is realtime (per second)
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        float edgeWrap = 11.3f;
        if (transform.position.x >= edgeWrap)
        {
            transform.position = new Vector3(-edgeWrap, transform.position.y, 0);
        }
        else if (transform.position.x <= -edgeWrap)
        {
            transform.position = new Vector3(edgeWrap, transform.position.y, 0);
        }
    }

    void ShootLaser()
    {
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.25f, 0), Quaternion.identity);
        _shotCount += 1;
        if (_shotCount >= _ammoClip)
        {
            _laserCanFire = false;
            StartCoroutine(LaserReloadTimer());
        }
    }

    IEnumerator LaserReloadTimer()
    {
        yield return new WaitForSeconds(_reloadTime);
        _shotCount = 0;
        _laserCanFire = true;
    }

    public void Damage()
    {
        _lives--;

        if (_lives < 1 )
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}