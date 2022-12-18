  using Firebase.Extensions;
  using System;
  using System.Threading.Tasks;
  using UnityEngine;

  public class FBInitializer : MonoBehaviour {
    //public GUISkin fb_GUISkin;
    private Vector2 controlsScrollViewVector = Vector2.zero;
    private Vector2 scrollViewVector = Vector2.zero;
    bool UIEnabled = true;
    private string logText = "";
    const int kMaxLogSize = 16382;
    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    public FirebaseStatus FbStatus = FirebaseStatus.Waiting;
    
    protected virtual void Awake() {
      Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
        dependencyStatus = task.Result;
        if (dependencyStatus == Firebase.DependencyStatus.Available) {
          InitializeFirebase();
        } else {
          Debug.LogError(
            "Could not resolve all Firebase dependencies: " + dependencyStatus);
        }
      });
    }

    // Initialize remote config, and set the default values.
    void InitializeFirebase() {
      // [START set_defaults]
      System.Collections.Generic.Dictionary<string, object> defaults =
        new System.Collections.Generic.Dictionary<string, object>();
        defaults.Add("url", "");

      Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults)
        .ContinueWithOnMainThread(task => {
        // [END set_defaults]
        DebugLog("RemoteConfig configured and ready!");
        FetchDataAsync();

      });

    }
    


    // [START fetch_async]
    // Start a fetch request.
    // FetchAsync only fetches new data if the current data is older than the provided
    // timespan.  Otherwise it assumes the data is "recent enough", and does nothing.
    // By default the timespan is 12 hours, and for production apps, this is a good
    // number. For this example though, it's set to a timespan of zero, so that
    // changes in the console will always show up immediately.
    public Task FetchDataAsync() {
      DebugLog("Fetching data...");
      System.Threading.Tasks.Task fetchTask =
      Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
          TimeSpan.Zero);

      return fetchTask.ContinueWithOnMainThread(FetchComplete);
    }
    //[END fetch_async]

    void FetchComplete(Task fetchTask) {
      if (fetchTask.IsCanceled) {
        DebugLog("Fetch canceled.");
            FbStatus = FirebaseStatus.Failed;
        } else if (fetchTask.IsFaulted) {
        DebugLog("Fetch encountered an error.");
            FbStatus = FirebaseStatus.Failed;
        } else if (fetchTask.IsCompleted) {
        DebugLog("Fetch completed successfully!");
            FbStatus = FirebaseStatus.Connected;
        }

      var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
      switch (info.LastFetchStatus) {
        case Firebase.RemoteConfig.LastFetchStatus.Success:
          Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
          .ContinueWithOnMainThread(task => {
            DebugLog(String.Format("Remote data loaded and ready (last fetch time {0}).",
                                 info.FetchTime));
          });

          break;
        case Firebase.RemoteConfig.LastFetchStatus.Failure:
          switch (info.LastFetchFailureReason) {
            case Firebase.RemoteConfig.FetchFailureReason.Error:
              DebugLog("Fetch failed for unknown reason");
              break;
            case Firebase.RemoteConfig.FetchFailureReason.Throttled:
              DebugLog("Fetch throttled until " + info.ThrottledEndTime);
              break;
          }
          break;
        case Firebase.RemoteConfig.LastFetchStatus.Pending:
          DebugLog("Latest Fetch call still pending.");
          break;
      }
    }



    // Output text to the debug log text field, as well as the console.
    public void DebugLog(string s) {
      print(s);
      logText += s + "\n";

      while (logText.Length > kMaxLogSize) {
        int index = logText.IndexOf("\n");
        logText = logText.Substring(index + 1);
      }

      scrollViewVector.y = int.MaxValue;
    }

    public enum FirebaseStatus
    {
        Waiting = 0,
        Connected = 1,
        Failed = 2
    }
  }
