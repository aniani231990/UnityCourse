using System.IO.Pipelines;
using System.Security.Cryptography.X509Certificates;

class DamageSystem
{
    public delegate int DamageModifier (int damage);

    public DamageModifier damageModifier = null;

    public int ApplyCritical(int dmg)
    {
        return dmg * 2;
    }

    public int ApplyArmor(int dmg)
    {
        return (int)(dmg * 0.7);
    }

    public int ApplyBuff(int dmg)
    {
        return dmg + 50;
    }

    public void Subscribe(DamageModifier func)
    {
        damageModifier += func;
    }

    public void Unsubscribe(DamageModifier func)
    {
        damageModifier += func;
    }

    public int Publish(int dmg)
    {
        return damageModifier?.Invoke(dmg);
    }

    public void ApplyAllDamage(int base_dmg)
    {

        Subscribe(ApplyCritical);
        Subscribe(ApplyArmor);
        Subscribe(ApplyBuff);

        int result = base_dmg;
        List<int> results = new List<int>(result);

        results.Add(result);

        foreach (DamageModifier handler in damageModifier.GetInvocationList())
        {
            result = handler(result);
            results.Add(result);
        }

        return results;
    }


}

class Player
{
    public DamageSystem damageSystem = new DamageSystem();

    public void GetFinalDamage(){
        List<int> results =  damageSystem.ApplyAllDamage(100);
        
    }

}


