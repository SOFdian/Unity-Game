using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBulletDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private AnimatorStateInfo info;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        info = animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 1)
        {
            ObjectPool.Instance.RecycleGameObject(gameObject);
        }
    }
}
