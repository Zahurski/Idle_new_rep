using System.Globalization;
using UnityEngine;

public static class FormatNums
{
    private static readonly string[] Names = new[]
    {
        "",
        "K",
        "M",
        "B",
        "T",
        "q",
        "Q",
        "s",
        "S",
        "o",
        "O"
    };

    public static string FormatNum(float num)
    {
        if (num == 0) return "0";

        num = Mathf.Round(num);

        int i = 0;
        while (i + 1 < Names.Length && num >= 1000f)
        {
            num /= 1000f;
            i++;
        }

        return num.ToString("#.#") + Names[i];
    }
    
}
