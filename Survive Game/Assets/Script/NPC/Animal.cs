using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Animal : MonoBehaviour
{
    [SerializeField] protected string animalName; //이름
    [SerializeField] protected int hp; // 동물의 체력
    [SerializeField] protected float walkSpeed; //걷기 스피드
    [SerializeField] protected float runSpeed; // 뛰기 스피드
    
    
    protected Vector3 destination; //방향 -> 목적지

    //상태변수
    protected bool isAction; //행동중인지 아닌지
    protected bool isWalking; //걷는지 안 걷는지
    protected bool isRunning; //뛰는지 판별
    protected bool isDead; //죽었는지 판별

    [SerializeField] protected float walkTime; //걷는 시간
    [SerializeField] protected float waitTime; //대기 시간, (풀 뜯고 이런거)
    [SerializeField] protected float runTime;
    protected float currentTime;

    //필요한 컴포넌트
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected BoxCollider boxCol;
    protected AudioSource theAudio;
    protected NavMeshAgent nav;

    [SerializeField] protected AudioClip[] sound_Normal;
    [SerializeField] protected AudioClip sound_Hurt;
    [SerializeField] protected AudioClip sound_Dead;


    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        theAudio = GetComponent<AudioSource>();
        currentTime = waitTime;
        isAction = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
           
        }

    }
    void FixedUpdate()
    {
        if (!isDead)
        {
            Move();
            ElapseTime();
        }
    }

    protected void Move()
    {
        if (isWalking || isRunning)
        {
            nav.SetDestination(transform.position + destination * 5f);
            //rigid.MovePosition(transform.position + (transform.forward * applySpeed * Time.deltaTime));
        }
    }
    protected void ElapseTime()
    {
        currentTime -= Time.fixedDeltaTime;
        if (currentTime <= 0)
        {
            //다음 랜덤 행동 개시
            ReSet();
        }
    }
    protected virtual void ReSet()
    {
        isWalking = false;
        isAction = true;
        isRunning = false;
        nav.speed = walkSpeed;
            
        nav.ResetPath();
        nav.velocity = Vector3.zero;

        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        destination.Set(Random.Range(-0.2f , 0.2f),transform.position.y,Random.Range(0.5f,1f));
       
    }



    protected void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        currentTime = walkTime;
        nav.speed = walkSpeed;
        Debug.Log("걷기");
    }
   
    public virtual void Damage(int _dmg, Vector3 _targetPos)
    {
        if (!isDead)
        {
            hp -= _dmg;
            if (hp <= 0)
            {
                Dead();
                return;
            }
            PlaySE(sound_Hurt);
            anim.SetTrigger("Hurt");
            
        }

    }

    protected void Dead()
    {
        PlaySE(sound_Dead);
        isWalking = false;
        isRunning = false;
        isDead = true;
        anim.SetTrigger("Dead");

        nav.updatePosition = false;
        nav.updateRotation = false;
        nav.isStopped = true;
        nav.velocity = Vector3.zero;


    }
    protected void RandomSound()
    {
        int _random = Random.Range(0, 3); // 일상 사운드는 3개
        PlaySE(sound_Normal[_random]);
    }
    protected void PlaySE(AudioClip _clip)
    {
        theAudio.clip = _clip;
        theAudio.Play();
    }
}
