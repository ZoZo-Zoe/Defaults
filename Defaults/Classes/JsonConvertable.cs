using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Defaults.Classes;

public class JsonConvertable<T>
{
    public static string Serialize(T @object) => JsonSerializer.Serialize(@object);

    public static T? Deserialize(string json) => JsonSerializer.Deserialize<T>(json);
}