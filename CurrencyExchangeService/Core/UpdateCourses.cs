using CurrencyExchangeService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using Tiny.RestClient;

namespace CurrencyExchangeService.Core
{
    public sealed class UpdaterCourse
    {
        const string API_KEY_NAME = "apiKey";
        const string API_SERVER_KEY_NAME = "CurrencyUpdateAPI";
        const string COMPACT_PARAM_NAME = "compact";
        const string COMPACT_PARAM_VALUE = "ultra";

        private static UpdaterCourse update = null;

        private DataContext dbContext = null;
        private TinyRestClient restClient = null;
        private string serverAPIkey = null;

        private UpdaterCourse()
        {
            dbContext = new DataContext();
            serverAPIkey = ConfigurationSettings.AppSettings[API_KEY_NAME];
            var m_serverName = ConfigurationSettings.AppSettings[API_SERVER_KEY_NAME];
            if (string.IsNullOrEmpty(serverAPIkey) || string.IsNullOrEmpty(m_serverName))
                throw new Exception("Server or key not found.");

            restClient = new TinyRestClient(new HttpClient(), m_serverName);
        }

        private string convertCurryncyPairToQueryParams(List<CurrencyPair> p_currencyPairsList)
        {
            var pairList = p_currencyPairsList.Select(c => c.PairCode).ToList();
            
            return pairList.Count() > 0 ? string.Join(",", pairList) : string.Empty;
        }

        private void writeRateHistory(Dictionary<string, double> p_currentCourseDictionary, List<CurrencyPair> p_currencyPairs)
        {
            foreach(var item in p_currentCourseDictionary)
            {
                var selectedPair = p_currencyPairs.FirstOrDefault(f => f.PairCode.Equals(item.Key));

                if(selectedPair != null)
                    dbContext.RateHistory.Add(new RateHistory(selectedPair.ID, item.Value));
            }

            dbContext.SaveChanges();
        }

        public static UpdaterCourse GetInstance()
        {
            if (update == null)
                update = new UpdaterCourse();

            return update;
        }

        public void Update()
        {
            var m_currencyPairList = dbContext.CurrencyPair.AsNoTracking()
                                                         .Where(cp => cp.isDeleted.Equals(false))
                                                         .ToList();

            var m_currentCourseDictionary = restClient.
                  GetRequest("convert").
                  AddQueryParameter("q", convertCurryncyPairToQueryParams(m_currencyPairList)).
                  AddQueryParameter(COMPACT_PARAM_NAME, COMPACT_PARAM_VALUE).
                  AddQueryParameter(API_KEY_NAME, serverAPIkey).
                  ExecuteAsync<Dictionary<string, double>>().
                  Result;

            writeRateHistory(m_currentCourseDictionary, m_currencyPairList);
        }
    }

    public static class UpdateServices
    {
        const string UPDATE_INTERVAL = "60000";
        const string UPDATE_INTERVAL_KEY_NAME = "UpdateInterval";
        const string APPLICATION_LOG = "Application";

        private static bool enableUpdate = false;
        private static List<Thread> threadPool = null;

        private static void UpdateCourses()
        {
            try
            {
                var m_interval = 0;
                int.TryParse(ConfigurationSettings.AppSettings[UPDATE_INTERVAL_KEY_NAME] ?? UPDATE_INTERVAL, out m_interval);

                while (enableUpdate)
                {
                    UpdaterCourse.GetInstance().Update();
                    Thread.Sleep(m_interval);
                }
            }
            catch (System.Exception ex)
            {
                EventLog.WriteEntry(APPLICATION_LOG,
                                    $"An error occurred in the service 'CurrencyExhcangeService': {ex.Message.ToString()}",
                                    EventLogEntryType.Error);
            }
        }

        public static void Start()
        {
            enableUpdate = true;
            threadPool = new List<Thread>();
            threadPool.Add(new Thread(new ThreadStart(UpdateCourses)));

            threadPool.ForEach(t => t.Start());
        }

        public static void Stop()
        {
            enableUpdate = false;
            threadPool.ForEach(t => t.Abort());
            threadPool = null;
        }
    }
}
