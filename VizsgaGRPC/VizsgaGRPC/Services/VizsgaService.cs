using Grpc.Core;
using VizsgaGRPC;
using MySql.Data.MySqlClient;

namespace VizsgaGRPC.Services
{
    public class VizsgaService : Vizsga.VizsgaBase
    {

        List<Product> Products = new List<Product>();
        public override async Task List(Empty vmi, Grpc.Core.IServerStreamWriter<Product> responseStream, Grpc.Core.ServerCallContext context)
        {
            
            string query = "SELECT * FROM products";
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query,connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product();
                    product.Name = reader.GetString("name");
                    product.Code = reader.GetString("code");
                    product.CurPrice = int.Parse(reader.GetString("cur_price"));
                    product.Username = reader.GetString("username");
                    Products.Add(product);
                }
            }
            foreach (var Product in Products)
            {
                await responseStream.WriteAsync(Product);
            }
        } //Done
        public override Task<Result> Add(Data data, ServerCallContext context)
        {
            string query = "SELECT COUNT(*) FROM users WHERE session_string='" + data.Uid + "'";
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int count = int.Parse(cmd.ExecuteScalar() + "");
                if(count == 1)
                {
                    if(data.Price > 0)
                    {
                        query = "SELECT COUNT(*) FROM products WHERE code= '" + data.Code  +"'";
                        cmd = new MySqlCommand(query, connection);
                        count = int.Parse(cmd.ExecuteScalar() + "");
                        if (count == 1)
                        {
                            return Task.FromResult(new Result { Success = "A termék már létezik!" });
                        }
                        else
                        {
                            string user = "";
                            query = "SELECT username FROM users WHERE session_string='" + data.Uid + "'";
                            cmd = new MySqlCommand(query, connection);
                            MySqlDataReader dataReader = cmd.ExecuteReader();
                            while (dataReader.Read())
                            {
                                user = dataReader.GetString(0);
                            }
                            dataReader.Close();
                            query = "INSERT INTO products (name,code,cur_price,username) VALUES('" + data.Name + "','" + data.Code + "','" + data.Price + "','" + user + "')";
                            cmd = new MySqlCommand(query, connection);
                            cmd.ExecuteNonQuery();
                            CloseConnection();
                            return Task.FromResult(new Result { Success = "Sikeresen termékregisztráció!" });
                        }
                    }
                    else
                    {
                        return Task.FromResult(new Result { Success = "Hibás ár!" });
                    }
                }
                else
                {
                    CloseConnection();
                    return Task.FromResult(new Result { Success = "Be kell jelentkezni!" });
                }
            }
            else
            {
                return Task.FromResult(new Result { Success = "Nem lehetett elérni az adatbázist!" }); //To-do lekezelni kliensen
            }
        } //done
        public override Task<Result> Bid (BidProduct bid, ServerCallContext context)
        {
            string query = "SELECT COUNT(*) FROM users WHERE session_string='" + bid.Uid + "'";
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int count = int.Parse(cmd.ExecuteScalar() + "");
                if (count == 1)
                {
                    query = "SELECT COUNT(*) FROM products WHERE code='" + bid.Code + "'";
                    cmd = new MySqlCommand(query, connection);
                    count = int.Parse(cmd.ExecuteScalar() + "");
                    if(count == 1)
                    {
                        string user = "";
                        query = "SELECT username FROM users WHERE session_string ='" + bid.Uid + "'";
                        cmd = new MySqlCommand(query, connection);
                        MySqlDataReader dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            user = dataReader.GetString(0);
                        }
                        dataReader.Close();
                        query = "SELECT COUNT(*) FROM products WHERE username='" + user + "' AND code='" + bid.Code + "'";
                        cmd = new MySqlCommand(query, connection);
                        count = int.Parse(cmd.ExecuteScalar() + "");
                        if (count == 1)
                        {
                            CloseConnection();
                            return Task.FromResult(new Result { Success = "Saját árura nem lehet ajánlatot tenni!" });
                        }
                        else
                        {
                            string product = "";
                            query = "SELECT cur_price FROM products WHERE code='" + bid.Code + "'";
                            cmd = new MySqlCommand(query, connection);
                            MySqlDataReader dataReader2 = cmd.ExecuteReader();
                            while (dataReader2.Read())
                            {
                                product = dataReader2.GetString(0);
                            }
                            dataReader2.Close();
                            if(Int32.Parse(product) < bid.Price)
                            {
                                query = "UPDATE products SET cur_price='"+bid.Price + "' WHERE code='" +bid.Code+"'";
                                cmd = new MySqlCommand(query, connection);
                                cmd.ExecuteNonQuery();
                                CloseConnection();
                                return Task.FromResult(new Result { Success = "Successful bid!" });
                            }
                            else
                            {
                                CloseConnection();
                                return Task.FromResult(new Result { Success = "Offer too low!" });
                            }
                        }
                    }
                    else
                    {
                        CloseConnection();
                        return Task.FromResult(new Result { Success = "Áru nem létezik!" });
                    }
                }
                else
                {
                    CloseConnection();
                    return Task.FromResult(new Result { Success = "Be kell jelentkezni!" });
                }
            }
            else
            {
                return Task.FromResult(new Result { Success = "Nem lehetett elérni az adatbázist!" }); //To-do lekezelni kliensen
            }
        } //done
        public override Task<Result> Modify(ModifyProduct product, ServerCallContext context) //done
        {
            string query = "SELECT COUNT(*) FROM users WHERE session_string='" + product.Uid + "'";
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int count = int.Parse(cmd.ExecuteScalar() + "");
                if (count == 1)
                {
                    query = "SELECT COUNT(*) FROM products WHERE code='" + product.Code + "'";
                    cmd = new MySqlCommand(query, connection);
                    count = int.Parse(cmd.ExecuteScalar() + "");
                    if (count == 1)
                    {
                        string user = "";
                        query = "SELECT username FROM users WHERE session_string ='" + product.Uid +"'";
                        cmd = new MySqlCommand(query, connection);
                        MySqlDataReader dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            user = dataReader.GetString(0);
                        }
                        dataReader.Close();

                        query = "SELECT COUNT(*) FROM products WHERE username='" + user + "' AND code='" + product.Code + "'";
                        cmd = new MySqlCommand(query, connection);
                        count = int.Parse(cmd.ExecuteScalar() + "");

                        if (count == 1)
                        {
                            query = "UPDATE products SET cur_price='"+product.Price+"'WHERE code='" + product.Code +"';";
                            cmd = new MySqlCommand(query, connection);
                            cmd.ExecuteNonQuery();
                            CloseConnection();
                            return Task.FromResult(new Result { Success = "OK!" });
                        }
                        else
                        {
                            CloseConnection();
                            return Task.FromResult(new Result { Success = "Más áruját nem lehet változtatni!" });
                        }
                    }
                    else
                    {
                        CloseConnection();
                        return Task.FromResult(new Result { Success = "Nem létezik az áru!" });
                    }
                }
                else
                {
                    CloseConnection();
                    return Task.FromResult(new Result { Success = "Be kell jelentkezni!" });
                }
            }
            else
            {
                return Task.FromResult(new Result { Success = "Nem lehetett elérni az adatbázist!" }); //To-do lekezelni kliensen
            }
        }
        public override Task<Result> Delete(DeleteProduct product, ServerCallContext context) //done
        {
            string query = "SELECT COUNT(*) FROM users WHERE session_string='" + product.Uid + "'";
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int count = int.Parse(cmd.ExecuteScalar() + "");
                if(count == 1)
                {
                    query = "SELECT COUNT(*) FROM products WHERE code='" + product.Code + "'";
                    cmd = new MySqlCommand(query, connection);
                    count = int.Parse(cmd.ExecuteScalar() + "");
                    if(count == 1)
                    {
                        string user = "";
                        query = "SELECT username FROM users WHERE session_string ='" + product.Uid + "'";
                        cmd = new MySqlCommand(query, connection);
                        MySqlDataReader dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            user = dataReader.GetString(0);
                        }
                        dataReader.Close();

                        query = "SELECT COUNT(*) FROM products WHERE username='" + user + "' AND code='" + product.Code + "'";
                        cmd = new MySqlCommand(query, connection);
                        count = int.Parse(cmd.ExecuteScalar() + "");
                        if (count == 1)
                        {
                            query = "DELETE FROM products WHERE code='" + product.Code + "'";
                            cmd = new MySqlCommand(query, connection);
                            cmd.ExecuteNonQuery();
                            CloseConnection();
                            return Task.FromResult(new Result { Success = "OK!" });
                        }
                        else
                        {
                            CloseConnection();
                            return Task.FromResult(new Result { Success = "Más áruját nem lehet törölni!" });
                        }
                    }
                    else
                    {
                        CloseConnection();
                        return Task.FromResult(new Result { Success = "Nem létezik az áru!" });
                    }
                }
                else
                {
                    CloseConnection();
                    return Task.FromResult(new Result { Success = "Be kell jelentkezni!" });
                }
            }
            else
            {
                return Task.FromResult(new Result { Success = "Nem lehetett elérni az adatbázist!" }); //To-do lekezelni kliensen
            }
        }
        public override Task<Session_Id> Login(User user, ServerCallContext context) //done
        {
            string id = "";
            string query = "SELECT COUNT(*) FROM users WHERE username='" + user.Name + "' AND password='" + user.Password + "'";
            if(this.OpenConnection())
            {

                MySqlCommand cmd = new MySqlCommand(query,connection);
                int count = int.Parse(cmd.ExecuteScalar()+"");
                if(count == 1)
                {
                    id = Guid.NewGuid().ToString();
                    query = "UPDATE users SET session_string='" + id + "'WHERE username='"+user.Name + "'";
                    cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();

                    query = "INSERT INTO sessionlogs (username, session_string) VALUES('" + user.Name + "','" + id + "')";
                    cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();

                    this.CloseConnection();
                    return Task.FromResult(new Session_Id { Id = id });
                }
                else
                {
                    this.CloseConnection();
                    return Task.FromResult(new Session_Id { Id = "No" });
                }
            }
            else
            {
                return Task.FromResult(new Session_Id { Id = "Nem lehetett elérni az adatbázist!" }); //To-do lekezelni kliensen
            }

        }
        public override Task<Result> Logout(Session_Id id, ServerCallContext context) //done
        {
            string query = "SELECT COUNT(*) FROM users WHERE session_string='" + id.Id + "'";
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int count = int.Parse(cmd.ExecuteScalar() + "");
                if(count == 1)
                {
                    query = "UPDATE users SET session_string='' WHERE session_string='" + id.Id + "'";
                    cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return Task.FromResult(new Result { Success = "Sikeres Kijelentkezés!" });
                }
                else
                {
                    return Task.FromResult(new Result { Success = "Nincs bejelentkezve!" });
                }
            }
            else
            {
                return Task.FromResult(new Result { Success = "Nem lehetett elérni az adatbázist!" }); //To-do lekezelni kliensen
            }

        }
        //Regisztráció
        public override Task<Result> Register(User user, ServerCallContext context)
        {
            if (OpenConnection())
            {
                string query = "SELECT COUNT(*) FROM users WHERE username='" + user.Name + "'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int count = int.Parse(cmd.ExecuteScalar() + "");
                if(count == 1)
                {
                    CloseConnection();
                    return Task.FromResult(new Result { Success = "A felhasználó már létezik!" });
                }
                else
                {
                    query = "INSERT INTO users (username,password) VALUES('"+ user.Name+"','"+user.Password + "')";
                    cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    CloseConnection();
                    return Task.FromResult(new Result { Success = "Sikeres regisztráció!" });
                }
            }
            else
            {
                return Task.FromResult(new Result { Success = "Nem lehetett elérni az adatbázist!" });
            }

        }
        //SQL
        MySqlConnection connection = new MySqlConnection("SERVER=localhost;DATABASE=sopdb;UID=root;PASSWORD= ;");
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch(MySqlException e)
            {
                switch (e.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}