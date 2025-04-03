using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class GunController : MonoBehaviour
{
    //�}�K�W���T�C�YAR�i�A�T���g���C�t���jHG�i�n���h�K���j
    public int magazineSizeHG = 16;
    public int magazineSizeAR = 32;
    //�����e��
    public int currentAmmoHG = 32;
    public int currentAmmoAR = 128;
    //���˃��[�g
    public float RateHG = 0.5f;
    public float RateAR = 0.1f;
    //���킲�Ƃ̃_���[�W��
    public float damageHG = 5.0f;
    public float damageAR = 2.0f;

    //���݂̕���𒲂ׂ�
    enum Weapon { HG, AR };
    Weapon currentWeapon = Weapon.HG;

    public GameObject hG;
    public GameObject aR;

    private int countHG = 0;
    private int countAR = 0;
    
    private Animator animator;
    private RigBuilder rigBuilder;

    //���݂̕���X�e�[�^�X
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
        //HG��on,off�̐؂�ւ�
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            countHG++;

            //AR�������Ă��鎞���܂� riglayers 0 = HG,riglayers 1 = AR
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
                    currentWeapon = Weapon.HG; //���݂̕�����X�V
                    UpdateWeaponStats();
                    break;
                case 2://
                    hG.SetActive(false);
                    rigBuilder.layers[0].active = false;
                    countHG = 0;
                    break;
            }
        }
        //AR��on,off�̐؂�ւ�
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            countAR++;
            //HG�������Ă��鎞���܂�,riglayers 0 = HG,riglayers 1 = AR
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
                    currentWeapon = Weapon.AR;//���݂̕�����X�V
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
