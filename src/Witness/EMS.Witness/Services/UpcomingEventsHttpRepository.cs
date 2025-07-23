using System.Linq.Expressions;
using EMS.Witness.Models;
using EMS.Witness.Models.NTS;
using EMS.Witness.Shared.Toasts;

namespace EMS.Witness.Services;


public class UpcomingEventHttpRepository : HttpRepository<UpcomingEventModel>, IUpcomingEventRepository
{
    public UpcomingEventHttpRepository(NHttpClient httpClient, IToaster toaster) : base("upcoming-event", httpClient, toaster)
    {
    }
}

public interface IUpcomingEventRepository
{
    Task<IEnumerable<UpcomingEventModel>> ReadAll();
}

public abstract class HttpRepository<T>
    where T : class, IIdentifiable
{
    readonly string _endpoint;
    private readonly IToaster _toaster;

    protected HttpRepository(string endpoint, NHttpClient client, IToaster toaster)
    {
        _endpoint = endpoint;
        _toaster = toaster;
        Client = client;
    }

    protected NHttpClient Client { get; }

    protected string BuildUrl(object id)
    {
        return $"{_endpoint}/{id}";
    }

    protected void HandleException(Exception ex)
    {
        if (ex is HttpRequestException { HttpRequestError: HttpRequestError.ConnectionError })
        {
            _toaster.Add("Could not connect", "Please check your internet connection", UiColor.Warning, 15);
        }
        else
        {
            _toaster.Add("Error", ex.Message, UiColor.Danger, 30);
        }
    }

    public async Task Create(T item)
    {
        try
        {
            await Client.Post(_endpoint, item);
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    public async Task Delete(int id)
    {
        try
        {
            var url = BuildUrl(id);
            await Client.Delete(url);
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    public async Task Delete(T item)
    {
        await Delete(item.Id);
    }

    public Task Delete(Expression<Func<T, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public Task Delete(IEnumerable<T> items)
    {
        throw new NotImplementedException();
    }

    public Task<T?> Read(Expression<Func<T, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public async Task<T?> Read(int id)
    {
        try
        {
            var url = BuildUrl(id);
            return await Client.GetJson<T>(url);
        }
        catch (Exception ex)
        {
            HandleException(ex);
            return null;
        }
    }

    public async Task<IEnumerable<T>> ReadAll()
    {
        try
        {
            return await Client.GetJson<IEnumerable<T>>(_endpoint) ?? [];
        }
        catch (Exception ex)
        {
            HandleException(ex);
            return [];
        }
    }

    public Task<IEnumerable<T>> ReadAll(Expression<Func<T, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public async Task SafeDelete(int id)
    {
        try
        {
            var url = $"{BuildUrl(id)}/safe";
            await Client.Delete(url);
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    public async Task Update(T items)
    {
        try
        {
            await Client.Patch(_endpoint, items);
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }
}
