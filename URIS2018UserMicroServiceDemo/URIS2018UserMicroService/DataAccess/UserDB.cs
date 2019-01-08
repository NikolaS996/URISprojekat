using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using URIS2018UserMicroServiceDemo.Models;
using URIS2018UtilDemo.DataAccess;

namespace URIS2018UserMicroServiceDemo.DataAccess
{
	//Staticka klasa koja sadrzi potrebnu logiku za obradu odredjenih zahteva
	public static class UserDB
	{
		private static string AllColumnSelect
		{
			get
			{
				return @"
                    [User].[Id],
	                [User].[Name],
	                [User].[Email],
                    [User].[Address],
                    [User].[ZipCode],
                    [User].[CityName],
                    [User].[CountryName],
                    [User].[Phone],
                    [User].[UserTypeId],
	                [User].[Active]
                ";
			}
		}

		//Metoda koja vrsi mapiranje podataka koji su vraceni iz baze na model
		private static User ReadRow(SqlDataReader reader)
		{
			User retVal = new User();

			retVal.Id = (int)reader["Id"];
			retVal.Name = reader["Name"] as string;
			retVal.Email = reader["Email"] as string;
			retVal.Phone = reader["Phone"] as string;
			retVal.UserTypeId = (int)reader["UserTypeId"];
			retVal.Active = (bool)reader["Active"];

			return retVal;
		}

