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
    "InStandbyMode": false,
    "JobStoreType": "Quartz.Simpl.RAMJobStore, Quartz, Version=3.4.0.0, Culture=neutral, PublicKeyToken=f6b8c98a402cc8a4",
    "JobStoreClustered": false,
    "JobsStoreSupportsPersistence": false,
    "NumbersOfJobsExecuted": 0,
    "RunningSince": "2022-05-11T16:30:06.5957565+00:00",
    "SchedulerInstanceId": "NON_CLUSTERED",
    "SchedulerName": "Boven",
    "SchedulerRemote": false,
    "SchedulerType": "Quartz.Impl.StdScheduler, Quartz, Version=3.4.0.0, Culture=neutral, PublicKeyToken=f6b8c98a402cc8a4",
    "Shutdown": false,
    "Started": true,
    "ThreadPoolSize": 10,
    "ThreadPoolType": "Quartz.Simpl.DefaultThreadPool, Quartz, Version=3.4.0.0, Culture=neutral, PublicKeyToken=f6b8c98a402cc8a4",
    "Version": "3.4.0.0"
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
    "TriggerKey": {
        "Name": "TriggerKeyName",
        "Group": "TriggerKeyGroup"
    },
    "JobKey": {
        "Name": "JobKeyName",
        "Group": "JobKeyGroup"
    },
    "Description": "Description",
    "CalendarName": "CalendarName",
    "JobDataMap": {
        "Key1": "Value1",
        "Key2": "Value2"
    },
    "StartTimeUtc": "2022-05-12T16:16:37.7210025+00:00",
    "EndTimeUtc": "2022-05-13T02:16:37.7210025+00:00",
    "FinalFireTimeUtc": "2022-05-13T22:16:37.7210025+00:00",
    "CronSchedule": "0 * * ? * *",
    "Priority": 5
}
```

This will return a datetime offset about when the job will be executed, for example `"2022-05-12T16:17:00+00:00"`

TODO: Task ScheduleJobs(IReadOnlyDictionary<IJobDetail, IReadOnlyCollection<ITrigger>> triggersAndJobs, bool replace);

TODO: Task ScheduleJob(IJobDetail jobDetail,IReadOnlyCollection<ITrigger> triggersForJob,bool replace,);
    
### Unschedule a job

Do a `POST` request to `scheduler/unschedulejob` with in the body the trigger of the job
   
```json    
{
    "Name": "TriggerKeyName",
    "Group": "TriggerKeyGroup"
}
```    
    
this will return `true` when the job is unscheduled or `false` when not    
 
### Unschedule multiple jobs

Do a `POST` request to `scheduler/unschedulejobs` with in the body the triggers of the jobs to unschedule
   
```json    
[
    {
        "Name": "TriggerKeyName1",
        "Group": "TriggerKeyGroup1"
    },
    {
        "Name": "TriggerKeyName2",
        "Group": "TriggerKeyGroup2"
    }
]
```      
    
this will return `true` when the jobs are unscheduled or `false` when not
    
TODO: Task<DateTimeOffset?> RescheduleJob(TriggerKey triggerKey, ITrigger newTrigger)    
    
TODO: Task AddJob(IJobDetail jobDetail, bool replace);
    
### Delete a job

Do a `POST` request to `scheduler/deletejob` with in the body the key of the job
   
```json    
{
    "Name": "JobKeyName",
    "Group": "JobKeyGroup"
}
```    
    
this will return `true` when the job is deleted or `false` when not        
    
### Delete multiple jobs

Do a `POST` request to `scheduler/deletejobs` with in the body the keys of the jobs
   
```json    
[
    {
        "Name": "JobKeyName1",
        "Group": "JobKeyGroup1"
    },
    {
        "Name": "JobKeyName2",
        "Group": "JobKeyGroup2"
    }
]
```    
    
### Trigger a job to execute NOW

Do a `POST` request to `scheduler/triggerjob` with in the body the key of the job
   
```json    
{
    "Name": "JobKeyName1",
    "Group": "JobKeyGroup1"
}
```     
    
### Trigger a job to execute NOW and associated a JobDataMap

Do a `POST` request to `scheduler/triggerjobwithdatamap` with in the body
   
```json    
{    
    "JobKey": {
        "Name": "JobKeyName",
        "Group": "JobKeyGroup"
    },
    "JobDataMap": {
        "Key1": "Value1",
        "Key2": "Value2"
    }
}
```         

### Pause a job
    
Do a `POST` request to `scheduler/pausejob` with in the body the key of the job
   
```json    
{
    "Name": "JobKeyName1",
    "Group": "JobKeyGroup1"
}
```        
    
### Pause multiple jobs
    
Do a `POST` request to `scheduler/pausejobs` with in the body the keys of the jobs to pause
TODO: Check json   
```json    
[    
    {
        "Name": "JobKeyName1",
        "Group": "JobKeyGroup1"
    },
    {
        "Name": "JobKeyName2",
        "Group": "JobKeyGroup2"
    }    
]    
```     
    
### Pause a trigger
    
Do a `POST` request to `scheduler/pausetrigger` with in the body the triggerkey
   
```json    
{
    "Name": "TriggerKeyName",
    "Group": "TriggerKeyGroup"
}
```   
    
### Pause multiple triggers
    
Do a `POST` request to `scheduler/pausetriggers` with in the body the triggerkeys to pause
TODO: Check json   
```json    
[    
    {
        "Name": "TriggerKeyName1",
        "Group": "TriggerKeyGroup1"
    },
    {
        "Name": "TriggerKeyName2",
        "Group": "TriggerKeyGroup2"
    }    
]    
```        
    
Errors returned
===============

When an error occures this is returned as a .NET exception in JSON format like this

```json
{
    "Message": "An error has occurred.",
    "ExceptionMessage": "Unable to store Trigger: 'TriggerKeyGroup.TriggerKeyName', because one already exists with this identification.",
    "ExceptionType": "Quartz.ObjectAlreadyExistsException",
    "StackTrace": "... the .NET stack trace ..."
}
```
