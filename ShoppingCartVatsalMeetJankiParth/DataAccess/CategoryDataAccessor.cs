using ShoppingCartVatsalMeetJankiParth.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ShoppingCartVatsalMeetJankiParth.DataAccess
{
    public class CategoryDataAccessor
    {
        private static readonly string SelectCommandCore =
              "SELECT  Id, CategoryName, DateCreated, DateModified "
              + "FROM dbo.Category ";


        private static string ConnectionString
            => ConfigurationManager.ConnectionStrings["ShoppingDB_VMJP"]?.ConnectionString;

        public Category Get(int id)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = SelectCommandCore + "WHERE Id = @Id ";
            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;

            SqlDataReader reader = command.ExecuteReader();
            Category category = null;

            if (reader.Read())
            {
                category = ExtractNextData(reader);
            }

            return category;
        }

        public IList<Category> GetAll()
        {
            return GetAllCore(null, null);
        }

        public IList<Category> GetAll(string whereClause, string orderByClause, params SqlParameter[] parameters)
        {
            return GetAllCore(whereClause, orderByClause, parameters);
        }

        private IList<Category> GetAllCore(string whereClause, string orderByClause, params SqlParameter[] parameters)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = SelectCommandCore + whereClause + orderByClause;
            command.Parameters.AddRange(parameters);

            SqlDataReader reader = command.ExecuteReader();
            List<Category> products = new List<Category>();

            while (reader.Read())
            {
                products.Add(ExtractNextData(reader));
            }

            return products;
        }

        public bool UpdateCategory(Category category)
        {

            using SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText =
                "UPDATE dbo.Category"
                + "SET DateModified = @DateModified, "
                + "CategoryName= @CategoryName "
                + "WHERE Id = @Id ";

            category.DateModified = DateTime.UtcNow;
            command.Parameters.Add("@DateModified", SqlDbType.DateTime2).Value = category.DateModified;

            command.Parameters.Add("@CategoryName", SqlDbType.NVarChar).Value = category.CategoryName;
            command.Parameters.Add("@Id", SqlDbType.Int).Value = category.Id;

            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }

        private Category ExtractNextData(SqlDataReader reader)
        {
            Category cat = new Category()
            {
                Id = reader.GetInt32(0),
                CategoryName = reader.GetString(1),
                DateCreated = reader.GetDateTime(2),
                DateModified = (reader.IsDBNull(3)) ? (DateTime?)null : reader.GetDateTime(3),
            };
            return cat;
        }


        public bool Remove(Category category)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "DELETE from dbo.Category WHERE Id = @Id ";
            command.Parameters.Add("@Id", SqlDbType.Int).Value = category.Id;

            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }

    }
}
