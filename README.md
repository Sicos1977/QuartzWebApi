# QuartzWebApi
A REST api for Quartz.net

How to use
==========

## Check if a job group is paused

Do a `POST` request to `scheduler/isjobgrouppaused/{groupName}` , this will return `true` or `false`

## Check if a trigger group is paused

Do a `POST` request to `scheduler/istriggergrouppaused/{groupName}` , this will return `true` or `false`

## Get the schedulers name

Do a `GET` request to `scheduler/schedulername`

## Get the schedulers instance id

Do a `GET` request to `scheduler/schedulerinstanceid`

## Check if a scheduler is in standby mode

Do a `GET` request to `scheduler/instandbymode` , this will return `true` or `false`

## Check if a scheduler is in shutdown

Do a `GET` request to `scheduler/isshutdown` , this will return `true` or `false`

## Get the schedulers meta-data

Do a `GET` request to `scheduler/getmetadata` , this will return `true` or `false`
