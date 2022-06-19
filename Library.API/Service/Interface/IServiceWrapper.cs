namespace Library.API.Service.Interface
{
    public interface IServicesWrapper
    {
        IBookService Book { get; }
        ICategoryService Category { get; }
        ILendConfigService LendConfig { get; }
        ILendRecordService LendRecord { get; }
        INoticeService Notice { get; }
    }
}