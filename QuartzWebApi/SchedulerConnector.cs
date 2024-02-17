using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QuartzWebApi;

/// <summary>
///     Connects to the <see cref="SchedulerHost"/>
/// </summary>
internal class SchedulerConnector
{
    #region Fields
    private HttpClient _httpClient;
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="schedulerHostAddress">The host address of the scheduler</param>
    public SchedulerConnector(string schedulerHostAddress)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(schedulerHostAddress);
    }

    /// <summary>
    ///     Returns <c>true</c> if the given JobGroup is paused
    /// </summary>
    /// <param name="groupName">The group name</param>
    /// <returns></returns>
    public async Task<bool> IsJobGroupPaused(string groupName)
    {
        var response = await _httpClient.GetAsync($"Scheduler/IsJobGroupPaused/{groupName}");
        return await response.Content.ReadAsAsync<bool>();
    }

    /// <summary>
    ///     Returns <c>true</c> if the given TriggerGroup is paused
    /// </summary>
    /// <param name="groupName">The group name</param>
    /// <returns></returns>
    public async Task<bool> IsTriggerGroupPaused(string groupName)
    {
        var response = await _httpClient.GetAsync($"Scheduler/IsTriggerGroupPaused/{groupName}");
        return await response.Content.ReadAsAsync<bool>();
    }

    /// <summary>
    ///     Returns the name of the scheduler
    /// </summary>
    public async Task<string> SchedulerName()
    {
        var response = await _httpClient.GetAsync("Scheduler/SchedulerName");
        return await response.Content.ReadAsStringAsync();
    }

    // ... Continue this pattern for all other HttpGet methods in SchedulerController ...

    public async Task Start()
    {
        var response = await _httpClient.PostAsync("Scheduler/Start", null);
        // Handle response if necessary
    }

    public async Task StartDelayed(int delay)
    {
        var response = await _httpClient.PostAsync($"Scheduler/StartDelayed/{delay}", null);
        // Handle response if necessary
    }
}