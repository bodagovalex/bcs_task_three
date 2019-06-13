using CurrencyExchangeService;
using CurrencyExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyExplorer.Core
{
    public sealed class CurrencyAggregatedCourses
    {
        private readonly int[] AggregateIntervalArray = new int[] { 5, 15, 30, 60};

        private DataContext dataContext = null;
        
        public CurrencyAggregatedCourses()
        {
            dataContext  = new DataContext();
        }

        public List<CurrencyAggregatedCourseModel> GetCurrencCourses()
        {
            var m_resultList = new List<CurrencyAggregatedCourseModel>();
            try{
                var m_rateHistoryAll = dataContext.RateHistory.AsNoTracking().ToList();

                foreach (var currentInterval in AggregateIntervalArray)
                {
                    var m_rateHistoryInterval = m_rateHistoryAll.Where(r => r.TimeDifference <= 5);
                    if (m_rateHistoryInterval.Any())
                    {
                        var rateHistoryInsert = from r in m_rateHistoryInterval
                                group r by new
                                {
                                    r.CurrencyPair.ID,
                                    r.CurrencyPair.Name
                                } into rg
                                select new CurrencyAggregatedCourseModel()
                                {
                                    AggregateInterval = currentInterval,
                                    ValuePairName = rg.Key.Name,
                                    FirstValue = rg.First().Price,
                                    LastValue = rg.Last().Price,
                                    MaxValue = rg.Max(m => m.Price),
                                    MinValue = rg.Min(m => m.Price)
                                };

                        m_resultList.AddRange(rateHistoryInsert);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось получить курсы валют");
            }

            return m_resultList;
        }
    }
}