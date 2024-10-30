using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace _10_DatabaseCrud
{
    internal class Program
    {
        static void Main(string[] args)
        {

            #region Ürün Ekleme İşlemi

            //Console.WriteLine("***** Ürün ekleme *****");
            //Console.WriteLine();

            //string productName;
            //decimal productPrice;
            ////bool productStatus;

            //Console.Write("Ürün adı:");
            //productName = Console.ReadLine();

            //Console.Write("Ürün fiyatı:");
            //productPrice = decimal.Parse(Console.ReadLine());

            //SqlConnection connection = new SqlConnection("Data Source=WIN11\\MYSQLEXPRESS; " +
            //                                             "initial catalog=EgitimKampiDb;" +
            //                                             "integrated security=true");

            //connection.Open();
            //SqlCommand cmd = new SqlCommand("insert into TblProduct " +
            //    "           (ProductName,ProductPrice,ProductStatus) " +
            //    "           values(@productName,@productPrice, @productStatus)", connection);

            //cmd.Parameters.AddWithValue("@productName", productName);
            //cmd.Parameters.AddWithValue("@productPrice", productPrice);
            //cmd.Parameters.AddWithValue("@productStatus", true);

            //cmd.ExecuteNonQuery();
            //connection.Close();

            //Console.WriteLine("Başarı ile eklenmiştir.");

            #endregion

            #region Ürün Silme İşlemi

            Console.WriteLine("***** Ürün listesi *****");
            Console.WriteLine();

            // Ado net sqlconnection tanımlama
            SqlConnection connection = new SqlConnection("Data Source=WIN11\\MYSQLEXPRESS; initial catalog=EgitimKampiDb; integrated security=true");

            // Ado net sql komutu tanımlama
            SqlCommand sqlCommand = new SqlCommand("Select * From TblProduct", connection);

            // Ado.net te sql den gelen verileri SqlDataAdaptor yardımı ile yakalama
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

            try
            {
                // sql bağlantısı aç
                connection.Open();

                // SqlCommand ile oluşturulan sorgu komutunu herhangi bir koşul olmadan sorgula
                sqlCommand.ExecuteNonQuery();

                // database'den gelen verileri dataTable ile geçici belleğe al
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // datatable içinde satır satır dön
                foreach (DataRow row in dataTable.Rows)
                {
                    // datatable içindeki satırın sütun değerlerini bulmak için dön
                    foreach (var item in row.ItemArray)
                    {
                        Console.Write(item.ToString() + " ");
                    }
                    Console.WriteLine();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                throw ex;
            }

            int deletedId;

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("***** Ürün silme işlemi *****");
            Console.Write("Silinecek ürün kodunu girin:");

            deletedId = int.Parse(Console.ReadLine());

            SqlCommand sqlCommandDelete = new SqlCommand("Delete From TblProduct Where ProductId=@productId", connection);
            sqlCommandDelete.Parameters.AddWithValue("productId", deletedId);

            try
            {
                connection.Open();
                sqlCommandDelete.ExecuteNonQuery();
                connection.Close();

                Console.WriteLine("Başarı ile silindi");
            }
            catch (Exception e)
            {
                connection.Close();
                throw e;
            }

            #endregion




            Console.Read();
        }
    }
}
