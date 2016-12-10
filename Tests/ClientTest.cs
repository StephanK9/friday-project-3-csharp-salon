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
      Client testClient = new Client("Joe Klein", 0);
      List<Client> clientList = new List<Client>{};

      //Act
      testClient.Save();
      clientList = Client.GetAll();
      Client theClient = clientList[0];

      //Assert
      Assert.Equal(testClient, theClient);

    }

    [Fact]
    public void Test_IfSavesMultipleClients_True()
    {
      Client newFirstClient = new Client("Joe Klein", 0);
      Client newSecondClient = new Client("Ashley Watson", 0);
      List<Client> clientList = new List<Client>{};

      newFirstClient.Save();
      newSecondClient.Save();
      clientList = Client.GetAll();

      Assert.Equal(2, clientList.Count);
    }

    [Fact]
    public void Test_IfReturnsListOfClients_True()
    {
      Client newFirstClient = new Client("Joe Klein", 0);
      Client newSecondClient = new Client("Ashley Watson", 0);
      List<Client> clientList = new List<Client>{newFirstClient, newSecondClient};

      newFirstClient.Save();
      newSecondClient.Save();
      List<Client> savedClients = Client.GetAll();

      Assert.Equal(clientList, savedClients);
    }

    [Fact]
    public void Test_IfDeleteRemovesClientFromList_True()
    {
      Client newClient = new Client("Joe Klein", 0);

      newClient.Save();
      newClient.Delete();
      List<Client> savedClients = Client.GetAll();

      Assert.Equal(0, savedClients.Count);
    }

    [Fact]
    public void Test_IfEditClientWorks_True()
    {

      Client newClient = new Client("Joe Klein", 1);
      Client editClient = new Client("Joe K", 1);

      newClient.Save();
      newClient.Edit("Joe K", 1);
      List<Client> savedEditClient = Client.GetAll();

      Assert.Equal(editClient, savedEditClient[0]);
    }

    [Fact]
    public void Test_IfCanFindByName_True()
    {

      Client newClient = new Client("Joe Klein", 1);
      Client foundClient = new Client("Joe Klein", 1);

      newClient.Save();
      newClient = Client.FindByName(newClient.GetName());

      Assert.Equal(foundClient, newClient);
    }


    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}
