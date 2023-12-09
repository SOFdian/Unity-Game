using Unity.Jobs;
using UnityEngine;
public class HelperPlane : MonoBehaviour
{

    public float distance;
    public GameObject parent;
    public Rigidbody2D rb;
    //武器
    public GameObject weapon;

    public Vector3 offset;

    public float lerpSpeed = 5f;
    public float moveSpeed = 5f;

    MyTimer timer;
    public float interval;

    protected virtual void Awake()
    {
        timer = new MyTimer();
        parent = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        offset = new Vector3(Random.Range(-distance, distance), Random.Range(-distance, distance), 0); 
    }

    private void FixedUpdate()
    {
        transform.up = weapon.transform.up;
        timer.runTheClock();
        if (timer.GetTime() >= interval)
        {
            offset = new Vector3(Random.Range(-distance, distance), Random.Range(-distance, distance), 0);
            interval = Random.Range(0.5f, 1.5f);
            timer.refreshTime();
        }
        Follow();

    }
    void Follow()
    {
        Vector2 lerpTarget = Vector3.Lerp(transform.position, parent.transform.position+offset, lerpSpeed * Time.fixedDeltaTime);
        //第三个参数是每帧最大移动距离
        transform.position = Vector3.MoveTowards(transform.position, lerpTarget, moveSpeed * Time.fixedDeltaTime);
    }
}
