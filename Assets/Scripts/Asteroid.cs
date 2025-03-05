using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 18f;
    [SerializeField]
    private GameObject _explosion;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null) Debug.LogError("Spawn Manager is NULL.");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Laser")
        {
            Vector3 location = transform.position;
            Destroy(other.gameObject);

            Destroy(this.gameObject, 0.25f);
            Instantiate(_explosion, location, Quaternion.identity);
            _spawnManager.StartSpawning();
        }

    }
}
