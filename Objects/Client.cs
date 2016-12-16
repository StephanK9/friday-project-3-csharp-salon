using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace HairSalon.Objects
{
  public class Client
  {
    private int _id;
    private string _name;
    private int _stylist_id;

    public Client(string name, int stylistId, int id = 0)
    {
      _id = id;
      _name = name;
      _stylist_id = stylistId;
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
        bool stylistIdEquality = (this.GetStylistId() == newClient.GetStylistId());

        return (nameEquality && stylistIdEquality);
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

    public int GetStylistId()
    {
      return _stylist_id;
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
        int stylistId = rdr.GetInt32(2);
        Client newClient = new Client(clientName, stylistId, clientId);
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

      SqlCommand cmd = new SqlCommand("INSERT INTO clients (name, stylist_id) OUTPUT INSERTED.id VALUES (@ClientName, @ClientStylistId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@ClientName";
      nameParameter.Value = this.GetName();

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@ClientStylistId";
      stylistIdParameter.Value = this.GetStylistId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(stylistIdParameter);

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

    public void Edit(string name, int stylistId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("UPDATE clients SET name = @ClientName, stylist_id = @ClientStylistId WHERE id = @ClientId;", conn);

      SqlParameter clientIdParameter = new SqlParameter();
      clientIdParameter.ParameterName = "@ClientId";
      clientIdParameter.Value = this.GetId();

      SqlParameter clientNameParameter = new SqlParameter();
      clientNameParameter.ParameterName = "@ClientName";
      clientNameParameter.Value = name;

      SqlParameter clientStylistIdParameter = new SqlParameter();
      clientStylistIdParameter.ParameterName = "@ClientStylistId";
      clientStylistIdParameter.Value = stylistId;

      cmd.Parameters.Add(clientIdParameter);
      cmd.Parameters.Add(clientNameParameter);
      cmd.Parameters.Add(clientStylistIdParameter);

      cmd.ExecuteNonQuery();

      conn.Close();
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id = @ClientId;", conn);

      SqlParameter clientIdParameter = new SqlParameter();
      clientIdParameter.ParameterName = "@ClientId";
      clientIdParameter.Value = this.GetId();

      cmd.Parameters.Add(clientIdParameter);

      cmd.ExecuteNonQuery();

      conn.Close();
    }

    public static Client FindByName(string name)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE name = @ClientName;", conn);

      SqlParameter clientNameParameter = new SqlParameter();
      clientNameParameter.ParameterName = "@ClientName";
      clientNameParameter.Value = name;
      cmd.Parameters.Add(clientNameParameter);

      SqlDataReader rdr = cmd.ExecuteReader ();
      int clientId = 0;
      int stylistId = 0;

      while(rdr.Read())
      {
        clientId = rdr.GetInt32(0);
        stylistId = rdr.GetInt32(2);
      }
      Client locatedClient = new Client(name, stylistId, clientId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return locatedClient;
    }

    public static Client FindById(int clientId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @ClientId;", conn);

      SqlParameter clientIdParameter = new SqlParameter();
      clientIdParameter.ParameterName = "@ClientId";
      clientIdParameter.Value = clientId.ToString();
      cmd.Parameters.Add(clientIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();
      int foundClientId = 0;
      string foundClientName = null;
      int foundStylistId = 0;

      while(rdr.Read())
      {
        foundClientId = rdr.GetInt32(0);
        foundClientName = rdr.GetString(1);
        foundStylistId = rdr.GetInt32(2);
      }
      Client foundClient = new Client(foundClientName, foundStylistId, foundClientId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundClient;
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
