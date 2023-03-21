using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatusController : MonoBehaviour
{
    [SerializeField]
    private int hp;
    private int currenthp;

    [SerializeField]
    private int sp;
    private int currentsp;

    [SerializeField]
    private int spIncreaseSpeed;

    [SerializeField] //스태미너 재회복 딜레이
    private int spRechargeTime;
    private int currentspRechargeTime;

    //스태미너 감소 여부
    private bool spUsed;

    [SerializeField]
    private int dp;
    private int currentdp;

    [SerializeField]
    private int hungry;
    private int currenthungry;

    [SerializeField]
    private int hungryDecreaseTime;
    private int currenthungryDecreaseTime;

    [SerializeField]
    private int thirsty;
    private int currentthirsty;

    [SerializeField]
    private int thirstyDecreaseTime;
    private int currentthirstyDecreaseTime;

    [SerializeField]
    private int satisfy;
    private int currentsatisfy;
    //필요한 이미지
    [SerializeField]
    private Image[] images_Gauge;

    private const int HP = 0, DP = 1, SP = 2, HUNGRY = 3, THIRSTY = 4, SATISFY = 5;
    
    
    // Start is called before the first frame update
    void Start()
    {
        currenthp = hp;
        currentsp = sp;
        currentdp = dp; 
        currenthungry = hungry; 
        currentthirsty = thirsty;
        currentsatisfy = satisfy;
    
    }

    // Update is called once per frame
    void Update()
    {
        Hungry();
        Thirsty();
        GaugeUpdate();
        SPRechargeTime();
        SPRecover();
    }

    private void SPRechargeTime()
    {
        if(spUsed)
        {
            if (currentspRechargeTime < spRechargeTime)
            {
                currentspRechargeTime++;
            }
            else
            {
                spUsed = false;
            }
        }
    }


    private void SPRecover()
    {
        if (!spUsed && currentsp < sp)
        {
            currentsp += spIncreaseSpeed;
        }
    }


    private void Hungry()
    {
        if (currenthungry > 0)
        {
            if (currenthungryDecreaseTime <= hungryDecreaseTime)
            {
                currenthungryDecreaseTime++;
            }
            else
            {
                currenthungry--;
                currenthungryDecreaseTime = 0;
            }
        }
        else
            Debug.Log("배고픔 수치가 0이 되었습니다.");
    }
    private void Thirsty()
    {
        if (currentthirsty > 0)
        {
            if (currentthirstyDecreaseTime <= thirstyDecreaseTime)
            {
                currentthirstyDecreaseTime++;
            }
            else
            {
                currentthirsty--;
                currentthirstyDecreaseTime = 0;
            }
        }
        else
            Debug.Log("배고픔 수치가 0이 되었습니다.");
    }
    private void GaugeUpdate()
    {
        images_Gauge[HP].fillAmount = (float)currenthp / hp;
        images_Gauge[SP].fillAmount = (float)currentsp / sp;
        images_Gauge[DP].fillAmount = (float)currentdp / dp;
        images_Gauge[HUNGRY].fillAmount = (float)currenthungry / hungry;
        images_Gauge[THIRSTY].fillAmount = (float)currentthirsty / thirsty;
        images_Gauge[SATISFY].fillAmount = (float)currentsatisfy / satisfy;
    }

    public void IncreaseHP(int _count)
    {
        if (currenthp + _count < hp)
            currenthp += _count;
        else
            currenthp = hp;
    }
    public void DecreaseHP(int _count)
    {
        if(currentdp > 0)
        {
            DecreaseDP(_count);
            return;
        }
        currenthp -= _count;
        if (currenthp <= 0)
            Debug.Log("캐릭터의 hp가 0이 되었습니다!");
    }

    public void IncreaseDP(int _count)
    {
        if (currentdp + _count < dp)
            currentdp += _count;
        else
            currentdp = dp;
    }
    public void DecreaseDP(int _count)
    {
        currentdp -= _count;
        if (currentdp <= 0)
            Debug.Log("캐릭터의 dp가 0이 되었습니다!");
     }
    public void IncreaseHungry(int _count)
    {
        if (currenthungry + _count < hungry)
            currenthungry += _count;
        else
            currenthungry = hungry;
    }
    public void DecreaseHungry(int _count)
    {
        if (currenthungry - _count < 0)
        {
            currenthungry = 0;
        }
        else
            currenthungry -= _count;     
    }

    public void IncreaseThirsty(int _count)
    {
        if (currentthirsty + _count < thirsty)
            currentthirsty += _count;
        else
            currentthirsty = thirsty;
    }
    public void DecreaseThirsty(int _count)
    {
        if (currentthirsty - _count < 0)
        {
            currentthirsty = 0;
        }
        else
            currentthirsty -= _count;
    }

    public void DecreaseStamina(int _count)
    {
        spUsed = true;
        currentspRechargeTime = 0;
        if (currentsp - _count > 0)
            currentsp -= _count;
        else
            currentsp = 0;
    }
    public int GetCurrentSP()
    {
        return currentsp;
    }
}
