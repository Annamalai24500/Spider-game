using System;
using System.Collections;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Camera playerCamera;
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;
    public int bulletsPerBurst = 3;
    //currentBurst to keep track of how many bullets are left in the burst
    public int currentBurst;
    public float SpreadIntensity;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 25f;
    public float bulletPrefabLifetime = 3f;
    //for the effect
    public GameObject bulleteffect;
    public enum ShootingMode
    {
        Automatic,
        Burst,
        Single
    }
    public ShootingMode shootingMode;
    private void Awake()
    {
        readyToShoot = true;
        currentBurst = bulletsPerBurst;
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
          //  FireWeapon();
        //}
        if(shootingMode == ShootingMode.Automatic)
        {
            isShooting = Input.GetMouseButtonDown(0);
            shootingDelay = 0.01f;
        }else if(shootingMode == ShootingMode.Burst || shootingMode == ShootingMode.Single)
        {
            isShooting = Input.GetMouseButtonDown(0);
        }

        if(readyToShoot && isShooting)
        {
            currentBurst = bulletsPerBurst;
            FireWeapon();
        }
    }
    private void FireWeapon()
    {
        readyToShoot = false;
        if (bulleteffect)
        {
            bulleteffect.SetActive(true);
            Invoke(nameof(Hideeffect), 0.05f);
        }
        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;
        //Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bullet.transform.forward = shootingDirection;
        //Shoot the bullet
        bullet.GetComponent<BulletScript>().Targettag = "Enemy";
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection*bulletSpeed,ForceMode.Impulse);
        //Destroy the bullet after some time
        //alt plus enter nice move
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifetime));
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }
        if(shootingMode == ShootingMode.Burst && currentBurst > 1)
        {
            currentBurst--;
            Invoke("FireWeapon", shootingDelay);
        }
    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        //Shooting from the middleof the screen osee where we are pointing at.
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(1000); //Some far point
        }
        Vector3 direction = targetPoint - bulletSpawn.position;
        float x = UnityEngine.Random.Range(-SpreadIntensity, SpreadIntensity);
        float y = UnityEngine.Random.Range(-SpreadIntensity, SpreadIntensity);
        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float bulletPrefabLifetime)
    {
       yield return new WaitForSeconds(bulletPrefabLifetime);
       Destroy(bullet);
    }
    private void Hideeffect()
    {
        bulleteffect.SetActive(false);
    }
}
