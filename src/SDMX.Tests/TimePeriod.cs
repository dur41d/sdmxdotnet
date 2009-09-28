using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX.Tests
{
    public class TimePeriod
    {
        private DateTime dateTime;
        private TimePeriodType Type;
        private Quarterly quarter;
        private Weekly week;
        private Triannual triAnnum;
        private Biannual biAnuum;


        public TimePeriod(DateTime dateTime)
        {
            this.dateTime = dateTime;
            Type = TimePeriodType.DateTime;
        }

        public TimePeriod(int year, int month, int day)
        {
            dateTime = new DateTime(year, month, day);
            Type = TimePeriodType.Date;
        }

        public TimePeriod(int year, int month)
        {
            dateTime = new DateTime(year, month, 1);
            Type = TimePeriodType.YearMonth;
        }

        public TimePeriod(int year)
        {
            dateTime = new DateTime(year, 1, 1);
            Type = TimePeriodType.Year;
        }

        public TimePeriod(int year, Quarterly quarter)
        {
            dateTime = new DateTime(year, 1, 1);
            this.quarter = quarter;
            Type = TimePeriodType.Quarterly;
        }

        public TimePeriod(int year, Weekly week)
        {
            dateTime = new DateTime(year, 1, 1);
            this.week = week;
            Type = TimePeriodType.Weekly;
        }

        public TimePeriod(int year, Biannual annum)
        {
            dateTime = new DateTime(year, 1, 1);
            this.biAnuum = annum;
            Type = TimePeriodType.Biannual;
        }

        public TimePeriod(int year, Triannual annum)
        {
            dateTime = new DateTime(year, 1, 1);
            this.triAnnum = annum;
            Type = TimePeriodType.Triannual;
        }

        private enum TimePeriodType
        {
            DateTime,
            Date,
            YearMonth,
            Year,
            Quarterly,
            Weekly,
            Triannual,
            Biannual
        }
    }

    public enum Quarterly
    {
        Q1 = 1,
        Q2,
        Q3,
        Q4
    }

    public enum Weekly
    {
        W1 = 1,
        W2,
        W3,
        W4,
        W5,
        W6,
        W7,
        W8,
        W9,
        W10,
        W11,
        W12,
        W13,
        W14,
        W15,
        W16,
        W17,
        W18,
        W19,
        W20,
        W21,
        W22,
        W23,
        W24,
        W25,
        W26,
        W27,
        W28,
        W29,
        W30,
        W31,
        W32,
        W33,
        W34,
        W35,
        W36,
        W37,
        W38,
        W39,
        W40,
        W41,
        W42,
        W43,
        W44,
        W45,
        W46,
        W47,
        W48,
        W49,
        W50,
        W51,
        W52
    }

    public enum Triannual
    {
        T1 = 1,
        T2,
        T3
    }

    public enum Biannual
    {
        B1 = 1,
        B2
    }


}