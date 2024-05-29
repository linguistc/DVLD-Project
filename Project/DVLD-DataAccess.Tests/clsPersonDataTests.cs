using System.ComponentModel.DataAnnotations;

namespace DVLD_DataAccess.Tests
{
    public class clsPersonDataTests
    {
        [Fact]
        public void IsPersonExist_ForPersonIDExists_ReturnsTrue()
        {
            // Arrange
            int personID = 1;

            // Act
            bool isPersonExist = clsPersonData.IsPersonExist(personID);

            // Assert
            Assert.True(isPersonExist );

        }


        [Fact]
        public void IsPersonExist_ForPersonIDNotExist_ReturnsFalse()
        {
            // Arrange
            int personID = 800;

            // Act
            bool isPersonExist = clsPersonData.IsPersonExist(personID);

            // Assert
            Assert.False(isPersonExist);


        }

        [Fact]
        public void IsPersonExist_ForPersonNationalNumberExists_ReturnsTrue()
        {
            // Arrange
            string nationalNo = "N1";

            // Act
            bool isPersonExist = clsPersonData.IsPersonExist(nationalNo);

            // Assert
            Assert.True(isPersonExist);

        }


        [Fact]
        public void IsPersonExist_ForPersonNationalNumberNotExist_ReturnsFalse()
        {
            // Arrange
            string nationalNo = "N800";
            // Act
            bool isPersonExist = clsPersonData.IsPersonExist(nationalNo);

            // Assert
            Assert.False(isPersonExist);


        }

        [Fact]
        public void AddNewPerson_ForSuccessfullInsertion_ReturnsInsertedID()
        {
            // Arrange
            string firstName = "noor"; 
            string secondName = "raafat";
            string thirdName = "";
            string lastName = "yousef";

            string nationalNo = "N41"; // Valid National Number
             
            DateTime dateOfBirth = new DateTime(1987, 12, 9);
            short gender = 0;
            string address = "Suadi Arabia";
            string phone = "021232";
            string email = null;
            int nationalityCountryID = 90;
            string imagePath = null;

            // Act
            int insertedID = clsPersonData.AddNewPerson(firstName, secondName, thirdName, lastName, 
                nationalNo, dateOfBirth, gender, address, phone, email, nationalityCountryID, imagePath);

            // Assert
            Assert.True(insertedID > 0);
        }


        [Fact]
        public void AddNewPerson_ForInvalidNationalNumber_ReturnsMinusOne()
        {
            // Arrange
            string firstName = "Khaled";
            string secondName = "Mohamed";
            string thirdName = "";
            string lastName = "Al Shereef";

            string nationalNo = "N1"; // InValid National Number (Repeated)

            DateTime dateOfBirth = new DateTime(1997, 12, 9);
            short gender = 0;
            string address = "Suadi Arabia";
            string phone = "00191232";
            string email = null;
            int nationalityCountryID = 90;
            string imagePath = null;

            // Act
            int insertedID = clsPersonData.AddNewPerson(firstName, secondName, thirdName, lastName,
                nationalNo, dateOfBirth, gender, address, phone, email, nationalityCountryID, imagePath);

            // Assert
            Assert.Equal(-1, insertedID);
        }

        [Fact]
        public void AddNewPerson_ForNullValueInFieldsNotAcceptingNull_ReturnsMinusOne()
        {
            // Arrange
            string firstName = null; // not accepting null
            string secondName = "Ahmed"; // also not accepting null
            string thirdName = "";
            string lastName = "Yousef"; // also not accepting null

            string nationalNo = "N20"; // Valid National Number   // also not accepting null

            DateTime dateOfBirth = new DateTime(1997, 12, 9); // also not accepting null
            short gender = 0; // also not accepting null
            string address = "Suadi Arabia"; // also not accepting null
            string phone = "00191232"; // also not accepting null
            string email = null;
            int nationalityCountryID = 90; // also not accepting null
            string imagePath = null;

            // Act
            int insertedID = clsPersonData.AddNewPerson(firstName, secondName, thirdName, lastName,
                nationalNo, dateOfBirth, gender, address, phone, email, nationalityCountryID, imagePath);

            // Assert
            Assert.Equal(-1, insertedID);
        }


        [Fact]
        public void UpdatePerson_ForSuccessfullUpdating_ReturnsTrue()
        {
            // Arrange
            int personID = 1027;
            string firstName = "Mostafa";
            string secondName = "Mohamed";
            string thirdName = "";
            string lastName = "Al Shereef";

            string nationalNo = "N46"; // Valid National Number

            DateTime dateOfBirth = new DateTime(1997, 12, 9);
            short gender = 0;
            string address = "Suadi Arabia";
            string phone = "019125643";
            string email = null;
            int nationalityCountryID = 90;
            string imagePath = null;

            // Act
            bool updated = clsPersonData.UpdatePerson(personID ,firstName, secondName, thirdName, lastName,
                nationalNo, dateOfBirth, gender, address, phone, email, nationalityCountryID, imagePath);

            // Assert
            Assert.True(updated);
        }


        [Fact]
        public void UpdatePerson_ForUpdateFailed_ReturnsFalse()
        {
            // Arrange
            int personID = 2000;  // this ID is not exist in the DB
            string firstName = "Khaled";
            string secondName = "Mohamed";
            string thirdName = "";
            string lastName = "Al Shereef";

            string nationalNo = "N1"; // InValid National Number (Repeated)

            DateTime dateOfBirth = new DateTime(1997, 12, 9);
            short gender = 0;
            string address = "Suadi Arabia";
            string phone = "00191232";
            string email = null;
            int nationalityCountryID = 90;
            string imagePath = null;

            // Act
            bool updated = clsPersonData.UpdatePerson(personID, firstName, secondName, thirdName, lastName,
                nationalNo, dateOfBirth, gender, address, phone, email, nationalityCountryID, imagePath);

            // Assert
            Assert.False(updated);
        }

        [Fact]
        public void DeletePerson_ForDeletionFailed_ReturnsFalse()
        {
            // Arrange
            int personID = 3000; // ID not exists in DB

            // Act
            bool deleted = clsPersonData.DeletePerson(personID);

            // Assert
            Assert.False(deleted);
        }

        

    }
}