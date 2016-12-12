using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HairSalon.Objects
{
  public class Stylist
  {
    public int _id;
    public string _name;

    public Stylist(string name, int id = 0)
    {
      _id = id;
      _name = name;
    }

    public override bool Equals(System.Object otherStylist)
      {
        if (!(otherStylist is Stylist))
        {
          return false;
        }
        else
        {
          Stylist newStylist = (Stylist) otherStylist;
          bool idEquality = this.GetId() == newStylist.GetId();
          bool nameEquality = (this.GetName() == newStylist.GetName());
          return (idEquality && nameEquality);
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

      public static List<Stylist> GetAll()
    {
      List<Stylist> allStylistList = new List<Stylist>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        Stylist newStylist = new Stylist(stylistName, stylistId);
        allStylistList.Add(newStylist);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allStylistList;
    }

      public void Save()
      {
       SqlConnection conn = DB.Connection();
       conn.Open();

       SqlCommand cmd = new SqlCommand("INSERT INTO stylists(name) OUTPUT INSERTED.id VALUES (@StylistName);", conn);

       SqlParameter nameParameter = new SqlParameter();
       nameParameter.ParameterName = "@StylistName";
       nameParameter.Value = this.GetName();

       cmd.Parameters.Add(nameParameter);

       SqlDataReader rdr = cmd.ExecuteReader();

       while(rdr.Read())
       {
         this._id = rdr.GetInt32(0);
       }
       if(rdr != null)
       {
         rdr.Close();
       }
       if(conn != null)
       {
         conn.Close();
       }
     }

     public List<Client> GetClients()
     {
       List<Client> clientList = new List<Client>{};

       SqlConnection conn = DB.Connection();
       conn.Open();

       SqlCommand cmd = new SqlCommand("Select * FROM clients WHERE stylist_id = @StylistId;", conn);
       SqlParameter stylistIdParameter = new SqlParameter();
       stylistIdParameter.ParameterName = "@StylistId";
       stylistIdParameter.Value = this.GetId();

       cmd.Parameters.Add(stylistIdParameter);

       SqlDataReader rdr = cmd.ExecuteReader();

       while(rdr.Read())
       {
         int clientId = rdr.GetInt32(0);
         string clientName = rdr.GetString(1);
         int clientStylistId = rdr.GetInt32(2);
         Client newClient = new Client(clientName, clientStylistId, clientId);
         clientList.Add(newClient);
       }

       if (rdr != null)
       {
         rdr.Close();
       }
       if (conn != null)
       {
         conn.Close();
       }
       return clientList;
     }

     public static Stylist Find(int stylistId)
     {
       SqlConnection conn = DB.Connection();
       conn.Open();

       SqlCommand cmd = new SqlCommand("Select * FROM stylists WHERE id=@StylistId;", conn);

       SqlParameter stylistIdParameter = new SqlParameter();
       stylistIdParameter.ParameterName = "@StylistId";
       stylistIdParameter.Value = stylistId.ToString();
       cmd.Parameters.Add(stylistIdParameter);
       SqlDataReader rdr = cmd.ExecuteReader();

       int foundStylistId = 0;
       string foundStylistName = null;


       while(rdr.Read())
       {
         foundStylistId = rdr.GetInt32(0);
         foundStylistName = rdr.GetString(1);
       }
       Stylist foundStylist = new Stylist(foundStylistName, foundStylistId);

       if(rdr != null)
       {
         rdr.Close();
       }
       if(conn != null)
       {
         conn.Close();
       }
       return foundStylist;
     }

     public static void DeleteAll()
     {
       SqlConnection conn = DB.Connection();
       conn.Open();
       SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
       cmd.ExecuteNonQuery();
       conn.Close();
     }
   }
 }
