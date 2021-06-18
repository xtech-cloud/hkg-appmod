using System.Text.Json;

namespace hkg.builder
{
    public class ViewUtility
    {
        public static string[] FieldStringToAry(string _value)
        {
            if (string.IsNullOrEmpty(_value))
                return new string[0];

            string value = _value;
            if (value.StartsWith("["))
                value = value.Remove(0, 1);
            if (value.EndsWith("]"))
                value = value.Remove(value.Length-1, 1);
            return value.Split(',');
        }

        public static string DocumentToHtml(string _json, System.Action<System.Exception> _onException)
        {
            string textAry = "";
            string imageAry = "";
            try
            {
                var jsonDoc = JsonDocument.Parse(_json);
                foreach(var obj in jsonDoc.RootElement.EnumerateObject())
                {
                    string key = obj.Name;
                    var value = obj.Value;
                    if (key.Equals("_images_"))
                    {
                        imageAry = parseImages(value);
                        continue;
                    }
                    if (JsonValueKind.String == value.ValueKind)
                    {
                        textAry += string.Format("<tr><th>{0}</th><td>{1}</td></tr>", key, value.GetString());
                    }
                    else if (JsonValueKind.Array == value.ValueKind)
                    {
                        int index = 0;
                        foreach (var obj2 in value.EnumerateArray())
                        {
                            if (JsonValueKind.String == obj2.ValueKind)
                            {
                                if(index == 0)
                                    textAry += string.Format("<tr><th rowspan=\"{0}\">{1}</th><td>{2}</td></tr>",value.GetArrayLength(), key, obj2.GetString());
                                else
                                    textAry += string.Format("<tr><td>{0}</td></tr>", obj2.GetString());
                                index += 1;
                            }
                        }
                    }
                }
            }
            catch (System.Exception _ex)
            {
                _onException(_ex);
            }
            return table.Replace("{{textAry}}", textAry).Replace("{{imageAry}}", imageAry);
        }

        private static string parseImages(JsonElement _e)
        {
            string imageAry = "";
            if (JsonValueKind.Array == _e.ValueKind)
            {
                foreach (var obj in _e.EnumerateArray())
                {
                    if (JsonValueKind.Object == obj.ValueKind)
                    {
                        JsonElement src;
                        if(obj.TryGetProperty("src", out src))
                        {
                            if(JsonValueKind.String == src.ValueKind)
                            {
                                JsonElement title;
                                if (obj.TryGetProperty("title", out title))
                                {
                                    if (JsonValueKind.String == title.ValueKind)
                                    {
                                        imageAry += string.Format("<tr><th></th><td><img src=\"{0}\"></img><p>{1}</p></td></tr>",src.GetString(), title.GetString());
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return imageAry;
        }

        private static string table = @"
<html>
<head>
<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
<style>
    .table table {
        width:100%;
        margin:15px 0;
        border:0;
    }
    .table th {
        background-color:#96C7ED;
        color:#000000;
    }
    .table,.table th,.table td {
        font-size:0.95em;
        text-align:left;
        padding:10px 20px;
        border-collapse:collapse;
    }
    .table th,.table td {
        border: 1px solid #73b4e7;
        border-width:1px 0 1px 0;
        border:2px inset #ffffff;
    }
    .table tr {
        border: 1px solid #ffffff;
        vertical-align: top;
    }
    .table tr:nth-child(odd){
        background-color:#dcecf9;
    }
    .table tr:nth-child(even){
        background-color:#ffffff;
    }
</style>
</head>
<body>
<table class=table>
    {{textAry}}
    {{imageAry}}
</table>
</body>
</html>
";
    }
}
