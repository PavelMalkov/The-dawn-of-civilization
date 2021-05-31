public class TextControl
{
    private static string[] teams = { "", "K", "M", "B", "A", "Ax", "Ac", "" };

    public static string ConvertTxt(float x)
    {

        int count = 0;
        float buf = x;
        while (buf > 1000)
        {
            count++;
            buf /= 1000;
        }
        string str1 = (buf.ToString("####0.#") + teams[count]);

        while (str1.Length < 7)
        {
            str1 = " " + str1;
        }

        return str1; // значение teams
    }
}

public class Currency
{
    public static float X, Y;
    public static float Gold = 0;
    public static float Science = 0;
    public static int BildCount = 0; // количество зданий
}



