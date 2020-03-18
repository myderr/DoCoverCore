using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DoCover.Common
{
    public static class Extension
    {
        #region Enum拓展
        /// <summary>
        /// 获取枚举的中文描述
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>没有描述返回空字符串</returns>
        public static string GetDescription(this System.Enum obj)
        {
            string objName = obj.ToString();
            Type t = obj.GetType();
            System.Reflection.FieldInfo fi = t.GetField(objName);
            System.ComponentModel.DescriptionAttribute[] arrDesc = (System.ComponentModel.DescriptionAttribute[])fi.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (arrDesc.Length > 0)
                return arrDesc[0].Description;
            return "";
        }
        #endregion

        /// <summary>
        /// 设置appsetting值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="node">节点</param>
        /// <param name="appSettingsJsonFilePath">路径。默认为appsettings.json</param>
        /// <returns>是否设置成功</returns>
        public static bool SetAppSettingValue<T>(this T node,string appSettingsJsonFilePath = null)
        {
            if (node == null || typeof(T) == typeof(string)) { return false; }
            try
            {
                var nodeObj = JObject.FromObject(node);
                if (appSettingsJsonFilePath == null)
                {
                    appSettingsJsonFilePath = System.IO.Path.Combine(System.AppContext.BaseDirectory, "appsettings.json");
                }

                var json = System.IO.File.ReadAllText(appSettingsJsonFilePath);
                dynamic jsonObj = JsonConvert.DeserializeObject<JObject>(json);
                jsonObj[typeof(T).Name] = nodeObj;

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);

                System.IO.File.WriteAllText(appSettingsJsonFilePath, output);

                return true;
            }
            catch { return false; }

        }
    }
}
