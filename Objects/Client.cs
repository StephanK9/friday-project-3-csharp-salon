using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace HairSalon.Objects
{
  public class Client
  {
    private int _id;
    private string _name;
    private string _detail;

    public Client(string Name, string Detail, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _detail = Detail;
    }

    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool nameEquality = (this.GetName() == newClient.GetName());
        bool detailEquality = (this.GetDetail() == newClient.GetDetail());
        bool idEquality = (this.GetId() == newClient.GetId());

        return (nameEquality && detailEquality && idEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetDetail()
    {
      return _detail;
    }

    public void SetDetail(string newDetail)
    {
      _detail = newDetail;
    }
    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM Clients;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        string clientDetail = rdr.GetString(1);
        Client newClient = new Client(clientName, clientDetail, clientId);
        allClients.Add(newClient);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allClients;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO Clients (name) OUTPUT INSERTED.id VALUES (@ClientName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@ClientName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM Clients;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
