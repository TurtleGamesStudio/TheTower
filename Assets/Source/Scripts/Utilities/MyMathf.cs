using UnityEngine;

public static class MyMathf
{
    public static int GetRank(int number)
    {
        int rank = 1;

        while (number > 9)
        {
            number /= 10;
            rank++;
        }

        return rank;
    }

    public static int MoveTowards(int value, int target, int delta)
    {
        if (value > target)
        {
            int difference = value - delta;
            return difference > target ? difference : target;
        }
        else if (value < target)
        {
            int sum = value + delta;
            return sum < target ? sum : target;
        }
        else
        {
            return target;
        }
    }

    public static int Lerp(int startValue, int endValue, float progress)
    {
        progress = Mathf.Clamp01(progress);
        int difference = endValue - startValue;
        int surplus = (int)(difference * progress);
        return startValue + surplus;
    }

    public static int Evaluate(int difference, float progress)
    {
        progress = Mathf.Clamp01(progress);
        int surplus = (int)(difference * progress);
        return surplus;
    }
}
