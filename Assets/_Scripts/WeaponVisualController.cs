using UnityEngine;

public class WeaponVisualController : MonoBehaviour
{
    [SerializeField] private Transform[] gunTransform;

    [SerializeField] private Transform pistol;
    [SerializeField] private Transform revolver;
    [SerializeField] private Transform autoRifle;
    [SerializeField] private Transform shotgun;
    [SerializeField] private Transform rifle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            SwitchOnGun(rifle);
        }
    }

    private void SwitchOnGun(Transform gun)
    {
        SwitchOffGuns();
        gun.gameObject.SetActive(true);
    }
    private void SwitchOffGuns()
    {
        foreach (Transform gun in gunTransform)
        {
            gun.gameObject.SetActive(false);
        }
    }
}
