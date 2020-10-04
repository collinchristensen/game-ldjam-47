using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class JsonUtility {
    
    public static string Serialize(object val) {
        return JsonConvert.SerializeObject(val);
    }

    public static T Deserialize<T>(string val) {
        return JsonConvert.DeserializeObject<T>(val);
    }

}

