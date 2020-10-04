using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ObjectExtensions {

    public static string ToJSON(this object val) {

        return JsonUtility.Serialize(val);
    }

    public static T FromJSON<T>(this string val) {

        return JsonUtility.Deserialize<T>(val);

    }

    public static string ToBase64(this string val) {

        return FormatUtility.ConvertStringToBase64(val);
    }

    public static string FromBase64(this string val) {

        return FormatUtility.ConvertBase64ToString(val);

    }

}