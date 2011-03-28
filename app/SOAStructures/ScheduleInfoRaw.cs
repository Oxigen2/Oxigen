using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OxigenIIAdvertising.SOAStructures
{
  [DataContract]
  public class ScheduleInfoRaw
  {
    private string _dayOfWeekCode;
    private StartEndDateTime[] _startEndDateTimes;

    [DataMember]
    public string DayOfWeekCode
    {
      get { return _dayOfWeekCode; }
      set { _dayOfWeekCode = value; }
    }

    [DataMember]
    public StartEndDateTime[] StartEndDateTimes
    {
      get { return _startEndDateTimes; }
      set { _startEndDateTimes = value; }
    }

    public ScheduleInfoRaw()
    {
      _dayOfWeekCode = null;
      _startEndDateTimes = null;
    }
  }

  [DataContract]
  public class StartEndDateTime
  {
    private string _startDateString;
    private string _endDateString;
    private string _startTimeString;
    private string _endTimeString;

    [DataMember]
    public string StartDateString
    {
      get { return _startDateString; }
      set { _startDateString = value; }
    }

    [DataMember]
    public string EndDateString
    {
      get { return _endDateString; }
      set { _endDateString = value; }
    }

    [DataMember]
    public string StartTimeString
    {
      get { return _startTimeString; }
      set { _startTimeString = value; }
    }

    [DataMember]
    public string EndTimeString
    {
      get { return _endTimeString; }
      set { _endTimeString = value; }
    }

    public StartEndDateTime() { }

    public StartEndDateTime(string startDateString, string endDateString, string startTimeString, string endTimeString)
    {
      _startDateString = startDateString;
      _endDateString = endDateString;
      _startTimeString = startTimeString;
      _endTimeString = endTimeString;
    }
  }
}
