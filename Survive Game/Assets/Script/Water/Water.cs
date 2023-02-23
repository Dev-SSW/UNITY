using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Water : MonoBehaviour
{
   

    [SerializeField] private float waterDrag;
    private float originDrag;

    [SerializeField] private Color waterColor;
    [SerializeField] private float waterFogDensity;

   
    [SerializeField] private Color waterNightColor;
    [SerializeField] private float waterNightFogDensity;


    private Color originColor;
    private float originFogDensity;

    [SerializeField]
    private Color originNightColor;
    [SerializeField]
    private float originNightFogDensity;

    [SerializeField] private string sound_WaterOut;
    [SerializeField] private string sound_WaterIn;
    [SerializeField] private string sound_Breathe;

    [SerializeField] private float breatheTime;
    private float currentBreatheTime;

    [SerializeField] private float totalOxygen;
    private float currentOxygen;
    private float currentTemp;

    [SerializeField] private GameObject go_BaseUI;
    [SerializeField] private Text text_totalOxygen;
    [SerializeField] private Text text_currentOxygen;
    [SerializeField] private Image Image_Gauge;

    private StatusController thePlayerStat;
    // Start is called before the first frame update
    void Start()
    {
        originColor = RenderSettings.fogColor;
        originFogDensity = RenderSettings.fogDensity;

        originDrag = 0;
        thePlayerStat = FindObjectOfType<StatusController>();
        currentOxygen = totalOxygen;
        text_totalOxygen.text = totalOxygen.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isWater)
        {
            currentBreatheTime += Time.fixedDeltaTime;
            if(currentBreatheTime >= breatheTime)
            {
                SoundManager.instance.PlaySE(sound_Breathe);
                currentBreatheTime = 0;
            }
            

        }
        DecreaseOxygen();
    }
    private void DecreaseOxygen()
    {
        if (GameManager.isWater)
        {
            currentOxygen -= Time.fixedDeltaTime;
            text_currentOxygen.text = Mathf.RoundToInt(currentOxygen).ToString();
            Image_Gauge.fillAmount = currentOxygen / totalOxygen;

            if(currentOxygen <= 0)
            {
                currentTemp += Time.fixedDeltaTime;
                if (currentTemp >= 1)
                {
                    thePlayerStat.DecreaseHP(1);
                    currentTemp = 0;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            GetWater(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            GetOutWater(other);
        }
    }

    private void GetWater(Collider _player)
    {
        go_BaseUI.SetActive(true);
        SoundManager.instance.PlaySE(sound_WaterIn);
        GameManager.isWater = true;
        _player.transform.GetComponent<Rigidbody>().drag = waterDrag;
        if (!GameManager.isNight)
        {
            RenderSettings.fogColor = waterColor;
            RenderSettings.fogDensity = waterFogDensity;
        }
        else
        {
            RenderSettings.fogColor = waterNightColor;
            RenderSettings.fogDensity = waterNightFogDensity;
        }
    }

    private void GetOutWater(Collider _player)
    {
        if (GameManager.isWater)
        {
            go_BaseUI.SetActive(false);
            currentOxygen = totalOxygen;
            SoundManager.instance.PlaySE(sound_WaterOut);
            GameManager.isWater = false;
            _player.transform.GetComponent<Rigidbody>().drag = originDrag;
            if (!GameManager.isNight)
            {
                RenderSettings.fogColor = originColor;
                RenderSettings.fogDensity = originFogDensity;
            }
            else
            {
                RenderSettings.fogColor = originNightColor;
                RenderSettings.fogDensity = originNightFogDensity;
            }
        }
    }
}
