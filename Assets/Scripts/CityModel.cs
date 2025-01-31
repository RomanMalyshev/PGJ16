using System.Collections.Generic;

public class CityModel
{
    public int TotalHealth = 100;
    public int Speed = 100;
    public int AutoHealValue = 100; //per sec

    public int Currency = 0;

    public GunPlatformModel GunPlatformModel;

    public List<Building> Buildings = new List<Building>();

    public int MinBuildings = 1;
    public int MaxBuildings = 10;

    public int CurrentHealth;

    public CityModel(int health, int speed, int autoHealValue, int currency, GunPlatformModel gunPlatformModel)
    {
        TotalHealth = CurrentHealth = health;
        Speed = speed;
        AutoHealValue = autoHealValue;
        Currency = currency;
        GunPlatformModel = gunPlatformModel;
    }
}