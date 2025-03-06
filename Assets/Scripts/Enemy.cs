using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private float _mirroredXRange = 10.5f;

    private Player _player;
    private Animator _anim;

    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _explosionAudio;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null) Debug.LogError("the Player is NULL.");

        _anim = GetComponent<Animator>();
        if (_anim == null) Debug.LogError("Enemy Animator is NULL.");

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null) Debug.LogError("AudioSource on the Player is NULL.");
        else _audioSource.clip = _explosionAudio;
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

        if (other.transform.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.EnemyPoints(10);
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.5f);
        }
        
        if(other.transform.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.5f);
        }
    }
}