using Xunit;
using System;
using System.Collections.Generic;
using HairSalon.Objects;
using System.Data;
using System.Data.SqlClient;

namespace  HairSalon
{
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_IfSaveAndReturnStylist_True()
    {
       Stylist expectedStylist = new Stylist("Beth Jones");
       List<Stylist> allStylists = new List<Stylist>{};

       expectedStylist.Save();
       allStylists = Stylist.GetAll();
       Stylist returnStylist = allStylists[0];

       Assert.Equal(expectedStylist, returnStylist);
    }
    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}
