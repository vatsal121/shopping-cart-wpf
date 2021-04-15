using ShoppingCartVatsalMeetJankiParth.Entities;
using ShoppingCartVatsalMeetJankiParth.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ShoppingCartVatsalMeetJankiParth.DataAccess
{
    class CartDataAccessor
    {
        private static readonly string SelectCommandCore =
            "SELECT  c.Id, UserId, ProductId, "
            + "ProductName, CategoryId, CategoryName, DiscountPercentage, IsCustomerCheckedOut, QtyOrdered, FinalProductPrice, Price, Description, Quantity, c.DateCreated, c.DateModified "
            + "FROM dbo.Cart c "
            + "left join Products p on c.ProductId=p.Id " + "left join Category cc on cc.Id=p.CategoryId ";

        private static string ConnectionString
            => ConfigurationManager.ConnectionStrings["ShoppingDB_VMJP"]?.ConnectionString;

        public Cart Get(long id)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = SelectCommandCore + "WHERE c.Id = @Id ";
            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;

            SqlDataReader reader = command.ExecuteReader();
            Cart product = null;

            if (reader.Read())
            {
                product = ExtractNextData(reader);
            }

            return product;
        }

        public IList<Cart> GetAll()
        {
            return GetAllCore(null, null);
        }

        public IList<Cart> GetAll(string whereClause, string orderByClause, params SqlParameter[] parameters)
        {
            return GetAllCore(whereClause, orderByClause, parameters);
        }

        private IList<Cart> GetAllCore(string whereClause, string orderByClause, params SqlParameter[] parameters)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = SelectCommandCore + whereClause + orderByClause;
            command.Parameters.AddRange(parameters);

            SqlDataReader reader = command.ExecuteReader();
            List<Cart> products = new List<Cart>();

            while (reader.Read())
            {
                products.Add(ExtractNextData(reader));
            }

            return products;
        }
        public Cart GetProductFromCart(int productId, int userId)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "select Id, UserId, ProductId,DiscountPercentage,IsCustomerCheckedOut,QtyOrdered,FinalProductPrice,DateCreated, DateModified"
                + " from Cart" + " Where ProductId= @ProductId and UserId= @UserId and IsCustomerCheckedOut=@IsCustomerCheckedOut";
            command.Parameters.Add("@ProductId", SqlDbType.Int).Value = productId;
            command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            command.Parameters.Add("@IsCustomerCheckedOut", SqlDbType.Bit).Value = false;


            SqlDataReader reader = command.ExecuteReader();
            Cart cart = new Cart();

            while (reader.Read())
                cart = ExtractNextData(reader);


            return cart;
        }

        public bool UpdateCart(Cart cart)
        {

            using SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText =
                "UPDATE dbo.Cart "
                + "SET DateModified = @DateModified, "
                + "DiscountPercentage = @DiscountPercentage, IsCustomerCheckedOut = @IsCustomerCheckedOut, "
                + "QtyOrdered = @QtyOrdered, FinalProductPrice= @FinalProductPrice "
                + "WHERE Id = @Id ";

            cart.DateModified = DateTime.UtcNow;
            command.Parameters.Add("@DateModified", SqlDbType.DateTime2).Value = cart.DateModified;

            command.Parameters.Add("@Id", SqlDbType.Int).Value = cart.Id;
            command.Parameters.Add("@DiscountPercentage", SqlDbType.Decimal).Value = cart.DiscountPercentage;
            command.Parameters.Add("@IsCustomerCheckedOut", SqlDbType.Bit).Value = cart.IsCustomerCheckedOut;
            command.Parameters.Add("@QtyOrdered", SqlDbType.Int).Value = cart.QtyOrdered;
            command.Parameters.Add("@FinalProductPrice", SqlDbType.Decimal).Value = cart.FinalProductPrice;

            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }

        private Cart ExtractNextData(SqlDataReader reader)
        {
            Cart cart = new Cart()
            {
                Id = reader.GetInt32(0),
                UserId = reader.GetInt32(1),
                ProductId = reader.GetInt32(2),
                DiscountPercentage = reader.GetDecimal(6),
                IsCustomerCheckedOut = reader.GetBoolean(7),
                QtyOrdered = reader.GetInt32(8),
                FinalProductPrice = reader.GetDecimal(9),
                DateCreated = reader.GetDateTime(13),
                DateModified = (reader.IsDBNull(14)) ? (DateTime?)null : reader.GetDateTime(14),
                ProductDetails = new Product()
                {
                    Id = reader.GetInt32(2),
                    Description = reader.GetString(11),
                    ProductName = reader.GetString(3),
                    Price = reader.GetDecimal(10),
                    Quantity = reader.GetInt32(12),
                    CategoryId = reader.GetInt32(4),
                    Category = new Category()
                    {
                        Id = reader.GetInt32(4),
                        CategoryName = reader.GetString(5)
                    }
                }
            };
            return cart;
        }


        public bool Remove(Cart cart)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "DELETE from dbo.Cart WHERE Id = @Id ";
            command.Parameters.Add("@Id", SqlDbType.Int).Value = cart.Id;

            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }

    }
}
