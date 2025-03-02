using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _speedMultiplier = 2f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private int _ammoClip = 6;
    [SerializeField]
    private float _reloadTime = 1.2f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private GameObject _tripleShot;
    [SerializeField]
    private float _powerupTime = 5f;


    private bool _tripleshotActive = false;
    private bool _speedBoostActive = false;
    public bool shieldActive = false;

    [SerializeField]
    private GameObject _shieldVisualizer;

    private SpawnManager _spawnManager;
    private bool _laserCanFire = true;
    private int _shotCount = 0;

    [SerializeField]
    private int _score = 0;

    private UIManager _uiManager;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
            Debug.LogError("The Spawn Manager is NULL.");

        if (_uiManager == null)
            Debug.LogError("The UI Manager is NULL.");
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
        float calculatedSpeed = _speed;
        if (!!_speedBoostActive)
        {
            calculatedSpeed = calculatedSpeed * _speedMultiplier;
        }

        // Time.deltaTime is realtime (per second)
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * calculatedSpeed * Time.deltaTime);

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
        if(_tripleshotActive)
            Instantiate(_tripleShot, transform.position + new Vector3(0, 1.25f, 0), Quaternion.identity);
        else
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.25f, 0), Quaternion.identity);

        //if space key press,
        //if tripleshotActive is true
            //fire 3 lasers (triple shot prefab)

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

        if (shieldActive)
        {
            shieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives--;

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotPowerup()
    {
        _tripleshotActive = true;
        StartCoroutine(FiveSecondTripleShot());
    }

    public void SpeedPowerup()
    {
        _speedBoostActive = true;
        StartCoroutine(FiveSecondSpeed());
    }

    public void ShieldPowerup()
    {
        shieldActive = true;
        _shieldVisualizer.SetActive(true);
        //StartCoroutine(FiveSecondShield());
    }

    IEnumerator FiveSecondTripleShot()
    {
        yield return new WaitForSeconds(_powerupTime);
        _tripleshotActive = false;
    }

    IEnumerator FiveSecondSpeed()
    {
        yield return new WaitForSeconds(_powerupTime);
        _speedBoostActive = false;
    }

    public void EnemyPoints(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}