using System;
using System.Threading;
using System.Threading.Tasks;
using Library.API.Service.Interface;
using Microsoft.Extensions.Hosting;

namespace Library.API.Configs;

public class BookStatusService : IHostedService, IDisposable
{
    private Timer? _timer;
    private readonly IBookService _bookService;
    private readonly ILendRecordService _lendRecordService;

    public BookStatusService(IServicesWrapper serviceWrapper)
    {
        _lendRecordService = serviceWrapper.LendRecord;
        _bookService = serviceWrapper.Book;
    }


    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));

        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
        var books = await _bookService.GetByConditionAsync(book => !book.IsLend);
        foreach (var book in books)
        {
            var count = await _lendRecordService.CountByConditionAsync(record =>
                record.RealReturnTime == null && record.StartTime < DateTime.Now && record.BookId == book.Id);
            if (count != 0)
            {
                book.IsLend = true;
                await _bookService.UpdateAsync(book);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("StopAsync");

        return Task.CompletedTask;
    }


    public void Dispose()
    {
        _timer?.Dispose();
    }
}