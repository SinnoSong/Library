using System.Collections.Generic;
using Library.Common.Models;

namespace Library.API.Configs;

public static class MenuPathResource
{
    private static readonly Dictionary<string, MenuPath> menuDatas = SetMenuDatas();

    private static Dictionary<string, MenuPath> SetMenuDatas()
    {
        var result = new Dictionary<string, MenuPath>
        {
            {"book", new MenuPath("/books", "书籍管理", "book")},
            {"category", new MenuPath("/categories", "书籍分类管理", "categories")},
            {"lendConfig", new MenuPath("/lendConfigs", "借阅规则管理", "lendConfig")},
            {"lendRecords", new MenuPath("/lendRecords", "借阅管理", "lendRecords")},
            {"dashboard", new MenuPath("/dashboard", "数据统计", "dashboard")},
            {"user_account", new MenuPath("/account/settings", "个人信息", "user_account")},
            {"notice",new MenuPath("/notice","公告管理","notice") }
        };
        var accountMenu = new List<MenuPath>
        {
            new("/account/center", "用户中心"),
            new("/account/settings", "个人信息")
        };
        result.Add("account", new MenuPath("/account", "账号管理", "account", accountMenu));
        return result;
    }

    internal static List<MenuPath> SetMenu(string role)
    {
        var result = new List<MenuPath>();
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