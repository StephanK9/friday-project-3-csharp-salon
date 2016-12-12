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
    [Fact]
    public void Test_IfGetAllStoresSeveralStylists_True()
    {
      Stylist newStylistOne = new Stylist("Beth Jones");
      Stylist newStylistTwo = new Stylist("Jeremy Myer");
      List<Stylist> allStylists = new List<Stylist>{};

      newStylistOne.Save();
      newStylistTwo.Save();
      allStylists = Stylist.GetAll();

      Assert.Equal(2, allStylists.Count);
    }
    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
