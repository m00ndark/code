using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ProcessController.DataObjects;

namespace ProcessController
{
    public static class ApplicationControl
    {
        static ApplicationControl()
        {
            Configuration = null;
        }

        #region Properties

        public static Configuration Configuration { get; set; }

        #endregion

        #region Access

        public static Application GetApplicationByID(string idStr)
        {
            Guid id;
            try { id = new Guid(idStr); } catch { return null; }
            return (Configuration != null ? Configuration.Applications.FirstOrDefault(app => (app.ID == id)) : null);
        }

        #endregion

        #region Start

        public static void StartApplication(Application application)
        {
            if (Configuration != null) Configuration.AddRecentUsage(application.ID.ToString());
            StartApplication(application, false);
        }

        public static void StartApplication(Application application, bool waitUntilStopped)
        {
            if (waitUntilStopped)
            {
                DateTime timeout = DateTime.Now.AddSeconds(30);
                while (DateTime.Now < timeout && application.IsRunning)
                {
                    Thread.Sleep(100);
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            application.Start();
        }

        public static void StartApplications(IEnumerable<Application> applications)
        {
            foreach (Application application in applications)
                StartApplication(application);
        }

        public static void StartApplicationsBySet(string set)
        {
            StartApplicationsBySet(set, false);
        }

        public static void StartApplicationsBySet(string set, bool waitUntilStopped)
        {
            if (Configuration != null)
            {
                Configuration.AddRecentUsage(set);
                foreach (Application application in Configuration.Applications.Where(app => (app.Sets.Contains(set))))
                    StartApplication(application, waitUntilStopped);
            }
        }

        #endregion

        #region Restart

        public static void RestartApplication(Application application)
        {
            StopApplication(application);
            if (Configuration != null) Configuration.AddRecentUsage(application.ID.ToString());
            StartApplication(application, true);
        }

        public static void RestartApplicationsBySet(string set)
        {
            StopApplicationsBySet(set);
            StartApplicationsBySet(set, true);
        }

        #endregion

        #region Stop

        public static void StopApplication(Application application)
        {
            application.Stop();
        }

        public static void StopApplications(IEnumerable<Application> applications)
        {
            foreach (Application application in applications)
                StopApplication(application);
        }

        public static void StopApplicationsBySet(string set)
        {
            if (Configuration != null)
                foreach (Application application in Configuration.Applications.Where(app => (app.Sets.Contains(set))))
                    StopApplication(application);
        }

	    #endregion
    }
}
