using UnityEngine;

public class WeaponVisualController : MonoBehaviour
{
    private Animator animator;

    const string COMMON_WEAPON_LAYER = "Common Weapon Layer";
    const string SHOTGUN_LAYER = "Shotgun Weapon Layer";
    const string SNIPER_RIFLE_LAYER = "Rifle Weapon Layer";

    [SerializeField] private Transform[] gunTransform;

    [SerializeField] private Transform pistol;
    [SerializeField] private Transform revolver;
    [SerializeField] private Transform autoRifle;
    [SerializeField] private Transform shotgun;
    [SerializeField] private Transform sniper_rifle;

    [SerializeField] private Transform leftHand;

    [SerializeField] private Transform currentGun;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInParent<Animator>();

        SwitchOnGun(pistol);
    }

    // Update is called once per frame
    void Update()
    {
        SwitchGunVisual();
    }

    private void SwitchGunVisual()
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
