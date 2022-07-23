using System.Collections.Generic;

namespace Library.API.Configs;

public static class MenuPath
{
    private static readonly Dictionary<string, Common.Models.MenuPath> menuDatas = SetMenuDatas();

    private static Dictionary<string, Common.Models.MenuPath> SetMenuDatas()
    {
        var result = new Dictionary<string, Common.Models.MenuPath>
        {
            {"book", new Common.Models.MenuPath("/books", "书籍管理", "book")},
            {"category", new Common.Models.MenuPath("/categories", "书籍分类管理", "book")},
            {"lendConfig", new Common.Models.MenuPath("/lendConfigs", "借阅规则管理", "lendConfig")},
            {"lendRecords", new Common.Models.MenuPath("/lendRecords", "借阅管理", "lendRecords")},
            {"dashboard", new Common.Models.MenuPath("/dashboard", "数据统计", "dashboard")},
            {"user_account", new Common.Models.MenuPath("/account/settings", "个人信息", "user_account")}
        };
        var accountMenu = new List<Common.Models.MenuPath>
        {
            new("/account/center", "用户中心"),
            new("/account/settings", "个人信息")
        };
        result.Add("account", new Common.Models.MenuPath("/account", "账号管理", "account", accountMenu));
        return result;
    }

    internal static List<Common.Models.MenuPath> SetMenu(string role)
    {
        var result = new List<Common.Models.MenuPath>();
        if (role != "User")
        {
            result.Add(menuDatas.GetValueOrDefault("book")!);
            result.Add(menuDatas.GetValueOrDefault("category")!);
            result.Add(menuDatas.GetValueOrDefault("lendConfig")!);
            result.Add(menuDatas.GetValueOrDefault("dashboard")!);
            result.Add(menuDatas.GetValueOrDefault("account")!);
        }
        else
        {
            result.Add(menuDatas.GetValueOrDefault("user_account")!);
        }

        result.Add(menuDatas.GetValueOrDefault("lendRecords")!);
        return result;
    }
}