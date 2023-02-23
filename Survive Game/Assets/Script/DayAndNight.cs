using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    [SerializeField] private float secondPerRealTimeSecound; //���Ӽ����� 100�� = ������ 1��
    

    [SerializeField] private float fogDensityCalc; //������ ����

    [SerializeField] private float nightFogDensity; //�� ������ fog �е�

    private float dayFogDensity; //�� ������ fog �е�
    private float currentFogDensity;
    // Start is called before the first frame update
    void Start()
    {
        dayFogDensity = RenderSettings.fogDensity;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecound * Time.fixedDeltaTime);
        if (transform.eulerAngles.x >= 170)
        {
            GameManager.isNight = true;
        }
        else if (transform.eulerAngles.x <= 10)
        {
            GameManager.isNight = false;
        }

        if (GameManager.isNight)
        {
            if (currentFogDensity <= nightFogDensity)
            {
                currentFogDensity += 0.1f * fogDensityCalc * Time.fixedDeltaTime;
                RenderSettings.fogDensity = currentFogDensity;
            }
        }
        else
        {
            if (currentFogDensity <= nightFogDensity)
            {
                currentFogDensity -= 0.1f * fogDensityCalc * Time.fixedDeltaTime;
                RenderSettings.fogDensity = currentFogDensity;
            }
        }
    }
}
