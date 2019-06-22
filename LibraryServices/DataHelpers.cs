using LibraryData.Models;
using System;
using System.Collections.Generic;

namespace LibraryServices
{
  public class DataHelpers
  {
    public static IEnumerable<string> HumanizeBusinessHours(IEnumerable<BranchHour> branchHours)
    {
      var hours = new List<string>();

      foreach (var time in branchHours)
      {
        var day = HumanizeDay(time.DayOfWeek);
        var openTime = HumanizeTime(time.OpenTime);
        var closeTime = HumanizeTime(time.CloseTime);

        var timeEntry = $"{day} {openTime} to {closeTime}";
        hours.Add(timeEntry);
      }

      return hours;
    }

    public static object HumanizeTime(int time) => 
      TimeSpan.FromHours(time).ToString("hh':'mm");

    public static object HumanizeDay(int number) => 
      // -1 because days are 1 to 7 at DB and the method expects 0 to 6
      Enum.GetName(typeof(DayOfWeek), number - 1);
  }
}
