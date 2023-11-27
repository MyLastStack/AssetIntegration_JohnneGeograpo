using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] ParticleSystem lightFX;
    [SerializeField] ParticleSystem explosionFX;
    [SerializeField] GameObject explosion;

    [SerializeField] GameObject chicken;

    float speed = 3.5f;
    float boomDuration;

    bool move;

    void Start()
    {
        explosion.SetActive(false);
        move = true;
        boomDuration = explosionFX.duration + explosionFX.startLifetime;
    }

    void Update()
    {
        if (move)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    void DestroyItself()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Terrain") || collision.transform.CompareTag("AIControlled"))
        {
            move = false;

            lightFX.Stop();
            chicken.SetActive(false);

            explosion.SetActive(true);
            explosionFX.Play();

            Destroy(collision.gameObject);

            Invoke("DestroyItself", boomDuration);
        }
    }
}
