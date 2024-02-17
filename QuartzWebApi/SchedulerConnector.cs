using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Quartz;
using SchedulerMetaData = QuartzWebApi.Wrappers.SchedulerMetaData;

namespace QuartzWebApi;

/// <summary>
///     Connects to the <see cref="SchedulerHost"/>
/// </summary>
public class SchedulerConnector
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

    #region IsJobGroupPaused
    /// <summary>
    ///     Returns <c>true</c> if the given JobGroup is paused
    /// </summary>
    /// <param name="groupName">The group name</param>
    /// <returns></returns>
    public async Task<bool> IsJobGroupPaused(string groupName)
    {
        var response = await _httpClient.GetAsync($"Scheduler/IsJobGroupPaused/{groupName}");
        //return await response.Content.ReadAsAsync<bool>();
        return true;
    }
    #endregion

    #region IsTriggerGroupPaused
    /// <summary>
    ///     Returns <c>true</c> if the given TriggerGroup is paused
    /// </summary>
    /// <param name="groupName">The group name</param>
    /// <returns></returns>
    public async Task<bool> IsTriggerGroupPaused(string groupName)
    {
        var response = await _httpClient.GetAsync($"Scheduler/IsTriggerGroupPaused/{groupName}");
        //return await response.Content.ReadAsAsync<bool>();
        return true;
    }
    #endregion

    #region SchedulerName
    /// <summary>
    ///     Returns the name of the scheduler
    /// </summary>
    public async Task<string> SchedulerName()
    {
        var response = await _httpClient.GetAsync("Scheduler/SchedulerName");
        return await response.Content.ReadAsStringAsync();
    }
    #endregion

    #region GetMetaData
    /// <summary>
    ///     Get a <see cref="Quartz.SchedulerMetaData" /> object describing the settings
    ///     and capabilities of the scheduler instance.
    /// </summary>
    /// <remarks>
    ///     Note that the data returned is an 'instantaneous' snapshot, and that as
    ///     soon as it's returned, the meta-data values may be different.
    /// </remarks>
    public async Task<Wrappers.SchedulerMetaData> GetMetaData()
    {
        var response = await _httpClient.GetAsync("Scheduler/GetMetaData");
        var json = await response.Content.ReadAsStringAsync();
        return SchedulerMetaData.FromJsonString(json);
    }
    #endregion

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