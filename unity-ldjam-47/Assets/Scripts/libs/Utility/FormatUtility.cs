using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class FormatUtility {

    public static string ConvertStringToBase64(string val) {

        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(val);
        return Convert.ToBase64String(bytes);
    }

    public static string ConvertBase64ToString(string val) {

        byte[] bytes = Convert.FromBase64String(val);
        return System.Text.Encoding.UTF8.GetString(bytes);
    }

    // https://stackoverflow.com/questions/6309379/how-to-check-for-a-valid-base64-encoded-string
    public static bool IsBase64String(string s) {
        s = s.Trim();
        return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

    }
}