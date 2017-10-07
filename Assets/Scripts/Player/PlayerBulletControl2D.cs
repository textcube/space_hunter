using UnityEngine;
using System.Collections;

public class PlayerBulletControl2D : MonoBehaviour
{
    public float speed = 16f;
    Transform tr;
    public int damage = 20;
    public GameObject detonatorPrefab = null;
    Transform detonatorGroup = null;

    void Start()
    {
        tr = transform;
        detonatorGroup = GameObject.Find("DetonatorGroup").transform;
        detonatorPrefab = Resources.Load("HitEffect", typeof(GameObject)) as GameObject;
    }

    void Update()
    {
        if (tr.position.y > 10 || tr.position.y < -10 || tr.position.x > 6 || tr.position.x < -6)
        {
            gameObject.SetActive(false);
        }
        else
        {
            float angle = tr.eulerAngles.x;
            tr.position += Time.deltaTime * (transform.rotation * Vector3.up) * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collider = other.gameObject;
        if (collider.CompareTag("Enemy"))
        {
            EnemyControl2D script = collider.transform.parent.GetComponent<EnemyControl2D>();
            script.SetHealthDamage(damage);
            GameObject instance = Instantiate(detonatorPrefab, tr.position /* + Vector3.up * .1f */ , Quaternion.identity) as GameObject;
            instance.transform.parent = detonatorGroup;
            gameObject.SetActive(false);
        }
    }
}
