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
     public void DeleteAll()
     {
       SqlConnection conn = DB.Connection();
       conn.Open();
       SqlCommand cmd = new SqlCommand("DELTE FROM stylists;", conn);
       cmd.ExecuteNonQuery();
       conn.Close();
     }
   }
 }
