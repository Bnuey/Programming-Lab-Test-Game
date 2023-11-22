using System;
using UnityEngine;

public class coinBulletScript : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;

    private void Awake()
    {
        Destroy(gameObject, 3);
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            Destroy(gameObject);
            Debug.Log("Coin Hit");
        }
        
    }
}
