using Xunit;
using System;
using System.Collections.Generic;
using HairSalon.Objects;
using System.Data;
using System.Data.SqlClient;


namespace HairSalon
{
  public class HairSalonTest: IDisposable
  {
    public HairSalonTest()
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
    public void Test_Equal_ReturnsTrueIfNamesAreTheSame()
    {
      //Arrange, Act
      Client firstClient = new Client("Joe", "Monday");
      Client secondClient = new Client("Joe", "Monday");

      //Assert
      Assert.Equal(firstClient, secondClient);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Client testClient = new Client("Joe", "Monday", 0);

      //Act
      testClient.Save();
      List<Client> ClientList = Client.GetAll();

      //Assert
      Assert.Equal(ClientList[0], testClient);
    }

    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}
