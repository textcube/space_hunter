using UnityEngine;
using System.Collections;

public class BulletGroup2D : MonoBehaviour
{
    public GameObject[] bullets;
    public int maxBullets = 100;
    public int bulletPos = 0;
    GameObject bulletPrefab;

    private static BulletGroup2D s_instance = null;
    public static BulletGroup2D Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = FindObjectOfType(typeof(BulletGroup2D)) as BulletGroup2D;
                if (null == s_instance)
                    Debug.Log("Fail to get instance");
            }
            return s_instance;
        }
    }

    void Awake()
    {
        MakeBullets();
    }

    void MakeBullets()
    {
        bulletPrefab = Resources.Load("PlayerBulletPrefab2D", typeof(GameObject)) as GameObject;
        bullets = new GameObject[maxBullets];
        for (int i = 0; i < maxBullets; i++)
        {
            GameObject go = Instantiate(bulletPrefab) as GameObject;
            go.SetActive(false);
            go.transform.parent = transform;
            bullets[i] = go;
        }
    }

    public GameObject GetBullet()
    {
        GameObject go = bullets[bulletPos];
        bulletPos = (bulletPos + 1) % maxBullets;
        go.SetActive(true);
        return go;
    }

    void OnApplicationQuit()
    {
        s_instance = null;
    }
}
