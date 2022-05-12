# QuartzWebApi
A REST api for Quartz.net

How to use
==========

### Check if a job group is paused

Do a `POST` request to `scheduler/isjobgrouppaused/{groupName}`, where `{groupName}` is the name of the group this will return `true` or `false`

### Check if a trigger group is paused

Do a `POST` request to `scheduler/istriggergrouppaused/{groupName}`, where `{groupName}` is the name of the group this will return `true` or `false`

### Get the schedulers name

Do a `GET` request to `scheduler/schedulername`

### Get the schedulers instance id

Do a `GET` request to `scheduler/schedulerinstanceid`

### Check if a scheduler is in standby mode

Do a `GET` request to `scheduler/instandbymode` , this will return `true` or `false`

### Check if a scheduler is in shutdown

Do a `GET` request to `scheduler/isshutdown` , this will return `true` or `false`

### Get the schedulers meta-data

Do a `GET` request to `scheduler/getmetadata` , this will return

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

### Get the job group names

Do a `GET` request to `scheduler/getjobgroupnames` , this will return

```json
[
    "JobGroup1",
    "JobGroup2"
]
```

### Get the trigger group names

Do a `GET` request to `scheduler/gettriggergroupnames` , this will return

```json
[
    "TriggerGroup1",
    "TriggerGroup2"
]
```

### Get the paused trigger groups

Do a `GET` request to `scheduler/getpausedtriggergroups` , this will return

```json
[
    "PausedTriggerGroup1",
    "PausedTriggerGroup2"
]
```

### Start the scheduler

Do a `POST` request to `scheduler/start`

### Start the scheduler with a delay

Do a `POST` request to `scheduler/startdelayed/{delay}` where `{delay}` is the amount of *seconds* you want to delay the start

### Check if the scheduler is started

Do a `GET` request to `scheduler/isstarted`, this will return `true` or `false`

### Check if the scheduler is in standby mode

Do a `GET` request to `scheduler/standby`, this will return `true` or `false`

### Shutdown the scheduler

Do a `POST` request to `scheduler/shutdown`

### Shutdown the scheduler but wait for all jobs to complete

Do a `POST` request to `scheduler/shutdown/{waitForJobsToComplete}` where `{waitForJobsToComplete}` is `true`

### Schedule a job

Do a `POST` request to `scheduler/schedulejob` with in the body

```json
{
    "triggerKey": {
        "Name": "TriggerKeyName",
        "Group": "TriggerKeyGroup"
    },
    "jobKey": {
        "Name": "JobKeyName",
        "Group": "JobKeyGroup"
    },
    "description": "Description",
    "calendarName": "CalendarName",
    "jobDataMap": {
        "Key1": "Value1",
        "Key2": "Value2"
    },
    "startTimeUtc": "2022-05-12T16:16:37.7210025+00:00",
    "endTimeUtc": "2022-05-13T02:16:37.7210025+00:00",
    "finalFireTimeUtc": "2022-05-13T22:16:37.7210025+00:00",
    "priority": 5
}
```

This will return a datetime offset about when the job will be executed

