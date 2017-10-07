using UnityEngine;
using System.Collections;

public class EnemyControl2D : MonoBehaviour
{
    public float speed = 5f;
    Transform tr;
    public int healthMax = 100;
    public int healthState = 100;
    //public GameObject bulletPrefab = null;
    Transform detonatorGroup = null;
    GameObject detonatorPrefab = null;
    public Vector2 size;
    SpriteRenderer spriteRenderer;
    PlayerControl2D playerScript;

    void Start()
    {
        tr = transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl2D>();
        Bounds bounds = spriteRenderer.bounds;
        size = bounds.size;
        //bulletPrefab = Resources.Load("EnemyBulletPrefab", typeof(GameObject)) as GameObject;
        detonatorGroup = GameObject.Find("DetonatorGroup").transform;
        detonatorPrefab = Resources.Load("ExplodeEffect", typeof(GameObject)) as GameObject;
    }

    void Update()
    {
        if (tr.position.y < -14)
            Death(false);
        else
            tr.position += Time.deltaTime * Vector3.down * speed;
    }

    void InitHealthBar()
    {
        SetHealthState(healthMax);
    }

    public void SetHealthDamage(int damage)
    {
        int state = healthState - damage;
        SetHealthState(state);
    }

    public void SetHealthState(int state)
    {
        if (state > healthMax) state = healthMax;
        if (state < 0) state = 0;
        float ratio = (float)state / healthMax;
        healthState = state;
        if (state == 0)
        {
            if (playerScript)
                playerScript.SetPlayerScore(healthMax);
            Death(true);
        }
    }

    void ExplodeEffect(Vector3 vec)
    {
        GameObject instance = Instantiate(detonatorPrefab, vec, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward)) as GameObject;
        instance.transform.parent = detonatorGroup;
    }

    void Death(bool effect)
    {
        float x = Random.Range(-4.8f, 4.8f);
        if (effect)
        {
            if (size.y > 2.5f)
            {
                Vector3 pos;
                pos = new Vector3(0.5f, -0.5f, 0f);
                ExplodeEffect(transform.position + pos);
                pos = new Vector3(-0.5f, -0.5f, 0f);
                ExplodeEffect(transform.position + pos);
                pos = new Vector3(0f, 0.5f, 0f);
                ExplodeEffect(transform.position + pos);
            }
            ExplodeEffect(transform.position);
        }
        tr.position = new Vector3(x, 14f, tr.position.z);
        InitHealthBar();
    }
}
