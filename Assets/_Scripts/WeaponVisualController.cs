using UnityEngine;

public class WeaponVisualController : MonoBehaviour
{
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
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchOnGun(revolver);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchOnGun(autoRifle);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchOnGun(shotgun);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchOnGun(sniper_rifle);
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
}
