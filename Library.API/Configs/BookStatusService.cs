using System;
using System.Threading;
using System.Threading.Tasks;
using Library.API.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Library.API.Configs;

public class BookStatusService : BackgroundService
{
    private readonly IBookService _bookService;
    private readonly ILendRecordService _lendRecordService;

    public BookStatusService(IServiceScopeFactory scopeFactory)
    {
        using var scope = scopeFactory.CreateScope();
        var servicesWrapper = scope.ServiceProvider.GetService<IServicesWrapper>();
        _lendRecordService = servicesWrapper!.LendRecord;
        _bookService = servicesWrapper!.Book;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //var books = await _bookService.GetByConditionAsync(book => !book.IsLend);
        //foreach (var book in books)
        //{
        //    var count = await _lendRecordService.CountByConditionAsync(record =>
        //        record.RealReturnTime == null && record.StartTime < DateTime.Now && record.BookId == book.Id);
        //    if (count != 0)
        //    {
        //        book.IsLend = true;
        //        await _bookService.UpdateAsync(book);
        //    }
        //}
    }
}