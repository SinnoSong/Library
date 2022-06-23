namespace Library.Web.Constants;

public static class Apis
{
    #region auth

    public const string AuthLogin = "api/auth/login";
    public const string AuthRegister = "api/auth/register";

    #endregion

    #region book

    public const string CreateBook = "api/books";
    public const string DeleteOrUpdateOrGetBook = "api/books/";

    #endregion

    #region Category

    public const string CreateCategory = "api/Category";
    public const string DeleteOrUpdateOrGetCategory = "api/books/";

    #endregion

    #region lendConfig

    public const string CreateLendConfig = "api/LendConfig";
    public const string DeleteOrUpdateOrGetLendConfig = "api/LendConfig/";

    #endregion

    #region lendRecord

    public const string CreateLendRecord = "api/LendRecord";
    public const string DeleteOrUpdateOrGetLendRecord = "api/LendRecord/";

    #endregion

    #region notice

    public const string CreateNotice = "api/Notice";
    public const string DeleteOrUpdateOrGetNotice = "api/Notice/";

    #endregion
}