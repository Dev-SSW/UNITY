using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using Random = UnityEngine.Random;

public class Pig : MonoBehaviour
{
    [SerializeField] private string animalName; //이름
    [SerializeField] private int hp; // 동물의 체력
    [SerializeField] private float walkSpeed; //걷기 스피드
    [SerializeField] private float runSpeed; // 뛰기 스피드
    private float applySpeed;
    private Vector3 direction; //방향

    //상태변수
    private bool isAction; //행동중인지 아닌지
    private bool isWalking; //걷는지 안 걷는지
    private bool isRunning; //뛰는지 판별

    [SerializeField] private float walkTime; //걷는 시간
    [SerializeField] private float waitTime; //대기 시간, (풀 뜯고 이런거)
    [SerializeField] private float runTime;
    private float currentTime;

    //필요한 컴포넌트
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private BoxCollider boxCol;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = waitTime;
        isAction = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotation();
        
    }
    private void FixedUpdate()
    {
        ElapseTime();
    }

    private void Rotation()
    {
        if (isWalking || isRunning)
        {
            Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, new Vector3 (0f, direction.y ,0f), 0.01f);
            rigid.MoveRotation(Quaternion.Euler(_rotation));
        }
    }
    private void Move()
    {
        if (isWalking || isRunning)
        {
            rigid.MovePosition(transform.position+ (transform.forward * applySpeed *Time.deltaTime));
        }
    }
    private void ElapseTime()
    {
        currentTime -= Time.deltaTime;
        if(currentTime <= 0)
        {
            //다음 랜덤 행동 개시
            ReSet();
        }
    }
    private void ReSet()
    {
        isWalking = false;
        isAction = true;
        isRunning = false;
        applySpeed = walkSpeed;
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        direction.Set(0f, Random.Range(0f,360f), 0f);
        RandomAction();
    }
    private void RandomAction()
    {
        int _random = Random.Range(0, 4);

        if(_random == 0)
        {
            Wait();
        }
        else if (_random == 1)
        {
            Eat();
        }
        else if (_random == 2)
        {
            Peek();
        }
        else if (_random == 3)
        {
            TryWalk();
        }
    }

    private void Wait()
    {
        currentTime = waitTime;
        Debug.Log("대기");
    }
    private void Eat()
    {
        currentTime = waitTime;
        anim.SetTrigger("Eat");
        Debug.Log("풀 뜯기");
    }
    private void Peek()
    {
        currentTime = waitTime;
        anim.SetTrigger("Peek");
        Debug.Log("두리번");
    }
    private void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        currentTime = walkTime;
        applySpeed = walkSpeed;
        Debug.Log("걷기");
    }
    private void Run(Vector3 _targetPos)
    {
        direction = Quaternion.LookRotation(transform.position - _targetPos).eulerAngles;

        currentTime = runTime;
        isWalking = false;
        isRunning = true;
        applySpeed = runSpeed;
        anim.SetBool("Running", isRunning);
    }
    public void Damage(int _dmg , Vector3 _targetPos)
    {
        hp -= _dmg;
        if (hp <= 0)
        {
            Debug.Log("체력 0이하");
            return;
        }
        anim.SetTrigger("Hurt");
        Run(_targetPos); 
    }

}
