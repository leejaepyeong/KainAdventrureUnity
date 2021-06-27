using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 업그레이드 관리
public class UpgradeEquip : MonoBehaviour
{
    public Equipment equipment;
    public ItemUpgradeDB itemUpgradeDB; // 업그레이드 목록 데이터베이스
    public ItemDB itemDB;
    public ItemDB equipDB;
    public InventoryData inventoryData; //  아이템 인벤토리

    bool canUpgrade;
    bool isUpgrade;     // 강화 시도중

    public GameObject tryAnim;  // 강화 중 애니메이션 오브젝트
    public GameObject staticAnim;   // 평상 시 오브젝트

    public GameObject[] materials;  // 재료 오브젝트
    public Image[] materialImage;  //재료 이미지
    public Text[] materialCountTxt;   //재료 갯수

    public Item upgradeItem;    // 업그레이드 대상인 아이템
    public Image upgradeItemImg;    //업그레이드 아이템 이미지
    public Text upgradeTxt; // 업그레이드 정도

    public GameObject succesPannel; // 성공 여부
    public Text successTxt; // 성공 여부  텍스트

    int itemNum;    // 강화 아이템 아이디


    private void Start()
    {
        UpgradeItem(equipment.equipItem[1]);
    }


    // 업그레이드 아이템 정보 가져오기
    public void UpgradeItem(Item _item)
    {
        upgradeItem = _item;
        upgradeItemImg.sprite = _item.itemImage;
        upgradeTxt.text = "+ " + upgradeItem.Upgrade.ToString();

        itemNum = equipDB.GetIDFrom(upgradeItem);

        NeedMaterial();
    }

    // 필요 재료 개수
    void NeedMaterial()
    {
        for (int i = 0; i < materialImage.Length; i++)
        {
            materials[i].SetActive(false);
        }


        for (int i = 0; i < itemUpgradeDB.itemUpgradeDatas[itemNum].MaterialItems.Length; i++)
        {
            materials[i].SetActive(true);

            int itemID = itemDB.GetIDFrom(itemUpgradeDB.itemUpgradeDatas[itemNum].MaterialItems[i]);

            for (int j = 0; j < inventoryData.itemIDs.Length; j++)
            {
                materialImage[i].sprite = itemUpgradeDB.itemUpgradeDatas[itemNum].MaterialItems[i].itemImage;
                if (itemID == inventoryData.itemIDs[j])
                {
                    
                    materialCountTxt[i].text = inventoryData.itemCount[j] + " / " + itemUpgradeDB.itemUpgradeDatas[itemNum].MaterialItemCount[i];
                    break;
                }
                else
                {
                    materialCountTxt[i].text = 0 + " / " + itemUpgradeDB.itemUpgradeDatas[itemNum].MaterialItemCount[i];

                }
            }
        }
    }

    // 강화 시도
    public void TryUpgrade()
    {
        canUpgrade = itemUpgradeDB.itemUpgradeDatas[itemNum].isEnoughItem();

       if (canUpgrade && !isUpgrade)
        {
            isUpgrade = true;
            itemUpgradeDB.itemUpgradeDatas[itemNum].UseMaterial();

            UpgradeItem(upgradeItem);
            StartCoroutine(DoUpgrade());
        }
        
    }


    // 강화중
    IEnumerator DoUpgrade()
    {
        GameManager.instance.playerStop = true;

        int random = Random.Range(0,(upgradeItem.Upgrade + 1) * 2); // 1/2, 1/4, 1/8

        tryAnim.SetActive(true);
        staticAnim.SetActive(false);

        successTxt.text = "- 강화 시도 -";
        yield return new WaitForSeconds(0.3f);
        succesPannel.SetActive(true);

     

        yield return new WaitForSeconds(3f);
        tryAnim.SetActive(false);
        staticAnim.SetActive(true);
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

        isUpgrade = false;

        succesPannel.SetActive(false);

        GameManager.instance.playerStop = false;


    }


}
