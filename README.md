# QuartzWebApi
A self hosted Web API for Quartz.Net, hosting on .net 4.8 is done with OWIN and on .net6 and higher with Kestrel

## Installing via NuGet (not yet done)

[![NuGet](https://img.shields.io/nuget/v/QuartzWebApi.svg?style=flat-square)](https://www.nuget.org/packages/QuartzWebApi)

# How to host Quartz.Net

```c#
var host = new SchedulerHost("http://localhost:44344", <IScheduler>, <ILogger>);
host.Start();
```

Where `IScheduler` is your Quartz.Net scheduler and `ILogger` any logger that implements the Microsoft ILogger interface (or null if you don't want any logging) 

# How to connect to the host

```c#
var connector = new SchedulerConnector("http://localhost:44344");
```

## License Information

QuartzWebApi is Copyright (C) 2022 - 2024 Magic-Sessions and is licensed under the MIT license:

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NON INFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.

Core Team
=========
    Sicos1977 (Kees van Spelde)

Logging
=======

QuartzWebApi uses the Microsoft ILogger interface (https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging.ilogger?view=dotnet-plat-ext-5.0). You can use any logging library that uses this interface.

QuartzWebApi has some build in loggers that can be found in the ```QuartzWebApi.Logger``` namespace. 

For example

```csharp
var logger = !string.IsNullOrWhiteSpace(<some logfile>)
                ? new ChromeHtmlToPdfLib.Loggers.Stream(File.OpenWrite(<some logfile>))
                : new ChromeHtmlToPdfLib.Loggers.Console();
```

Most of the log informartion is logged at the `information` level, small answers like `booleans`, `DateTimeOffsets` and small `strings` (like the schedulers name) are also logged at this level. All the json that is received and sent back is logged at the `debug` level.

How to use the API directly
==========

### Check if a job group is paused

Do a `POST` request to `Scheduler/IsJobGroupPaused/{groupName}`, where `{groupName}` is the name of the group this will return `true` or `false`

### Check if a trigger group is paused

Do a `POST` request to `Scheduler/IsTriggerGroupPaused/{groupName}`, where `{groupName}` is the name of the group this will return `true` or `false`

### Get the schedulers name

Do a `GET` request to `Scheduler/SchedulerName`, this will return the name of the scheduler

### Get the schedulers instance id

Do a `GET` request to `Scheduler/SchedulerInstanceId`, this will return the schedulers instance id

### Get the schedulers context

Do a `GET` request to `Scheduler/SchedulerContext`

When the context is;

- key1 - value1
- key2 - value2
- key3 - value3

it will return

```json
{
    "key1": "value1",
    "key2": "value2",
    "key3": "value3"
}
```

### Check if a scheduler is in standby mode

Do a `GET` request to `Scheduler/InStandbyMode` , this will return `true` or `false`

### Check if a scheduler is in shutdown

Do a `GET` request to `Scheduler/Isshutdown` , this will return `true` or `false`

### Get the schedulers meta-data

Do a `GET` request to `Scheduler/GetMetaData` , this will return the meta-data that will look something like this

```json
{
    "InStandbyMode": false,
    "JobStoreType": "Quartz.Simpl.RAMJobStore, Quartz, Version=3.4.0.0, Culture=neutral, PublicKeyToken=f6b8c98a402cc8a4",
    "JobStoreClustered": false,
    "JobsStoreSupportsPersistence": false,
    "NumbersOfJobsExecuted": 0,
    "RunningSince": "2022-05-11T16:30:06.5957565+00:00",
    "SchedulerInstanceId": "NON_CLUSTERED",
    "SchedulerName": "The name of the scheduler",
    "SchedulerRemote": false,
    "SchedulerType": "Quartz.Impl.StdScheduler, Quartz, Version=3.4.0.0, Culture=neutral, PublicKeyToken=f6b8c98a402cc8a4",
    "Shutdown": false,
    "Started": true,
    "ThreadPoolSize": 10,
    "ThreadPoolType": "Quartz.Simpl.DefaultThreadPool, Quartz, Version=3.4.0.0, Culture=neutral, PublicKeyToken=f6b8c98a402cc8a4",
    "Version": "3.4.0.0"
}
```

### Get the currently executing jobs

Do a `GET` request to `Scheduler/GetCurrentlyExecutingJobs` , this will return the currently executing jobs that will look like this

```json
[
    "JobGroup1",
    "JobGroup2"
]
```

### Get the job group names

Do a `GET` request to `Scheduler/GetJobGroupNames` , this will return the job group names that will look like this

```json
[
    "JobGroup1",
    "JobGroup2"
]
```

### Get the trigger group names

Do a `GET` request to `Scheduler/GetTriggerGroupNames` , this will return

```json
[
    "TriggerGroup1",
    "TriggerGroup2"
]
```

### Get the paused trigger groups

Do a `GET` request to `Scheduler/GetPausedTriggerGroups` , this will return

```json
[
    "PausedTriggerGroup1",
    "PausedTriggerGroup2"
]
```

### Start the scheduler

Do a `POST` request to `Scheduler/Start`

### Start the scheduler with a delay

Do a `POST` request to `Scheduler/StartDelayed/{delay}` where `{delay}` is the amount of *seconds* you want to delay the start

### Check if the scheduler is started

Do a `GET` request to `Scheduler/IsStarted`, this will return `true` or `false`

### Check if the scheduler is in standby mode

Do a `GET` request to `Scheduler/Standby`, this will return `true` or `false`

### Shutdown the scheduler

Do a `POST` request to `Scheduler/Shutdown`

### Shutdown the scheduler but wait for all jobs to complete

Do a `POST` request to `Scheduler/Shutdown/{waitForJobsToComplete}` where `{waitForJobsToComplete}` is `true`

### Schedule a job with a job detail and a trigger

Do a `POST` request to `Scheduler/ScheduleJobWithJobDetailAndTrigger` with in the body the job detail and the trigger
   
```json
{
  "JobDetail": {
    "JobKey": {
      "Name": "Name",
      "Group": "Group"
    },
    "Description": "description",
    "JobType": "jobType",
    "JobDataMap": {
      "key": "value"
    },
    "Durable": true,
    "Replace": false,
    "StoreNonDurableWhileAwaitingScheduling": false
  },
  "Trigger": {
    "TriggerKey": {
      "Name": "name",
      "Group": "group"
    },
    "Description": "description",
    "StartTimeUtc": "2022-08-15T17:34:04.5786231+00:00",
    "EndTimeUtc": "2022-08-15T17:34:04.5786231+00:00",
    "Priority": 5,
    "CronSchedule": "0 * * ? * *",
    "Priority": 5,    
    "JobKey": {
      "Name": "name",
      "Group": "value"
    },
    "JobDataMap": {
      "key": "value"
    }
  }
}
```  

### Schedule the give trigger with the job identified by the trigger

Do a `POST` request to `Scheduler/ScheduleJobIdentifiedWithTrigger` with in the body

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

### Schedule multiple jobs with one or more associated trigger

TODO: Task ScheduleJobs(IReadOnlyDictionary<IJobDetail, IReadOnlyCollection<ITrigger>> triggersAndJobs, bool replace);

### Schedule a job with one or more associated trigger

TODO: Task ScheduleJobs(IReadOnlyDictionary<IJobDetail, IReadOnlyCollection<ITrigger>> triggersAndJobs, bool replace);

### Schedule a job with a job detail and a related set of triggers

Do a `POST` request to `Scheduler/ScheduleJobWithJobDetailAndTriggers` with in the body the job detail and a related set of triggers
   
```json
{
  "JobDetail": {
    "JobKey": {
      "Name": "Name",
      "Group": "Group"
    },
    "Description": "description",
    "JobType": "jobType",
    "JobDataMap": {
      "key": "value"
    },
    "Durable": true,
    "Replace": false,
    "StoreNonDurableWhileAwaitingScheduling": false
  },
  "Triggers": [
    {
      "TriggerKey": {
        "Name": "name",
        "Group": "group"
      },
      "Description": "description",
      "CalendarName": "calenderName",
      "CronSchedule": "0 * * ? * *",
      "Priority": 5,
      "JobKey": {
        "Name": "name",
        "Group": "value"
      },
      "JobDataMap": {
        "key": "value"
      }
    }
  ],
  "Replace": false
}
```   
   
When you get an error like `Could not find an IJob class with the type '<the job type>'` then make sure that you have added the full namespace with the `JobType`
   
### Unschedule a job

Do a `POST` request to `Scheduler/UnscheduleJob` with in the body the trigger of the job
   
```json    
{
    "Name": "TriggerKeyName",
    "Group": "TriggerKeyGroup"
}
```    
    
this will return `true` when the job is unscheduled or `false` when not    
 
### Unschedule multiple jobs

Do a `POST` request to `Scheduler/UnscheduleJobs` with in the body the triggers of the jobs to unschedule
   
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
    
### Reschedule job

Do a `POST` request to `Scheduler/RescheduleJob` with in the body the reschedulejob object

```json    
{
    "CurrentTriggerKey": {
        "Name": "CurrentTriggerKeyName",
        "Group": "CurrentTriggerKeyGroup"
    },
    "NewTrigger": {
        "TriggerKey": {
            "Name": "NewTriggerKeyName",
            "Group": "NewTriggerKeyGroup"
        },
        "JobKey": {
            "Name": "NewJobKeyName",
            "Group": "NewJobKeyGroup"
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
}
```  

Returns 'null' if a trigger with the given name and group was not found and removed from the store (and the new trigger is therefore not stored), otherwise the first fire time of the newly scheduled trigger
    
### Add a job with no associated trigger

Do a `POST` request to `Scheduler/AddJob` with in the body the addjob object    
    
```json    
{
    "JobKey": {
        "Name": "JobKeyName",
        "Group": "JobKeyGroup"
    },
    "Description": "Description",
    "JobType": "JobType",
    "JobDataMap": {
        "Key1": "Value1",
        "Key2": "Value2"
    },
    "Durable": true,
    "Replace": false,
    "StoreNonDurableWhileAwaitingScheduling": true
}
```  
    
### Delete a job

Do a `POST` request to `Scheduler/DeleteJob` with in the body the key of the job
   
```json    
{
    "Name": "JobKeyName",
    "Group": "JobKeyGroup"
}
```    
    
this will return `true` when the job is deleted or `false` when not        
    
### Delete multiple jobs

Do a `DELETE` request to `Scheduler/DeleteJobs` with in the body the keys of the jobs
   
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

Do a `POST` request to `Scheduler/TriggerJobWithJobkey` with in the body the key of the job
   
```json    
{
    "Name": "JobKeyName1",
    "Group": "JobKeyGroup1"
}
```     
    
### Trigger a job to execute NOW and associated a JobDataMap

Do a `POST` request to `Scheduler/TriggerJobWithDataMap` with in the body
   
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
    
Do a `POST` request to `Scheduler/PauseJob` with in the body the key of the job
   
```json    
{
    "Name": "JobKeyName",
    "Group": "JobKeyGroup"
}
```        
    
### Pause multiple jobs
    
Do a `POST` request to `Scheduler/PauseJobs` with in the body the group matching object that defined what jobs to pause

The values for `Type` can be: `Contains`, `EndsWith`, `Equals` or `StartsWith`
    
```json    
{
  "Type": "Contains",
  "Value": "<The job keys to match>"
}   
```     
    
### Pause a trigger
    
Do a `POST` request to `Scheduler/PauseTrigger` with in the body the triggerkey
   
```json    
{
    "Name": "TriggerKeyName",
    "Group": "TriggerKeyGroup"
}
```   
    
### Pause multiple triggers
    
Do a `POST` request to `Scheduler/PauseTriggers` with in the body the group matching object that defined what triggers to pause
    
The values for `Type` can be: `Contains`, `EndsWith`, `Equals` or `StartsWith`
    
```json    
{
  "Type": "Contains",
  "Value": "<The trigger keys to match>"
}   
```        
    
### Resume a job
    
Do a `POST` request to `Scheduler/ResumeJob` with in the body the key of the job
    
```json    
{
    "Name": "JobKeyName",
    "Group": "JobKeyGroup"
}
```         
    
### Resume multiple jobs
    
Do a `POST` request to `Scheduler/ResumeJobs` with in the body the group matching object that defined what jobs to resume
    
The values for `Type` can be: `Contains`, `EndsWith`, `Equals` or `StartsWith`
    
```json    
{
  "Type": "Contains",
  "Value": "<The job keys to match>"
}   
```     
    
### Resume a trigger
    
Do a `POST` request to `Scheduler/ResumeTrigger` with in the body the key of the trigger
    
```json    
{
    "Name": "TriggerKeyName",
    "Group": "TriggerKeyGroup"
}
```      
    
### Resume multiple triggers
    
Do a `POST` request to `Scheduler/ResumeTriggers` with in the body the group matching object that defined what triggers to resume
    
The values for `Type` can be: `Contains`, `EndsWith`, `Equals` or `StartsWith`
    
```json    
{
  "Type": "Contains",
  "Value": "<The trigger keys to match>"
}   
```

### Pause all triggers
    
Do a `POST` request to `Scheduler/PauseAllTriggers`

### Resume all triggers
    
Do a `POST` request to `Scheduler/ResumeAllTriggers`

### Get job keys
    
Do a `GET` request to `Scheduler/GetJobKeys`

The values for `Type` can be: `Contains`, `EndsWith`, `Equals` or `StartsWith`
    
```json    
{
  "Type": "Contains",
  "Value": "<The job keys to match>"
}   
```     

it will return something like this

```json
[
  {
    "Name": "JobKeyName",
    "Group": "JobKeyGroup"
  }
]
```

### Get triggers of job
    
Do a `GET` request to `Scheduler/GetTriggersOfJob` with in the body the key of the job
   
```json    
{
    "Name": "JobKeyName",
    "Group": "JobKeyGroup"
}
```     

It will return something like this

```json   
[
  {
    "TriggerKey": {
      "Name": "triggerKey",
      "Group": "DEFAULT"
    },
    "Description": "TestTrigger",
    "CalendarName": null,
    "CronSchedule": null,
    "NextFireTimeUtc": null,
    "PreviousFireTimeUtc": "2024-01-28T11:21:14.4966492+01:00",
    "StartTimeUtc": "2024-01-28T11:21:14.4966492+01:00",
    "EndTimeUtc": null,
    "FinalFireTimeUtc": "2024-01-28T11:21:14.4966492+01:00",
    "Priority": 5,
    "HasMillisecondPrecision": true,
    "JobKey": null,
    "JobDataMap": null
  }
]
```     

### Get trigger keys
    
Do a `GET` request to `Scheduler/GetTriggerKeys`

The values for `Type` can be: `Contains`, `EndsWith`, `Equals` or `StartsWith`
    
```json    
{
  "Type": "Contains",
  "Value": "<The group keys to match>"
}   
```     

it will return something like this

```json
[
  {
    "Name": "triggerKey",
    "Group": "DEFAULT"
  }
]
```

### Get the job detail
    
Do a `GET` request to `Scheduler/GetJobDetail` with in the body the key of the job
   
```json    
{
    "Name": "JobKeyName",
    "Group": "JobKeyGroup"
}
```     

It will return something like this

```json   
{
  "JobKey": {
    "Name": "JobKeyName",
    "Group": "JobKeyGroup"
  },
  "Description": "Test",
  "JobType": "QuartzWebApi.TestJob",
  "JobDataMap": {},
  "Durable": false,
  "Replace": false,
  "StoreNonDurableWhileAwaitingScheduling": false
}
```   

### Get trigger
    
Do a `GET` request to `Scheduler/GetTrigger` with in the body the key of the trigger
   
```json    
{
    "Name": "TriggerKeyName",
    "Group": "TriggerKeyGroup"
}
```     

It will return something like this

```json   
{
  "TriggerKey": {
    "Name": "triggerKey",
    "Group": "DEFAULT"
  },
  "Description": "TestTrigger",
  "CalendarName": null,
  "CronSchedule": null,
  "NextFireTimeUtc": null,
  "PreviousFireTimeUtc": "2024-01-28T14:09:21.9475007+01:00",
  "StartTimeUtc": "2024-01-28T14:09:21.9475007+01:00",
  "EndTimeUtc": null,
  "FinalFireTimeUtc": "2024-01-28T14:09:21.9475007+01:00",
  "Priority": 5,
  "HasMillisecondPrecision": true,
  "JobKey": null,
  "JobDataMap": null
}
```   

Do a `GET` request to `Scheduler/GetTriggerState` with in the body the key of the trigger
   
```json    
{
    "Name": "TriggerKeyName",
    "Group": "TriggerKeyGroup"
}
```     

It will return something like this

```json   
"Normal"
```   

### Add calendar

Do a `POST` request to `Scheduler/AddCalendar` with in the body the body the calendar you want to add, for example
   
```json    
{
  "Name" : "My new CRON calendar",
  "Type": "Cron"
  "CronExpression": "0 0-5 14 * * ?",
  "Description": "my description"
  "Replace": true
  "UpdateTriggers": true
}
```     

### Delete a calendar

Do a `DELETE` request to `Scheduler/DeleteCalendar/{calName}` where `calName` is het name of the calendar to delete, this will return `true` or `false`

### Get a calendar

Do a `GET` request to `Scheduler/GetCalendar/{calName}` where `calName` is het name of the calendar to get, when the calendar exists it will return something like this

```json    
{
  "CronExpression": "0 0-51 4 * * ?",
  "Name": null,
  "Type": "Cron",
  "TimeZone": {
    "Id": "W. Europe Standard Time",
    "DisplayName": "(UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna",
    "StandardName": "W. Europe Standard Time",
    "DaylightName": "W. Europe Daylight Time",
    "BaseUtcOffset": "01:00:00",
    "AdjustmentRules": [
      {
        "DateStart": "0001-01-01T00:00:00",
        "DateEnd": "9999-12-31T00:00:00",
        "DaylightDelta": "01:00:00",
        "DaylightTransitionStart": {
          "TimeOfDay": "0001-01-01T02:00:00",
          "Month": 3,
          "Week": 5,
          "Day": 1,
          "DayOfWeek": 0,
          "IsFixedDateRule": false
        },
        "DaylightTransitionEnd": {
          "TimeOfDay": "0001-01-01T03:00:00",
          "Month": 10,
          "Week": 5,
          "Day": 1,
          "DayOfWeek": 0,
          "IsFixedDateRule": false
        },
        "BaseUtcOffsetDelta": "00:00:00"
      }
    ],
    "SupportsDaylightSavingTime": true
  }
}
``` 

### Get calendar names

Do a `GET` request to `Scheduler/GetCalendarNames`, it will return something like this

```json
[
    "monthlyCalendar",
    "MynewCRONcalendar"
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
