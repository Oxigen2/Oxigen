using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Globalization;
using System.Collections.ObjectModel;

namespace OxigenIIAdvertising.AssetScheduling
{
  /// <summary>
  /// object with methods to determine asset playability according to temporal scheduling
  /// </summary>
  public class AssetScheduler
  {
    private Hashtable _vars = null;
    private Hashtable _daysofWeek = null;
    private CultureInfo _cultureInfo;
    private DateTimeStyles _dateTimeStyles;
    Regex _equalityOperators = new Regex(@"(?:<=|>=|!=|<|>|=)", RegexOptions.Compiled);

    /// <summary>
    /// Initializes constants and variables dependant on the current date and time
    /// </summary>
    public AssetScheduler()
    {
      _cultureInfo = CultureInfo.CreateSpecificCulture("en-GB");
      _dateTimeStyles = DateTimeStyles.None;

      _vars = GetVars();
      _daysofWeek = GetDaysOfWeek();
    }

    /// <summary>
    /// Analyzes an asset's temporal criteria and determines if an asset can be played at current date and time.
    /// </summary>
    /// <param name="inputConditionsCollection">a Collection object with the temporal criteria</param>
    /// <returns>true if the temporal criteria are met</returns>
    public bool IsAssetPlayable(string[] inputConditionsCollection)
    {
      foreach (string inputCondition in inputConditionsCollection)
      {
        if (IsAssetPlayableIndividualLine(inputCondition))
          return true;
      }

      return false;
    }

    private bool IsAssetPlayableIndividualLine(string inputCondition)
    {
      inputCondition = inputCondition.Replace(" ", "").ToLower();

      string[] conditions = inputCondition.Split(new string[] { "and" }, StringSplitOptions.RemoveEmptyEntries);

      string variable = "";
      string value = "";
      string comparisonOperator = "";

      foreach (string condition in conditions)
      {
        // extract the comparison operator using a copmpiled regex
        comparisonOperator = (_equalityOperators.Match(condition)).Value;

        string[] operands = _equalityOperators.Split(condition); // if comparisonOperator not found, condition won't split

        if (operands.Length != 2)
          return false;
        else
        {
          variable = operands[0];
          value = operands[1];

          // is variable in the variable dictionary
          if (!_vars.Contains(variable))
            return false;

          // is condition true according to given date and hour
          // check for false only. if sub-condition is true, must continue on with remaining sub-conditions
          if (!TemporalSchedulingPassed(variable, value, comparisonOperator))
            return false;
        }
      }      

      return true;
    }

    // check if the time unit in this condition satisfied current date/time
    private bool TemporalSchedulingPassed(string variable, string value, string comparisonOperator)
    {
      switch (variable)
      {
        case "hour":
          return HourPlayable(value, comparisonOperator);

        case "minute":
          return MinutePlayable(value, comparisonOperator);
          
        case "time":
          return TimePlayable(value, comparisonOperator);

        case "dayofweek":
          return DayOfWeekPlayable(value, comparisonOperator);

        case "dayofmonth":
          return DayOfMonthPlayable(value, comparisonOperator);

        case "week":
          return WeekPlayable(value, comparisonOperator);

        case "month":
          return MonthPlayable(value, comparisonOperator);
      
        case "year":
          return YearPlayable(value, comparisonOperator);
 
        case "date":
          return DatePlayable(value, comparisonOperator);
      }

      return false;
    }

