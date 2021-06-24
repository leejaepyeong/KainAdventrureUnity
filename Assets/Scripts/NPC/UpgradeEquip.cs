using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 업그레이드 관리
public class UpgradeEquip : MonoBehaviour
{
    public Equipment equipment;    // 장비 중인 아이템

    public ItemUpgradeDB itemUpgradeDB; // 업그레이드 목록 데이터베이스

    public GameObject tryAnim;  // 강화 중 애니메이션 오브젝트

    public Image[] equipImage; //아이템 이미지
    public Text equipTxt;   // 장비템 이름
    
    public Image[] materialImage;  //재료 이미지
    public Text materialCountTxt;   //재료 갯수

    public Item upgradeItem;    // 업그레이드 대상인 아이템
    public GameObject succesPannel; // 성공 여부
    public Text successTxt; // 성공 여부  텍스트

    private void Update()
    {
        ShowEquipItem();
    }

    //장착 중인 장비 이미지 보여주기
    void ShowEquipItem()
    {
        // 0~2 장비템 갯수
        for (int i = 0; i < equipment.equipItem.Length; i++)
        {
            equipImage[i].sprite = equipment.equipItem[i].itemImage;
        }
    }

    // 강화 아이템 목록 바꾸기
    void ChangeUpgradeItem(Item _item)
    {
        upgradeItem = _item;
    }

    // 강화 시도
    void TryUpgrade()
    {
        StartCoroutine(DoUpgrade());
    }

    // 강화중
    IEnumerator DoUpgrade()
    {
        int random = Random.Range(0,(upgradeItem.Upgrade + 1) * 2); // 1/2, 1/4, 1/8

        tryAnim.SetActive(true);

        successTxt.text = "- 강화 시도 -";
        yield return new WaitForSeconds(0.3f);
        succesPannel.SetActive(true);

     

        yield return new WaitForSeconds(3f);
        tryAnim.SetActive(false);
        succesPannel.SetActive(false);

        if (random == 1)
        {
            upgradeItem.Upgrade++;

            successTxt.text = "- 강화 성공 -";
        }
        else
        {
            successTxt.text = "- 강화 실패 -";
        }

        yield return new WaitForSeconds(0.2f);
        succesPannel.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        succesPannel.SetActive(false);
        
    }


}
