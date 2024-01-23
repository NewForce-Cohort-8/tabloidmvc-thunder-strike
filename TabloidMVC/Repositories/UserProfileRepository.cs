using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;
using TabloidMVC.Utils;

namespace TabloidMVC.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration config) : base(config) { }
        public List<UserProfile> GetAllUserProfiles()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT u.Id, u.FirstName, u.LastName, u.DisplayName, u.Email,
                       u.CreateDateTime, u.ImageLocation, u.UserTypeId,
                       ut.[Name] AS UserTypeName
                FROM UserProfile u
                LEFT JOIN UserType ut ON u.UserTypeId = ut.Id
                ORDER BY u.DisplayName ASC";
                    var reader = cmd.ExecuteReader();

                    var UserProfiles = new List<UserProfile>();

                    while (reader.Read())
                    {
                        UserProfiles.Add(NewUserProfileFromReader(reader));
                    }

                    reader.Close();

                    return UserProfiles;
                }
            }
        }
        private UserProfile NewUserProfileFromReader(SqlDataReader reader)
        {
            return new UserProfile()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                ImageLocation = DbUtils.GetNullableString(reader, "ImageLocation"),
                UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                UserType = new UserType()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                    Name = reader.GetString(reader.GetOrdinal("UserTypeName"))
                },
            };
        }

        public UserProfile GetByEmail(string email)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT u.id, u.FirstName, u.LastName, u.DisplayName, u.Email,
                              u.CreateDateTime, u.ImageLocation, u.UserTypeId,
                              ut.[Name] AS UserTypeName
                         FROM UserProfile u
                              LEFT JOIN UserType ut ON u.UserTypeId = ut.id
                        WHERE email = @email";
                    cmd.Parameters.AddWithValue("@email", email);

                    UserProfile userProfile = null;
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        userProfile = new UserProfile()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            ImageLocation = DbUtils.GetNullableString(reader, "ImageLocation"),
                            UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                            UserType = new UserType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                Name = reader.GetString(reader.GetOrdinal("UserTypeName"))
                            },
                        };
                    }

                    reader.Close();

                    return userProfile;
                }
            }
        }
    }
}