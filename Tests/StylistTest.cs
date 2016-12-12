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
    [Fact]
    public void Test_IfGetAllAlsoReturnsClients_True()
    {
      Stylist newStylist = new Stylist("Beth Jones");
      newStylist.Save();
      Client newClientOne = new Client("Joe Klein", newStylist.GetId());
      Client newClientTwo = new Client("Arnold Weber", newStylist.GetId());
      List<Client> allClients = new List<Client>{newClientOne, newClientTwo};

      newClientOne.Save();
      newClientTwo.Save();
      List<Client> returnClients = newStylist.GetClients();

      Assert.Equal(allClients, returnClients);
    }
    [Fact]
    public void Test_FindStylist_True()
    {
      Stylist newStylist = new Stylist("Beth Jones");

      newStylist.Save();
      Stylist foundStylist = Stylist.Find(newStylist.GetId());

      Assert.Equal(newStylist, foundStylist);
    }
    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
