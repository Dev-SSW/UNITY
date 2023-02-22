using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{

    //충돌한 오브젝트를 저장하는 컬라이더.
    private List<Collider> colliderList = new List<Collider>();

    [SerializeField]
    private int layerGround; //지상 레이어 (충돌 무시)
    private const int IGNORE_RAYCAST_LAYER = 2;   //이그노어 레이캐스트 레이어로 설정된 오브젝트는 충돌 무시

    [SerializeField]
    private Material green;
    [SerializeField]
    private Material red;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
    }
    private void ChangeColor()
    {
        if(colliderList.Count > 0)
        {
            SetColor(red);
            //레드
        }
        else
        {
            SetColor(green);
            //그린
        }
    }

    private void SetColor(Material mat)
    {
        foreach(Transform tf_Child in this.transform)
        {
            var newMaterials = new Material[tf_Child.GetComponent<Renderer>().materials.Length];
            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = mat;
            }
            tf_Child.GetComponent<Renderer>().materials = newMaterials;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_RAYCAST_LAYER)
        {
            colliderList.Add(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_RAYCAST_LAYER)
        {
            colliderList.Remove(other);
        }
    }

    public bool isBuildable()
    {
        return colliderList.Count == 0; //0개 일때만 true 반환
    }
}
