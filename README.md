# QuartzWebApi
A REST api for Quartz.net

How to use
==========

### Check if a job group is paused

Do a `POST` request to `scheduler/isjobgrouppaused/{groupName}` , this will return `true` or `false`

### Check if a trigger group is paused

Do a `POST` request to `scheduler/istriggergrouppaused/{groupName}` , this will return `true` or `false`

### Get the schedulers name

Do a `GET` request to `scheduler/schedulername`

### Get the schedulers instance id

Do a `GET` request to `scheduler/schedulerinstanceid`

### Check if a scheduler is in standby mode

Do a `GET` request to `scheduler/instandbymode` , this will return `true` or `false`

### Check if a scheduler is in shutdown

Do a `GET` request to `scheduler/isshutdown` , this will return `true` or `false`

### Get the schedulers meta-data

Do a `GET` request to `scheduler/getmetadata` , this will return something simular to this

```json
{
	"inStandbyMode": false,
	"jobStoreType": "Quartz.Simpl.RAMJobStore, Quartz, Version=3.4.0.0, Culture=neutral, PublicKeyToken=f6b8c98a402cc8a4",
	"jobStoreClustered": false,
	"jobsStoreSupportsPersistence": false,
	"numbersOfJobsExecuted": 0,
	"runningSince": "2022-05-11T16:30:06.5957565+00:00",
	"schedulerInstanceId": "NON_CLUSTERED",
	"schedulerName": "Boven",
	"schedulerRemote": false,
	"schedulerType": "Quartz.Impl.StdScheduler, Quartz, Version=3.4.0.0, Culture=neutral, PublicKeyToken=f6b8c98a402cc8a4",
	"shutdown": false,
	"started": true,
	"threadPoolSize": 10,
	"threadPoolType": "Quartz.Simpl.DefaultThreadPool, Quartz, Version=3.4.0.0, Culture=neutral, PublicKeyToken=f6b8c98a402cc8a4",
	"version": "3.4.0.0"
}
```
