using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Pig : MonoBehaviour
{
    [SerializeField] private string animalName; //이름
    [SerializeField] private int hp; // 동물의 체력
    [SerializeField] private float walkSpeed; //걷기 스피드

    private Vector3 direction; //방향

    //상태변수
    private bool isAction; //행동중인지 아닌지
    private bool isWalking; //걷는지 안 걷는지

    [SerializeField] private float walkTime; //걷는 시간
    [SerializeField] private float waitTime; //대기 시간, (풀 뜯고 이런거)
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
        if (isWalking)
        {
            Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, direction, 0.01f);
            rigid.MoveRotation(Quaternion.Euler(_rotation));
        }
    }
    private void Move()
    {
        if (isWalking)
        {
            rigid.MovePosition(transform.position+ (transform.forward * walkSpeed *Time.deltaTime));
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
        anim.SetBool("Walking", isWalking);
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
        
        Debug.Log("걷기");
    }
}
