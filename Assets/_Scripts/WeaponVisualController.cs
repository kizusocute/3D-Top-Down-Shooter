using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponVisualController : MonoBehaviour
{
    private Animator animator;
    private Rig rig;

    const string COMMON_WEAPON_LAYER = "Common Weapon Layer";
    const string SHOTGUN_LAYER = "Shotgun Weapon Layer";
    const string SNIPER_RIFLE_LAYER = "Rifle Weapon Layer";
    const string STR_RELOADING_TRIGGER = "Reload";

    [SerializeField] private Transform[] gunTransform;

    [SerializeField] private Transform pistol;
    [SerializeField] private Transform revolver;
    [SerializeField] private Transform autoRifle;
    [SerializeField] private Transform shotgun;
    [SerializeField] private Transform sniper_rifle;

    [SerializeField] private Transform leftHand;

    [SerializeField] private Transform currentGun;

    [Header("Rig")]

    [SerializeField] private bool rigShouldBeIncreased = false;
    [SerializeField] private float rigIncreaseSpeed = 2.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rig = GetComponentInChildren<Rig>();

        SwitchOnGun(pistol);
    }

    // Update is called once per frame
    void Update()
    {
        CheckWeaponSwitch();
        if(Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger(STR_RELOADING_TRIGGER);
            rig.weight = 0.15f;
        }
        if (rigShouldBeIncreased)
        {
            IncreaseRigWeight();
        }
    }

    private void IncreaseRigWeight()
    {
        rig.weight += rigIncreaseSpeed * Time.deltaTime;
        if (rig.weight >= 1f)
        {
            rig.weight = 1f;
            rigShouldBeIncreased = false;
        }
    }

    public void StartReturnRigWeightToOne()
    {
        rigShouldBeIncreased = true;
    }

    private void CheckWeaponSwitch()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchOnGun(pistol);
            SwitchAnimationLayer(animator.GetLayerIndex(COMMON_WEAPON_LAYER));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchOnGun(revolver);
            SwitchAnimationLayer(animator.GetLayerIndex(COMMON_WEAPON_LAYER));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchOnGun(autoRifle);
            SwitchAnimationLayer(animator.GetLayerIndex(COMMON_WEAPON_LAYER));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchOnGun(shotgun);
            SwitchAnimationLayer(animator.GetLayerIndex(SHOTGUN_LAYER));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchOnGun(sniper_rifle);
            SwitchAnimationLayer(animator.GetLayerIndex(SNIPER_RIFLE_LAYER));
        }
    }

    private void SwitchOnGun(Transform gun)
    {
        SwitchOffGuns();
        gun.gameObject.SetActive(true);
        currentGun = gun;

        AttachLeftHand();
    }
    private void SwitchOffGuns()
    {
        foreach (Transform gun in gunTransform)
        {
            gun.gameObject.SetActive(false);
        }
    }

    private void AttachLeftHand()
    {
        Transform targetTransform = currentGun.GetComponentInChildren<LeftHandTargetTransform>().transform;
        leftHand.SetLocalPositionAndRotation(targetTransform.localPosition, targetTransform.localRotation);
    }

    private void SwitchAnimationLayer(int layerIndex)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }
        animator.SetLayerWeight(layerIndex, 1);
    }
}
