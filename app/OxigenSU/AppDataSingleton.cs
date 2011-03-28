using System;
using System.Collections.Generic;
using System.Text;

namespace OxigenSU
{
  public sealed class AppDataSingleton
  {
    private static volatile AppDataSingleton _instance;
    private static object _lockObject = new Object();

    private bool _bVerboseMode = false;    

    public bool IsVerboseMode
    {
      get
      {
        lock (_lockObject)
          return _bVerboseMode;
      }
      set
      {
        lock (_lockObject)
          _bVerboseMode = value;
      }
    }

    private AppDataSingleton() { }

    public static AppDataSingleton Instance
    {
      get
      {
        // first check if instance is null: if it isn't null, do not execute the lock block
        if (_instance == null)
        {
          lock (_lockObject)
          {
            // double check if instance exists as it may have been created between the first if and the lock
            if (_instance == null)
              _instance = new AppDataSingleton();
          }
        }

        return _instance;
      }
    }
  } 
}
