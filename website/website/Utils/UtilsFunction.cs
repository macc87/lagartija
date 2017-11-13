using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using website.Models;

namespace website.Utils
{
    public static class UtilsFunction
    {
        public enum PrizingRules
        {
            Winner_Takes_All = 0,
            Top_10 = 1,
            Top_3 = 2,
            Top_Third = 3,
            Winners_Half = 4, 

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Tuples.The first element is the amount of places and the second the price</returns>
        public static List<Tuple<int,double>>GetPrizingRules(Contest Contest, PrizingRules Rule, double SitePercentage)
        {
            List<Tuple<int, double>> result = new List<Tuple<int, double>>();
            switch (Rule)
            {
                case PrizingRules.Winner_Takes_All:
                    result = GetWinnerTakesAll(Contest, SitePercentage);
                    break;
                case PrizingRules.Top_10:
                    result = GetWinners_TopX(Contest, SitePercentage, 10);
                    break;
                case PrizingRules.Top_3:
                    result = GetWinners_TopX(Contest, SitePercentage, 3);
                    break;
                case PrizingRules.Top_Third:
                    result = GetWinners_TopX(Contest, SitePercentage, 1/3);
                    break;
                case PrizingRules.Winners_Half:
                    result = GetWinners_Half(Contest, SitePercentage);
                    break;
                default:
                    break;
            }
            return result;
        }

        private static double CalculateTotalWinnings(double entryFee, int maxCapacity, double SitePercentage)
        {
            double totalMoney = entryFee * maxCapacity;
            double siteCut = totalMoney * SitePercentage / 100;
            return totalMoney - siteCut;
        }
        private static List<Tuple<int, double>> GetWinnerTakesAll(Contest Contest, double SitePercentage)
        {
            List<Tuple<int, double>> result = new List<Tuple<int, double>>();
            double moneyToSplit = CalculateTotalWinnings(Contest.EntryFee, Contest.MaxCapacity, SitePercentage);
            result.Add(new Tuple<int, double>(1, moneyToSplit));
            return result;
        }
        private static List<Tuple<int, double>> GetWinners_Half(Contest Contest, double SitePercentage)
        {
            List<Tuple<int, double>> result = new List<Tuple<int, double>>();
            double moneyToSplit = CalculateTotalWinnings(Contest.EntryFee, Contest.MaxCapacity, SitePercentage);
            int winnerAmount = Contest.MaxCapacity / 2;
            double moneyToPay = moneyToSplit / winnerAmount;
            result.Add(new Tuple<int, double>(winnerAmount, moneyToPay));
            return result;
        }
        private static List<Tuple<int, double>> GetWinners_TopX(Contest Contest, double SitePercentage, int winnerAmount)
        {
            List<Tuple<int, double>> result = new List<Tuple<int, double>>();
            double moneyToSplit = CalculateTotalWinnings(Contest.EntryFee, Contest.MaxCapacity, SitePercentage);
            double moneyToPay = moneyToSplit / winnerAmount;
            for (int i = 1; i < winnerAmount; i++)
            {

            }
            result.Add(new Tuple<int, double>(winnerAmount, moneyToPay));
            return result;
        }
    }
}