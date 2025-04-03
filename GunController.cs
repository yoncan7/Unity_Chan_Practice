using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class GunController : MonoBehaviour
{
    //マガジンサイズAR（アサルトライフル）HG（ハンドガン）
    public int magazineSizeHG = 16;
    public int magazineSizeAR = 32;
    //所持弾数
    public int currentAmmoHG = 32;
    public int currentAmmoAR = 128;
    //発射レート
    public float RateHG = 0.5f;
    public float RateAR = 0.1f;
    //武器ごとのダメージ量
    public float damageHG = 5.0f;
    public float damageAR = 2.0f;

    //現在の武器を調べる
    enum Weapon { HG, AR };
    Weapon currentWeapon = Weapon.HG;

    public GameObject hG;
    public GameObject aR;

    private int countHG = 0;
    private int countAR = 0;
    
    private Animator animator;
    private RigBuilder rigBuilder;

    //現在の武器ステータス
    private int currentMagazineSize;
    private int currentAmmo;
    private float currentRate;
    private float currentDamage;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigBuilder = GetComponent<RigBuilder>();
    }
    void Update()
    {
        //HGのon,offの切り替え
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            countHG++;

            //ARを持っている時しまう riglayers 0 = HG,riglayers 1 = AR
            if(countAR > 0)
            {
                rigBuilder.layers[1].active = false;
                aR.SetActive(false);
                countAR = 0;
            }

            switch (countHG)
            {
                case 1:
                    hG.SetActive(true);
                    rigBuilder.layers[0].active = true;
                    currentWeapon = Weapon.HG; //現在の武器を更新
                    UpdateWeaponStats();
                    break;
                case 2://
                    hG.SetActive(false);
                    rigBuilder.layers[0].active = false;
                    countHG = 0;
                    break;
            }
        }
        //ARのon,offの切り替え
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            countAR++;
            //HGを持っている時しまう,riglayers 0 = HG,riglayers 1 = AR
            if (countHG > 0)
            {
                rigBuilder.layers[0].active = false;
                hG.SetActive(false);
                countHG = 0;
            }

            switch (countAR)
            {
                case 1:
                    aR.SetActive(true);
                    rigBuilder.layers[1].active = true;
                    currentWeapon = Weapon.AR;//現在の武器を更新
                    UpdateWeaponStats();
                    break;
                case 2:
                    aR.SetActive(false);
                    rigBuilder.layers[1].active = false;
                    countAR = 0;
                    break;
            }
        }
    }

    void UpdateWeaponStats()
    {
        switch (currentWeapon)
        {
            case Weapon.HG:
                currentMagazineSize = magazineSizeHG;
                currentAmmo = currentAmmoHG;
                currentRate = RateHG;
                currentDamage = damageHG;
                break;
            case Weapon.AR:
                currentMagazineSize = magazineSizeAR;
                currentAmmo = currentAmmoAR;
                currentRate = RateAR;
                currentDamage = damageAR;
                break;
        }
    }
}
