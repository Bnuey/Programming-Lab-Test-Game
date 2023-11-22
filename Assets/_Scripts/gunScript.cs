using UnityEngine;

public class gunScript : MonoBehaviour
{
    [SerializeField] private GameObject bulletFab;
    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletFab);
        bullet.transform.position = Camera.main.transform.position;
        bullet.transform.rotation = Camera.main.transform.rotation;
    }
}
