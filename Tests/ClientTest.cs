using Xunit;
using System;
using System.Collections.Generic;
using HairSalon.Objects;
using System.Data;
using System.Data.SqlClient;


namespace HairSalon
{
  public class ClientTest: IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Client.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfNamesAreTheSame_True()
    {
      //Arrange, Act
      Client firstClient = new Client("Joe Klein", 0);
      Client secondClient = new Client("Joe Klein", 0);

      //Assert
      Assert.Equal(firstClient, secondClient);
    }

    [Fact]
    public void Test_Save_SavesToDatabase_True()
    {
      //Arrange
      Client testClient = new Client("Joe Klein", 1);
      List<Client> clientList = new List<Client>{};

      //Act
      testClient.Save();
      clientList = Client.GetAll();
      Client theClient = clientList[1];

      //Assert
      Assert.Equal(testClient, theClient);

    }

    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}
