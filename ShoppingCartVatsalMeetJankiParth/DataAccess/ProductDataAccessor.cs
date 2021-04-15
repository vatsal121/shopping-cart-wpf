
using ShoppingCartVatsalMeetJankiParth.Entities;
using ShoppingCartVatsalMeetJankiParth.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ShoppingCartVatsalMeetJankiParth.DataAccess
{
    public class ProductDataAccessor
    {
        private static readonly string SelectCommandCore =
            "SELECT TOP(1000) p.Id, "
            + "p.ProductName, p.CategoryId, p.Price, p.Description, p.Quantity, p.DateCreated, p.DateModified, "
            + "c.Id, c.CategoryName, c.DateCreated, c.DateModified "
            + "FROM dbo.Products p "
            + "LEFT JOIN dbo.Category c on c.Id=p.CategoryId ";


        private static string ConnectionString
            => ConfigurationManager.ConnectionStrings["ShoppingDB_VMJP"]?.ConnectionString;

        public Product Get(int id)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = SelectCommandCore + "WHERE p.Id = @Id ";
            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            SqlDataReader reader = command.ExecuteReader();
            Product product = null;

            if (reader.Read())
            {
                product = ExtractNextData(reader);
            }

            return product;
        }

        public IList<Product> GetAll()
        {
            return GetAllCore(null, null);
        }

        private IList<Product> GetAllCore(string whereClause, string orderByClause, params SqlParameter[] parameters)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = SelectCommandCore + whereClause + orderByClause;
            command.Parameters.AddRange(parameters);

            SqlDataReader reader = command.ExecuteReader();
            List<Product> products = new List<Product>();

            while (reader.Read())
            {
                products.Add(ExtractNextData(reader));
            }

            return products;
        }

        private Product ExtractNextData(SqlDataReader reader)
        {
            return new Product(id: reader.GetInt32(0),
                                productName: reader.GetString(1),
                                categoryId: reader.GetInt32(2),
                                price: reader.GetDecimal(3),
                                description: reader.GetString(4),
                                quantity: reader.GetInt32(5),
                                dateCreated: reader.GetDateTime(6),
                                dateModified: (reader.IsDBNull(7)) ? (DateTime?)null : reader.GetDateTime(7),
                                category: new Category()
                                {
                                    Id = reader.GetInt32(8),
                                    CategoryName = reader.GetString(9),
                                    DateCreated = reader.GetDateTime(10),
                                    DateModified = (reader.IsDBNull(11)) ? (DateTime?)null : reader.GetDateTime(11)

                                }
                                );
        }

        public void Add(Product product)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText =
                "INSERT INTO dbo.Products "
                + "(ProductName, "
                + "CategoryId, Price, Description, Quantity, "
                + "DateCreated, DateModified) "
                + "OUTPUT INSERTED.Id "
                + "VALUES(@ProductName, "
                + "@CategoryId, @Price, @Description, @Quantity, "
                + "@DateCreated, @DateModified) ";

            product.DateCreated = (DateTime)(product.DateModified = DateTime.UtcNow);
            command.Parameters.Add("@DateCreated", SqlDbType.DateTime2).Value = product.DateCreated;
            command.Parameters.Add("@DateModified", SqlDbType.DateTime2).Value = product.DateModified;

            command.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = product.ProductName;
            command.Parameters.Add("@CategoryId", SqlDbType.Int).Value = product.CategoryId;
            command.Parameters.Add("@Price", SqlDbType.Decimal).Value = product.Price;
            command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = product.Description;
            command.Parameters.Add("@Quantity", SqlDbType.Int).Value = product.Quantity;


            product.Id = (int)command.ExecuteScalar();

        }

        public void AddToCart(Cart cart)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText =
                "INSERT INTO dbo.Cart "
                + "(UserId, "
                + "ProductId, DiscountPercentage, IsCustomerCheckedOut, QtyOrdered, "
                + "FinalProductPrice, DateCreated, DateModified) "
                + "OUTPUT INSERTED.Id "
                + "VALUES(@UserId, "
                + "@ProductId, @DiscountPercentage, @IsCustomerCheckedOut, @QtyOrdered, "
                + "@FinalProductPrice, @DateCreated, @DateModified) ";

            cart.DateCreated = (DateTime)(cart.DateModified = DateTime.UtcNow);
            command.Parameters.Add("@DateCreated", SqlDbType.DateTime2).Value = cart.DateCreated;
            command.Parameters.Add("@DateModified", SqlDbType.DateTime2).Value = cart.DateModified;

            command.Parameters.Add("@UserId", SqlDbType.Int).Value = cart.UserId;
            command.Parameters.Add("@ProductId", SqlDbType.Int).Value = cart.ProductId;
            command.Parameters.Add("@DiscountPercentage", SqlDbType.Decimal).Value = cart.DiscountPercentage;
            command.Parameters.Add("@IsCustomerCheckedOut", SqlDbType.Bit).Value = cart.IsCustomerCheckedOut;
            command.Parameters.Add("@QtyOrdered", SqlDbType.Int).Value = cart.QtyOrdered;
            command.Parameters.Add("@FinalProductPrice", SqlDbType.Decimal).Value = cart.FinalProductPrice;

            cart.Id = (int)command.ExecuteScalar();

        }

        public bool Remove(Product product)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "DELETE from dbo.Products WHERE Id = @Id ";
            command.Parameters.Add("@Id", SqlDbType.Int).Value = product.Id;

            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }

        public bool Update(Product product)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText =
                "UPDATE dbo.Products "
                + "SET DateModified = @DateModified, "
                + "ProductName = @ProductName, "
                + "CategoryId = @CategoryId, Price = @Price, "
                + "Description = @Description, Quantity = @Quantity "
                + "WHERE Id = @Id ";

            product.DateModified = DateTime.UtcNow;
            command.Parameters.Add("@DateModified", SqlDbType.DateTime2).Value = product.DateModified;

            command.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = product.ProductName;
            command.Parameters.Add("@CategoryId", SqlDbType.Int).Value = product.CategoryId;
            command.Parameters.Add("@Price", SqlDbType.Decimal).Value = product.Price;
            command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = product.Description;
            command.Parameters.Add("@Quantity", SqlDbType.Int).Value = product.Quantity;
            command.Parameters.Add("@Id", SqlDbType.Int).Value = product.Id;

            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }
    }
}
