using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace AnimalShelter.Models
{
  public class Animal
  {
    private string _name;
    private string _sex;
    private DateTime _dateOfAdmittance;
    private string _breed;
    private string _type;

    public Animal(string AnimalName, string AnimalSex, DateTime AnimalDateOfAdmittance, string AnimalBreed, string AnimalType)
    {
      _name = AnimalName;
      _sex = AnimalSex;
      _dateOfAdmittance = AnimalDateOfAdmittance;
      _breed = AnimalBreed;
      _type = AnimalType;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }

    public string GetSex()
    {
      return _sex;
    }

    public void SetSex(string newSex)
    {
      _sex = newSex;
    }

    public DateTime GetDateOfAdmittance()
    {
      return _dateOfAdmittance;
    }

    public void SetDateOfAdmittance(DateTime newDateOfAdmittance)
    {
      _dateOfAdmittance = newDateOfAdmittance;
    }

    public string GetBreed()
    {
      return _breed;
    }

    public void SetBreed(string newBreed)
    {
      _breed = newBreed;
    }

    public string GetAnimalType()
    {
      return _type;
    }

    public void SetAnimalType(string newType)
    {
      _type = newType;
    }

    public static List<Animal> GetAll(string sortField)
    {
      List<Animal> allAnimals = new List<Animal> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;

      if (sortField == "default")
      {
        cmd.CommandText = @"SELECT * FROM Animals;";
      }
      else
      {
        cmd.CommandText = @"SELECT * FROM Animals ORDER BY " + sortField + ";";
      }

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string AnimalName = rdr.GetString(0);
        string AnimalSex = rdr.GetString(1);
        DateTime AnimalDateOfAdmittance = rdr.GetDateTime(2);
        string AnimalBreed = rdr.GetString(3);
        string AnimalType = rdr.GetString(4);
        Animal newAnimal = new Animal(AnimalName, AnimalSex, AnimalDateOfAdmittance, AnimalBreed, AnimalType);

        allAnimals.Add(newAnimal);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allAnimals;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM animals;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
       conn.Dispose();
      }
    }

    public static Animal Find(string name)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM animals WHERE name = (@searchName);";
      MySqlParameter searchName = new MySqlParameter();
      searchName.ParameterName = "@searchName";
      searchName.Value = name;
      cmd.Parameters.Add(searchName);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      string AnimalName = "";
      string AnimalSex = "";
      DateTime AnimalDateOfAdmittance = new DateTime(1900,1,1);
      string AnimalBreed = "";
      string AnimalType = "";
      while(rdr.Read())
      {
        AnimalName = rdr.GetString(0);
        AnimalSex = rdr.GetString(1);
        AnimalDateOfAdmittance = rdr.GetDateTime(2);
        AnimalBreed = rdr.GetString(3);
        AnimalType = rdr.GetString(4);
      }
      Animal newAnimal = new Animal(AnimalName, AnimalSex, AnimalDateOfAdmittance, AnimalBreed, AnimalType);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newAnimal;
    }

    // public List<Animal> GetAnimals()
    // {
    //   return _Animals;
    // }
    //
    // public void AddAnimal(Animal Animal)
    // {
    //   _countries.Add(Animal);
    // }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO animals (name, sex, date_of_admittance, breed, type) VALUES (@name, @sex, @date_of_admittance, @breed, @type);";
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);
      MySqlParameter sex = new MySqlParameter();
      sex.ParameterName = "@sex";
      sex.Value = this._sex;
      cmd.Parameters.Add(sex);
      MySqlParameter dateOfAdmittance = new MySqlParameter();
      dateOfAdmittance.ParameterName = "@date_of_admittance";
      dateOfAdmittance.Value = this._dateOfAdmittance;
      cmd.Parameters.Add(dateOfAdmittance);
      MySqlParameter breed = new MySqlParameter();
      breed.ParameterName = "@breed";
      breed.Value = this._breed;
      cmd.Parameters.Add(breed);
      MySqlParameter type = new MySqlParameter();
      type.ParameterName = "@type";
      type.Value = this._type;
      cmd.Parameters.Add(type);

      cmd.ExecuteNonQuery();
//    _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherAnimal)
    {
      if (!(otherAnimal is Animal))
      {
        return false;
      }
      else
      {
        Animal newAnimal = (Animal) otherAnimal;
        bool nameEquality = this.GetName() == newAnimal.GetName();
        bool sexEquality = this.GetSex() == newAnimal.GetSex();
        bool dateOfAdmittanceEquality = this.GetDateOfAdmittance() == newAnimal.GetDateOfAdmittance();
        bool breedEquality = this.GetBreed() == newAnimal.GetBreed();
        bool typeEquality = this.GetType() == newAnimal.GetType();

        return (nameEquality && sexEquality && dateOfAdmittanceEquality && breedEquality && typeEquality);
      }
    }
  }
}
