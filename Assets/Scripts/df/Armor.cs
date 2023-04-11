namespace PigCastleDefence
{
    public static class Armor
    {
        public static float CalculateDamageAfterArmor(float damage, float armor)
        {
            float damageReduction = armor / (armor + 100f);
            float actualDamage = damage * (1f - damageReduction);
            return actualDamage;
        }
    }
}