		//Metoda koja obradjuje zahtev za vracanje korisnika iz baze po ID-ju
		public static User GetUserById(int userId) //Ulazni parametar je ID koji je prosledjen u samoj ruti
		{
			try
			{
				//Kreiranje praznog modela
				User retVal = new User();

				//Kreiranje konekcije na osnovu connection string-a iz Web.config-a
				using (SqlConnection connection = new SqlConnection(DBFunctions.ConnectionString))
				{
					//Kreiranje SQL komande nad datom konekcijom i dodavanje SQL-a koji ce se izvrsiti nad bazom
					SqlCommand command = connection.CreateCommand();
					command.CommandText = String.Format(@"
                        SELECT
                            {0}
                        FROM
                            [User]
                        WHERE
                            [Id] = @Id
                    ", AllColumnSelect);

					//Dodavanje parametara u SQL. Metoda AddParameter se nalazi u Util projektu.
					command.AddParameter("@Id", SqlDbType.Int, userId);

					//Otvaranje konekcije nad bazom
					connection.Open();

					//Izvrsavanje SQL komande i vracanje podataka iz baze
					using (SqlDataReader reader = command.ExecuteReader())
					{
						//Provera da li su podaci vraceni i popunjavanje modela uz pomoc metode ReadRow
						if (reader.Read())
						{
							retVal = ReadRow(reader);
						}
					}
				}
				return retVal; //Vracanje popunjenog modela (ukoliko je trazeni korisnik u bazi)
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

        public static User PostUser(User user)
		{
			try
			{
				int id = 0;
				using (SqlConnection connection = new SqlConnection(DBFunctions.ConnectionString))
				{
					//Kreiranje SQL komande nad datom konekcijom i dodavanje SQL-a koji ce se izvrsiti nad bazom
					SqlCommand command = connection.CreateCommand();
					command.CommandText = String.Format(@"
                        INSERT INTO User(Name, email, UserTypeId, Phone, Address, Active)
						values(@Name, @Email, @UserTypeId, @Phone, @Address, @Active)
						set @Id = SCOPE_IDENTITY();
						select @Id as Id
                    ");

					//Dodavanje parametara u SQL. Metoda AddParameter se nalazi u Util projektu.
					command.AddParameter("@Id", SqlDbType.Int, user.Id);
					command.AddParameter("@Name", SqlDbType.VarChar, user.Name);
					command.AddParameter("@Email", SqlDbType.VarChar, user.Email);
					command.AddParameter("@UserTypeId", SqlDbType.Int, user.UserTypeId);
					command.AddParameter("@Phone", SqlDbType.VarChar, user.Phone);
					command.AddParameter("@Address", SqlDbType.VarChar, user.Addresses);
					command.AddParameter("@Active", SqlDbType.Bit, user.Active);


					//Otvaranje konekcije nad bazom
					connection.Open();

					//Izvrsavanje SQL komande i vracanje podataka iz baze
					using (SqlDataReader reader = command.ExecuteReader())
					{
						//Provera da li su podaci vraceni i popunjavanje modela uz pomoc metode ReadRow
						if (reader.Read())
						{
							id = (int)reader["id"];
						}
					}
				}
				return GetUserById(id);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

        public static User PutUser(User user)
        {
            try
            {
                int id = 0;
                using (SqlConnection connection = new SqlConnection(DBFunctions.ConnectionString))
                {
                    //Kreiranje SQL komande nad datom konekcijom i dodavanje SQL-a koji ce se izvrsiti nad bazom
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = String.Format(@"
                        UPDATE User
                        SET Name = @Name, Email = @Email, UserTypeId = @UserTypeId, Phone = @Phone,
                                   Address = @Address, Active = @Active
                        WHERE Id = @Id");

                    //Dodavanje parametara u SQL. Metoda AddParameter se nalazi u Util projektu.
                    command.AddParameter("@Id", SqlDbType.Int, user.Id);
                    command.AddParameter("@Name", SqlDbType.VarChar, user.Name);
                    command.AddParameter("@Email", SqlDbType.VarChar, user.Email);
                    command.AddParameter("@UserTypeId", SqlDbType.Int, user.UserTypeId);
                    command.AddParameter("@Phone", SqlDbType.VarChar, user.Phone);
                    command.AddParameter("@Address", SqlDbType.VarChar, user.Addresses);
                    command.AddParameter("@Active", SqlDbType.Bit, user.Active);


                    //Otvaranje konekcije nad bazom
                    connection.Open();

                    //Izvrsavanje SQL komande i vracanje podataka iz baze
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //Provera da li su podaci vraceni i popunjavanje modela uz pomoc metode ReadRow
                        if (reader.Read())
                        {
                            id = (int)reader["id"];
                        }
                    }
                }
                return GetUserById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static User GetUser(string email, string password)
        {
            try
            {
                User retVal = null;
                using (SqlConnection connection = new SqlConnection(DBFunctions.ConnectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = String.Format(@"
                    SELECT {0}, Password
                    FROM [User]
                    WHERE [Active] = 'True' AND
                    [Email] = @Email AND
                    [Password] = @Password
                    ", AllColumnSelect);

                    command.AddParameter("@Email", SqlDbType.NVarChar, email);
                    command.AddParameter("@Password", SqlDbType.NVarChar, password);


                    connection.Open();

                    //Izvrsavanje SQL komande i vracanje podataka iz baze
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //Provera da li su podaci vraceni i popunjavanje modela uz pomoc metode ReadRow
                        if (reader.Read())
                        {
                            retVal = ReadRow(reader);
                        }
                    }
                }
                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Metoda koja obradjuje zahtev za brisanje korisnika iz baze po ID-ju
        public static void DeleteUser(int userId) //Ulazni parametar je ID koji je prosledjen u samoj ruti
        {
            try
            {
                //Kreiranje praznog modela
                User retVal = new User();

                //Kreiranje konekcije na osnovu connection string-a iz Web.config-a
                using (SqlConnection connection = new SqlConnection(DBFunctions.ConnectionString))
                {
                    //Kreiranje SQL komande nad datom konekcijom i dodavanje SQL-a koji ce se izvrsiti nad bazom
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = String.Format(@"
                    DELETE FROM User WHERE Id = @Id
                    ");

                    //Dodavanje parametara u SQL. Metoda AddParameter se nalazi u Util projektu.
                    command.AddParameter("@Id", SqlDbType.Int, userId);

                    //Otvaranje konekcije nad bazom
                    connection.Open();

                    //Izvrsavanje SQL komande i vracanje podataka iz baze
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
