﻿namespace QuartzWebApi.Loggers;

/// <summary>
///     Writes log information to the console
/// </summary>
public class Console() : Stream(System.Console.OpenStandardOutput());