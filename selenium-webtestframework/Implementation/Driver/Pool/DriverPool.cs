  using System.Collections.Concurrent;
  using selenium_webtestframework.Implementation.Driver.Util;

  namespace selenium_webtestframework.Implementation.Driver.Pool; 

  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T">Driver Class</typeparam>
  public class DriverPool<T> : IDriverPool<T> where T : notnull
  {
      /// <summary>
      /// DriverInstance Value = true, wenn in Benutzung, false, wenn frei
      /// </summary>
      private static readonly ConcurrentDictionary<T, DriverStatus> DriverInstances = new();

      private readonly object _lockObject = new();
      private readonly Configuration _configuration;
      private readonly TestContext _context;
      private readonly int _numberOfWorkers;

      public DriverPool(Configuration configuration, TestContext context)
      {
          _configuration = configuration;
          _context = context;
          _numberOfWorkers = GetNumberOfConfiguredWorkers();
      }

      /// <summary>
      /// Close all existing driver instances of the instantiated type
      /// </summary>
      public void CloseAllDriverInstances()
      {
          lock (_lockObject)
          {
              foreach (var driver in DriverInstances)
              {
                  (driver.Key as IDisposable)?.Dispose();
              }
          }
      }

      /// <summary>
      /// Releases the passed Driver instance of the instantiated type so that it can be used by another test case
      /// </summary>
      /// <param name="driver"></param>
      public void ReleaseDriverInstance(T driver)
      {
          lock (_lockObject)
          {
              DriverInstances[driver] = DriverStatus.Free;
          }
      }

      /// <summary>
      /// Allocates an unused driver and returns it
      /// </summary>
      /// <returns></returns>
      public T GetFreeDriver()
      {
          T freeDriver;

          lock (_lockObject)
          {
              if (DriverInstances.All(keyValuePair => keyValuePair.Value == DriverStatus.InUse))
              {
                  AddNewDriver();
              }

              freeDriver = DriverInstances.FirstOrDefault(keyValuePair => keyValuePair.Value == DriverStatus.Free).Key;

              if (freeDriver != null)
              {
                  DriverInstances[freeDriver] = DriverStatus.InUse;
              }
          }

          return freeDriver;
      }

      /// <summary>
      /// Creates the instances of the drivers and adds them to the dictionary (a.k.a. pool)
      /// </summary>
      public void AddNewDriver()
      {
          var genericType = typeof(T);

          lock (_lockObject)
          {
              var newDriver = (T)Activator.CreateInstance(genericType, args: _configuration)!;
              DriverInstances.TryAdd(newDriver, DriverStatus.Free);
          }

          _context.WriteLine($"Neuer Treiber - {genericType} - erstellt");
      }


      /// <summary>
      /// Determines the desired number of worker threads to be started(number of browsers, etc.) If this is configured with 0, the maximum possible number is determined
      /// </summary>
      /// <returns></returns>
      /// <exception cref="Exception"></exception>
      private int GetNumberOfConfiguredWorkers()
      {
          var assembly = typeof(DriverPool<T>).Assembly;
          var attributes = assembly.GetCustomAttributes(typeof(ParallelizeAttribute), false);
          if (attributes.Length <= 0 || attributes[0] is not ParallelizeAttribute attribute)
          {
              throw new Exception("Die Anzahl von Workern kann nicht bestimmen werden");
          }

          if (attribute.Workers.Equals(0))
          {
              GetCpuInfo(out var numberOfLogicalProcessors);
              return numberOfLogicalProcessors;
          }

          return attribute.Workers;
      }

      /// <summary>
      /// Determines the max. possible number of workers
      /// </summary>
      /// <param name="numberOfLogicalProcessors"></param>
      private void GetCpuInfo(out int numberOfLogicalProcessors)
      {
          numberOfLogicalProcessors = Environment.ProcessorCount;
      }

  }
