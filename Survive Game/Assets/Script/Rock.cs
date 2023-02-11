using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private int hp;   //바위 체력

    [SerializeField]
    private float destroyTime; //파편 제거 시간

    [SerializeField]
    private SphereCollider col; //구체 콜라이더 비활성화

    //필요한 게임 오브젝트
    [SerializeField]
    private GameObject go_rock; //일반 바위
    [SerializeField]
    private GameObject go_debris; //깨진 바위
    [SerializeField]
    private GameObject go_effect_prefabs;

    [SerializeField] 
    private GameObject go_rock_item_prefab; //돌멩이 아이템

    [SerializeField]  // 나오는 돌맹이 개수
    private int count;

    [SerializeField]
    private string strike_sound;
    [SerializeField]
    private string destroy_sound;

    public void Mining()
    {
        SoundManager.instance.PlaySE(strike_sound);
        var clone = Instantiate(go_effect_prefabs, col.bounds.center, Quaternion.identity);
        Destroy(clone,destroyTime);

        hp--;
        if(hp <= 0)
        {
            Destruction();
        }
    }
    private void Destruction()
    {
        SoundManager.instance.PlaySE(destroy_sound);
        col.enabled = false;
        for (int i = 0; i <= count; i++)
        {
            Instantiate(go_rock_item_prefab, go_rock.transform.position, Quaternion.identity);
        }
        Destroy(go_rock);
        
         
        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);
    }
}
