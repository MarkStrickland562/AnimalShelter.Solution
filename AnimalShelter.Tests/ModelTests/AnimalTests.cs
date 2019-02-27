using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using AnimalShelter.Models;

namespace AnimalShelter.Tests
{
  [TestClass]
  public class AnimalTest : IDisposable
  {
    public void Dispose()
    {
      Animal.ClearAll();
    }

    public AnimalTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=animalshelter_test;";
    }

    [TestMethod]
    public void AnimalConstructor_CreatesInstanceOfAnimal_Animal()
    {
      //Arrange, Act
      string name = "Spot";
      string sex = "Male";
      DateTime dateOfAdmittance = new DateTime(2019, 01, 01);
      string breed = "German Shepherd";
      string type = "Dog";
      Animal newAnimal = new Animal(name, sex, dateOfAdmittance, breed, type);

      //Assert
      Assert.AreEqual(typeof(Animal), newAnimal.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      //Arrange
      string name = "Spot";
      string sex = "Male";
      DateTime dateOfAdmittance = new DateTime(2019, 01, 01);
      string breed = "German Shepherd";
      string type = "Dog";
      Animal newAnimal = new Animal(name, sex, dateOfAdmittance, breed, type);

      //Act
      string result = newAnimal.GetName();

      //Assert
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void SetName_SetName_String()
    {
      //Arrange
      string name = "Spot";
      string sex = "Male";
      DateTime dateOfAdmittance = new DateTime(2019, 01, 01);
      string breed = "German Shepherd";
      string type = "Dog";
      Animal newAnimal = new Animal(name, sex, dateOfAdmittance, breed, type);

      //Act
      string updatedName = "Russell";
      newAnimal.SetName(updatedName);
      string result = newAnimal.GetName();

      //Assert
      Assert.AreEqual(updatedName, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyListFromDatabase_AnimalList()
    {
      // Arrange
      List<Animal> newList = new List<Animal> { };

      // Act
      List<Animal> result = Animal.GetAll("default");

      // Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsAnimals_AnimalList()
    {
      //Arrange
      string name1 = "Spot";
      string sex1 = "Male";
      DateTime dateOfAdmittance1 = new DateTime(2019, 01, 01);
      string breed1 = "German Shepherd";
      string type1 = "Dog";
      Animal newAnimal1 = new Animal(name1, sex1, dateOfAdmittance1, breed1, type1);
      newAnimal1.Save();

      string name2 = "Jetson";
      string sex2 = "Male";
      DateTime dateOfAdmittance2 = new DateTime(2018, 01, 01);
      string breed2 = "Calico";
      string type2 = "Cat";
      Animal newAnimal2 = new Animal(name2, sex2, dateOfAdmittance2, breed2, type2);
      newAnimal2.Save();

      List<Animal> newList = new List<Animal> { newAnimal1, newAnimal2 };

      //Act
      List<Animal> result = Animal.GetAll("default");

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    // [TestMethod]
    // public void GetId_AnimalsInstantiateWithAnIdAndGetterReturns_Int()
    // {
    //   //Arrange
    //   string description = "Walk the dog.";
    //   Animal newAnimal = new Animal(description);
    //
    //   //Act
    //   int result = newAnimal.GetId();
    //
    //   //Assert
    //   Assert.AreEqual(1, result);
    // }

    [TestMethod]
    public void Find_ReturnsCorrectAnimalFromDatabase_Animal()
    {
      //Arrange
      string name = "Spot";
      string sex = "Male";
      DateTime dateOfAdmittance = new DateTime(2019, 01, 01);
      string breed = "German Shepherd";
      string type = "Dog";
      Animal newAnimal = new Animal(name, sex, dateOfAdmittance, breed, type);
      newAnimal.Save();
    
      //Act
      Animal foundAnimal = Animal.Find(newAnimal.GetName());

      //Assert
      Assert.AreEqual(newAnimal, foundAnimal);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfSame_Animal()
    {
      // Arrange, Act
      string name1 = "Spot";
      string sex1 = "Male";
      DateTime dateOfAdmittance1 = new DateTime(2019, 01, 01);
      string breed1 = "German Shepherd";
      string type1 = "Dog";
      Animal newAnimal1 = new Animal(name1, sex1, dateOfAdmittance1, breed1, type1);
      newAnimal1.Save();

      string name2 = "Spot";
      string sex2 = "Male";
      DateTime dateOfAdmittance2 = new DateTime(2019, 01, 01);
      string breed2 = "German Shepherd";
      string type2 = "Dog";
      Animal newAnimal2 = new Animal(name2, sex2, dateOfAdmittance2, breed2, type2);
      newAnimal2.Save();

      // Assert
      Assert.AreEqual(newAnimal1, newAnimal2);
    }

    [TestMethod]
    public void Save_SavesToDatabase_AnimalList()
    {
      //Arrange
      string name1 = "Spot";
      string sex1 = "Male";
      DateTime dateOfAdmittance1 = new DateTime(2019, 01, 01);
      string breed1 = "German Shepherd";
      string type1 = "Dog";
      Animal newAnimal1 = new Animal(name1, sex1, dateOfAdmittance1, breed1, type1);

      //Act
      newAnimal1.Save();
      List<Animal> result = Animal.GetAll("default");
      List<Animal> testList = new List<Animal>{newAnimal1};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    // [TestMethod]
    // public void Save_AssignsIdToObject_Id()
    // {
    //   //Arrange
    //   Animal testAnimal = new Animal("Mow the lawn");
    //
    //   //Act
    //   testAnimal.Save();
    //   Animal savedAnimal = Animal.GetAll()[0];
    //
    //   int result = savedAnimal.GetId();
    //   int testId = testAnimal.GetId();
    //
    //   //Assert
    //   Assert.AreEqual(testId, result);
    // }

//     [TestMethod]
//     public void Edit_UpdatesAnimalInDatabase_String()
//     {
//       //Arrange
//       string name1 = "Spot";
//       string sex1 = "Male";
//       DateTime dateOfAdmittance1 = "01/01/2019";
//       string breed1 = "German Shepherd";
//       string type1 = "Dog";
//       Animal newAnimal1 = new Animal(name1, sex1, dateOfAdmittance1, breed1, type1);
//
//       string name2 = "Rex";
//
//       //Act
//       newAnimal1.Save();
//       newAnimal1.Edit(name2);
//       string result = Animal.Find(testAnimal.GetId()).GetName();
//
//       //Assert
//       Assert.AreEqual(name2, result);
//     }
  }
}
