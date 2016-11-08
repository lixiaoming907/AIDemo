using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PeetMove : MonoBehaviour
{

    public Transform playerTrans;

    public float maxDisToPlayer = 10;
    public float curDisToPlayer = 0;
    public float moveSpeed = 10;
    [HideInInspector]
    public List<Vector3> enemyPoses = new List<Vector3>(); //敌人列表
    //public float rotateSpeed = 10;

    private Animator anim;
    private Vector3 targetPos;
    private Vector3 minDisToEnemy;
    private Quaternion targetQua;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        curDisToPlayer = Vector3.Distance(transform.position, playerTrans.position);
        if (curDisToPlayer > maxDisToPlayer) //距离玩家prefab的距离超过了跟随最大距离
        {

        }
        else
        {
            anim.SetBool("move", false);
        }

        if (enemyPoses.Count > 0)
        {
            minDisToEnemy = enemyPoses[0] - transform.position;
            LookAtSomething(minDisToEnemy, false);
        }
        else
        {
            minDisToEnemy = playerTrans.position - transform.position;
            LookAtSomething(minDisToEnemy, true);
        }

    }

    //当有怪物的时候就看向怪物，没有怪物的时候就看向玩家prefab
    void LookAtSomething(Vector3 targetPos, bool needLookPlayer)
    {
        targetQua = Quaternion.LookRotation(targetPos);
        targetQua.y = 0;
        transform.rotation = targetQua;
        //transform.rotation = Quaternion.Slerp(transform.rotation,targetQua,Time.time);
    }

    //跟随玩家prefab移动
    void FollowPlayer()
    {
        anim.SetBool("move", true);

        targetPos = playerTrans.position - transform.position;
        transform.Translate(targetPos * Time.deltaTime * moveSpeed);
    }
}
