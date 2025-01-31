public class GunPlatformModel
{
    public int RotateSpeed;
    
    public WeaponModel MiniGun1;
    public WeaponModel MiniGun2;
    public WeaponModel Laser1;
    public WeaponModel Laser2;
    public WeaponModel CsarPushka;

    public GunPlatformModel(WeaponModel miniGun1, WeaponModel miniGun2, WeaponModel laser1, WeaponModel laser2, WeaponModel csarPushka)
    {
        MiniGun1 = miniGun1;
        MiniGun2 = miniGun2;
        Laser1 = laser1;
        Laser2 = laser2;
        CsarPushka = csarPushka;
    }
}

public class WeaponModel
{
    public int Damage;
    public int RechargeSpeed;

}
