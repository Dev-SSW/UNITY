using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using Random = UnityEngine.Random;

public class Pig : MonoBehaviour
{
    [SerializeField] private string animalName; //�̸�
    [SerializeField] private int hp; // ������ ü��
    [SerializeField] private float walkSpeed; //�ȱ� ���ǵ�
    [SerializeField] private float runSpeed; // �ٱ� ���ǵ�
    private float applySpeed;
    private Vector3 direction; //����

    //���º���
    private bool isAction; //�ൿ������ �ƴ���
    private bool isWalking; //�ȴ��� �� �ȴ���
    private bool isRunning; //�ٴ��� �Ǻ�

    [SerializeField] private float walkTime; //�ȴ� �ð�
    [SerializeField] private float waitTime; //��� �ð�, (Ǯ ��� �̷���)
    [SerializeField] private float runTime;
    private float currentTime;

    //�ʿ��� ������Ʈ
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
            //���� ���� �ൿ ����
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
        Debug.Log("���");
    }
    private void Eat()
    {
        currentTime = waitTime;
        anim.SetTrigger("Eat");
        Debug.Log("Ǯ ���");
    }
    private void Peek()
    {
        currentTime = waitTime;
        anim.SetTrigger("Peek");
        Debug.Log("�θ���");
    }
    private void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        currentTime = walkTime;
        applySpeed = walkSpeed;
        Debug.Log("�ȱ�");
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
            Debug.Log("ü�� 0����");
            return;
        }
        anim.SetTrigger("Hurt");
        Run(_targetPos); 
    }

}
