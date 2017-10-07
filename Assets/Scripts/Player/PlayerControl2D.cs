using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControl2D : MonoBehaviour
{
    Transform tr;
    public float fireDelayTime = 0.2f;
    public float tripSpeed = 2f;
    public Text scoreText;
    public Image[] heartImages;
    public Image startImage;
    public SpriteRenderer bodyDamage;
    public int life = 3;
    int score = 0;
    Transform shootTran;
    AudioClip audioFireClip = null;
    AudioSource audioFireSource = null;
    Transform detonatorGroup = null;
    GameObject detonatorPrefab = null;

    bool isGameOver = true;

    Vector3 startPosition;

    void Start()
    {
        tr = transform;
        startPosition = tr.position;
        shootTran = tr.Find("shoot");
        if (audioFireClip == null) audioFireClip = Resources.Load("shoot", typeof(AudioClip)) as AudioClip;
        audioFireSource = gameObject.AddComponent<AudioSource>();
        audioFireSource.volume = 0.2f;
        audioFireSource.clip = audioFireClip;
        detonatorGroup = GameObject.Find("DetonatorGroup").transform;
        detonatorPrefab = Resources.Load("ExplodeEffect", typeof(GameObject)) as GameObject;

        StartCoroutine(MakeBullet(fireDelayTime));

        GameStart();
    }

    void InitPlayer()
    {
        life = 3;
        DrawLife(life);
        score = 0;
        DrawScore(score);
        InitBodyDamage();
    }

    public void OnClickStartButton()
    {
        GameStart();
    }

    void GameStart()
    {
        InitPlayer();
        isGameOver = false;
        startImage.enabled = false;
    }

    void GameOver()
    {
        Vector3 pos;
        pos = new Vector3(0.5f, -0.5f, 0f);
        ExplodeEffect(transform.position + pos);
        pos = new Vector3(-0.5f, -0.5f, 0f);
        ExplodeEffect(transform.position + pos);
        pos = new Vector3(0f, 0.5f, 0f);
        ExplodeEffect(transform.position + pos);
        ExplodeEffect(transform.position);
        isGameOver = true;
        startImage.enabled = true;
        tr.position = startPosition;
    }

    void ExplodeEffect(Vector3 vec)
    {
        GameObject instance = Instantiate(detonatorPrefab, vec, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward)) as GameObject;
        instance.transform.parent = detonatorGroup;
    }

    public void SetPlayerScore(int point)
    {
        score += point + Random.Range(0,100);
        DrawScore(score);
    }

    void DrawScore(int point)
    {
        scoreText.text = point.ToString();
    }

    public void SetHealthDamage(int damage)
    {
        life -= damage;
        if (life < 0) life = 0;
        if (life < 1) GameOver();
        DrawLife(life);
    }

    void DrawLife(int point)
    {
        for (int i=0; i<heartImages.Length; i++)
            heartImages[i].enabled = i < point;
    }

    public void MoveToTarget()
    {
        if (isGameOver) return;
        Vector3 pos = Joystick.Instance.delta * (tripSpeed / 100f);
        float x = Mathf.Clamp(tr.position.x + pos.x, -4f, 4f);
        float y = Mathf.Clamp(tr.position.y + pos.y, -6f, 0f);
        Vector3 p = new Vector3(x, y, 0f);
        tr.position = p;
    }

    public void SetDamageFrom(GameObject collider)
    {
        if (isGameOver) return;
        EnemyControl2D script = collider.transform.parent.GetComponent<EnemyControl2D>();
        script.SetHealthState(0);
        SetHealthDamage(1);
        DrawDamageEffect();
    }

    public void DrawDamageEffect()
    {
        for (int i=0; i<8; i++)
            Invoke("DrawBodyDamage", 0.1f * i);
    }

    void InitBodyDamage()
    {
        bodyDamage.enabled = false;
    }

    void DrawBodyDamage()
    {
        bodyDamage.enabled = !bodyDamage.enabled;
    }

    void SpawnBullet()
    {
        if (isGameOver) return;
        GameObject go;
        audioFireSource.Play();
        Vector3 pos = shootTran.position;

        go = BulletGroup2D.Instance.GetBullet();
        go.transform.position = pos - Vector3.right * 0.1f;

        go = BulletGroup2D.Instance.GetBullet();
        go.transform.position = pos + Vector3.right * 0.1f;

        go = BulletGroup2D.Instance.GetBullet();
        go.transform.position = pos + Vector3.right * 0.1f;
        go.transform.rotation = Quaternion.AngleAxis(15, Vector3.forward);

        go = BulletGroup2D.Instance.GetBullet();
        go.transform.position = pos - Vector3.right * 0.1f;
        go.transform.rotation = Quaternion.AngleAxis(-15, Vector3.forward);
    }

    IEnumerator MakeBullet(float delayTime)
    {
        SpawnBullet();
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(MakeBullet(delayTime));
    }
}
