using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    [SerializeField] private string animalName; //�̸�
    [SerializeField] private int hp; // ������ ü��
    [SerializeField] private float walkSpeed; //�ȱ� ���ǵ�
    //���º���
    private bool isAction; //�ൿ������ �ƴ���
    private bool isWalking; //�ȴ��� �� �ȴ���

    [SerializeField] private float walkTime; //�ȴ� �ð�
    [SerializeField] private float waitTime; //��� �ð�, (Ǯ ��� �̷���)
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
        ElapseTime();
    }
    private void ElapseTime()
    {
        currentTime -= Time.deltaTime;
        if(currentTime <= 0)
        {
            //���� ���� �ൿ ����
            RandomAction();
        }
    }
    private void RandomAction()
    {
        isAction = true;
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
        Debug.Log("Ǯ ���");
    }
    private void Peek()
    {
        currentTime = waitTime;
        Debug.Log("�θ���");
    }
    private void TryWalk()
    {
        currentTime = walkTime;
        Debug.Log("�ȱ�");
    }
}
