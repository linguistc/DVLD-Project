using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;

namespace DVLD_DataAccess
{
    public class clsPersonData
    {
        public static bool GetPersonInfoByID (int PersonID, ref string FirstName, ref string SecondName,
          ref string ThirdName, ref string LastName, ref string NationalNo, ref DateTime DateOfBirth,
           ref short Gender, ref string Address, ref string Phone, ref string Email,
           ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;
            
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select * from People where PersonID = @PersonID";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;

                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];

                    //ThirdName: allows null in database so we should handle null
                    if (reader["ThirdName"] != DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];
                    else
                        ThirdName = "";

                    LastName = (string)reader["LastName"];
                    NationalNo = (string)reader["NationalNo"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = (byte)reader["Gender"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];

                    // Email allows null in database
                    if (reader["Email"] != DBNull.Value)
                        Email = (string)reader["Email"];
                    else
                        Email = "";

                    NationalityCountryID = (int)reader["NationalityCountryID"];

                    //ImagePath allows null in database
                    if (reader["ImagePath"] != DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];
                    else
                        ImagePath = "";

                }
                else
                {
                    isFound = false;
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally { conn.Close(); }


            return isFound;
        }


        public static bool GetPersonInfoByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, ref string SecondName,
        ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
         ref short Gender, ref string Address, ref string Phone, ref string Email,
         ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from people where NationalNo = @NationalNo";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];

                    //ThirdName allows null in database
                    if (reader["ThirdName"] != DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];
                    else
                        ThirdName = "";

                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = (byte)reader["Gender"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];

                    // Email allows null in database
                    if (reader["Email"] != DBNull.Value)
                        Email = (string)reader["Email"];
                    else
                        Email = "";

                    NationalityCountryID = (int)reader["NationalityCountryID"];

                    //ImagePath allows null in database
                    if (reader["ImagePath"] != DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];
                    else
                        ImagePath = "";

                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally { conn.Close(); }


            return isFound;
        }


        public static int AddNewPerson(string FirstName, string SecondName,
           string ThirdName, string LastName, string NationalNo, DateTime DateOfBirth,
           short Gender, string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)
        {
            int PersonID = -1;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO People (FirstName, SecondName, ThirdName,LastName,NationalNo,
                                                   DateOfBirth, Gender ,Address ,Phone , 
                                                    Email, NationalityCountryID, ImagePath)
                            VALUES (@FirstName, @SecondName,@ThirdName, @LastName, @NationalNo,
                                     @DateOfBirth,@Gender,@Address,@Phone, @Email,@NationalityCountryID,@ImagePath);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@SecondName", SecondName);
            if (ThirdName != null && ThirdName != "")
                cmd.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                cmd.Parameters.AddWithValue("@ThirdNmae", System.DBNull.Value);

            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@Gender", Gender);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Phone", Phone);

            if (Email != "" && Email != null)
                cmd.Parameters.AddWithValue("@Email", Email);
            else
                cmd.Parameters.AddWithValue("@Email", System.DBNull.Value);

            cmd.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != "" && ImagePath != null)
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                cmd.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                conn.Open();

                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PersonID = insertedID;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }
            finally { conn.Close(); }


            return PersonID;
        }


        public static bool UpdatePerson(int PersonID, string FirstName, string SecondName,
           string ThirdName, string LastName, string NationalNo, DateTime DateOfBirth,
           short Gender, string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)
        {
            int rowsAffected = 0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  People  
                            set FirstName = @FirstName,
                                SecondName = @SecondName,
                                ThirdName = @ThirdName,
                                LastName = @LastName, 
                                NationalNo = @NationalNo,
                                DateOfBirth = @DateOfBirth,
                                Gender=@Gender,
                                Address = @Address,  
                                Phone = @Phone,
                                Email = @Email, 
                                NationalityCountryID = @NationalityCountryID,
                                ImagePath =@ImagePath
                                where PersonID = @PersonID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@SecondName", SecondName);
            if (ThirdName != null && ThirdName != "")
                cmd.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                cmd.Parameters.AddWithValue("@ThirdNmae", System.DBNull.Value);

            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@Gender", Gender);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Phone", Phone);

            if (Email != "" && Email != null)
                cmd.Parameters.AddWithValue("@Email", Email);
            else
                cmd.Parameters.AddWithValue("@Email", System.DBNull.Value);

            cmd.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != "" && ImagePath != null)
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                cmd.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally { conn.Close(); }

            return rowsAffected > 0;
        }



        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();

            string query =
              @"SELECT People.PersonID, People.NationalNo,
              People.FirstName, People.SecondName, People.ThirdName, People.LastName,
			  People.DateOfBirth, People.Gendor,  
				  CASE
                  WHEN People.Gender = 0 THEN 'Male'

                  ELSE 'Female'

                  END as GenderCaption ,
			  People.Address, People.Phone, People.Email, 
              People.NationalityCountryID, Countries.CountryName, People.ImagePath
              FROM            People INNER JOIN
                         Countries ON People.NationalityCountryID = Countries.CountryID
                ORDER BY People.FirstName";

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, conn);


            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    dt.Load(reader);

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }
            finally { conn.Close(); }
            return dt;
        }

        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Delete People Where PersonID = @PersonID;";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }
            finally { conn.Close(); }

            return rowsAffected > 0;
        }

        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select found = 1 from People where PersonID = @PersonID;";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
                //Console.WriteLine("Error: " + ex.Message);
            }
            finally { conn.Close(); }

            return isFound;

        }


        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select found = 1 from People where NationalNo = @NationalNo;";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
                //Console.WriteLine("Error: " + ex.Message);
            }
            finally { conn.Close(); }

            return isFound;

        }




    }
}
