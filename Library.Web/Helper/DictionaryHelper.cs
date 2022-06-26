namespace Library.Web.Helper;

public static class DictionaryHelper
{
    public static Dictionary<string, object> ToDictionary(this object data)
    {
        return data.GetType().GetProperties() //这一步获取匿名类的公共属性，返回一个数组
            .Where(q => q.GetValue(data) != null)
            .ToDictionary(q => q.Name, q => q.GetValue(data)); //这一步将数组转换为字典
    }
}