    private bool DayOfMonthPlayable(string value, string comparisonOperator)
    {
      int currentDayOfMonth = DateTime.Now.Day;

      if (value.Length > 2) // day of month must be 1 or 2 digits long
        return false;

      int outDay;

      if (!int.TryParse(value, out outDay))
        return false;

      if (outDay < 0 || outDay > DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
        return false;

      return Compare(comparisonOperator, currentDayOfMonth, outDay);
    }

    private bool MinutePlayable(string value, string comparisonOperator)
    {
      int currentMinuteOfHour = DateTime.Now.Minute;

      if (value.Length > 2) // minute must be 1 or 2 digits long
        return false;

      int outMinute;

      if (!int.TryParse(value, out outMinute))
        return false;

      if (outMinute >= 60 || outMinute < 0)
        return false;

      return Compare(comparisonOperator, currentMinuteOfHour, outMinute);
    }

    private bool HourPlayable(string value, string comparisonOperator)
    {
      int currentHourOfDay = DateTime.Now.Hour;

      if (value.Length > 2) // time must be 1 or 2 digits long
        return false;

      int outHour;

      if (!int.TryParse(value, out outHour))
        return false;

      if (outHour >= 24 || outHour < 0)
        return false;

      return Compare(comparisonOperator, currentHourOfDay, outHour);
    }

    private bool TimePlayable(string value, string comparisonOperator)
    {
      TimeSpan curTimeOfDay = DateTime.Now.TimeOfDay;

      if (value.Length != 4) // time must be 4 digits long
        return false;

      TimeSpan outTime;

      if (!TimeSpan.TryParse("0." + value.Substring(0, 2) + ":" + value.Substring(2, 2) + ":00", out outTime))
        return false;

      return Compare(comparisonOperator, curTimeOfDay, outTime);
    }
        
    private bool DayOfWeekPlayable(string value, string comparisonOperator)
    {
      int curDayOfWeek = (int)_daysofWeek[DateTime.Now.DayOfWeek.ToString().ToLower()];

      if (!_daysofWeek.Contains(value))
        return false;

      int numericalvalue = (int)_daysofWeek[value];

      return Compare(comparisonOperator, curDayOfWeek, numericalvalue);
    }

    private bool WeekPlayable(string value, string comparisonOperator)
    {
      int curWeekOfYear = _cultureInfo.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

      int numericalValue;

      if (!int.TryParse(value, out numericalValue))
        return false;

      return Compare(comparisonOperator, curWeekOfYear, numericalValue);
    }

    private bool MonthPlayable(string value, string comparisonOperator)
    {
      int curMonth = DateTime.Now.Month;

      int numericalValue;

      if (!int.TryParse(value, out numericalValue))
        return false;

      return Compare(comparisonOperator, curMonth, numericalValue);
    }

    // curDate does not hols time information, only date
    private bool DatePlayable(string value, string comparisonOperator)
    {
      DateTime curDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

      DateTime outDate;

      if (!DateTime.TryParse(value, _cultureInfo, _dateTimeStyles, out outDate))
        return false;

      return Compare(comparisonOperator, curDate, outDate);     
    }

    private bool YearPlayable(string value, string comparisonOperator)
    {
      int curYear = DateTime.Now.Year;

      int numericalValue;

      if (!int.TryParse(value, out numericalValue))
        return false;

      return Compare(comparisonOperator, curYear, numericalValue);
    }

    private bool Compare(string comparisonOperator, IComparable currentTimeUnit, IComparable comparedTimeUnit)
    {
      switch (comparisonOperator)
      {
        case "=":
          return currentTimeUnit.CompareTo(comparedTimeUnit) == 0;

        case ">=":
          return currentTimeUnit.CompareTo(comparedTimeUnit) > 0 || currentTimeUnit.CompareTo(comparedTimeUnit) == 0;

        case "<=":
          return currentTimeUnit.CompareTo(comparedTimeUnit) < 0 || currentTimeUnit.CompareTo(comparedTimeUnit) == 0;

        case ">":
          return currentTimeUnit.CompareTo(comparedTimeUnit) > 0;

        case "<":
          return currentTimeUnit.CompareTo(comparedTimeUnit) < 0;

        case "!=":
          return currentTimeUnit.CompareTo(comparedTimeUnit) != 0;
      }

      return false;
    }

    private Hashtable GetVars()
    {
      Hashtable ht = new Hashtable();

      ht.Add("dayofmonth", "");
      ht.Add("hour", "");
      ht.Add("minute", "");
      ht.Add("time", ""); 
      ht.Add("dayofweek", "");
      ht.Add("week", "");
      ht.Add("month", "");
      ht.Add("year", "");
      ht.Add("date", "");

      return ht;
    }
    
    private Hashtable GetDaysOfWeek()
    {
      Hashtable ht = new Hashtable();

      ht.Add("monday", 1);
      ht.Add("tuesday", 2);
      ht.Add("wednesday", 3);
      ht.Add("thursday", 4);
      ht.Add("friday", 5);
      ht.Add("saturday", 6);
      ht.Add("sunday", 7);

      return ht;
    }
  }
}
