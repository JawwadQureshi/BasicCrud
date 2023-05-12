using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Xml;

namespace MyStore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listOfClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";

                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Opens SQL connection
                    connection.Open();

                    //Reads Data from all the rows in the clients table
                    String sql = "SELECT * FROM clients";

                    //Executes SQL Query
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        //Obtain SQL Data Reader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //Read the data from the table
                            while (reader.Read())
                            {
                                //Save data into a client info object
                                ClientInfo clientInfo = new ClientInfo();

                                //Id is of type string, but in database it is integer, so must add "" to convert
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5).ToString();
                            }
                        }
                    }
                }
            }
            catch(Exception ex) { }

        }
    }

    public class ClientInfo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string created_at;
    }
}
