using System;

public static class UpgradeLogic
{
    public static int GetStepMultiplier(bool isIncreasable)
    {
        return isIncreasable ? 1 : -1;
    }

    public static float CalculateValue(float startValue, int level, float upgradeStep, Types type)
    {
        switch (type)
        {
            case Types.Additive:
                return CalculateValue(startValue, level, upgradeStep);
            default:
                throw new NotImplementedException(nameof(type));
        }
    }

    private static float CalculateValue(float startValue, int level, float upgradeStep)
    {
        return startValue + level * upgradeStep;
    }

    public static int CalculateValue(int startValue, int level, float upgradeStep, Types type)
    {
        switch (type)
        {
            case Types.Additive:
                return CalculateValue(startValue, level, (int)upgradeStep);
            default:
                throw new NotImplementedException(nameof(type));
        }
    }

    private static int CalculateValue(int startValue, int level, int upgradeStep)
    {
        return startValue + level * upgradeStep;
    }

    //private float CalculateNextValue()
    //{
    //    return Value + _upgradeStep * _stepMultiplier;
    //}

    public enum Types
    {
        Additive,
        Multiply
    }
}
