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
            if (OpenConnection())
            {
                string query = "SELECT * FROM products";
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
            if (this.OpenConnection())
            {
                string query = "SELECT COUNT(*) FROM users WHERE session_string='" + data.Uid + "'";
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
                            return Task.FromResult(new Result { Success = "A term�k m�r l�tezik!" });
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
                            return Task.FromResult(new Result { Success = "Sikeresen term�kregisztr�ci�!" });
                        }
                    }
                    else
                    {
                        return Task.FromResult(new Result { Success = "Hib�s �r! Minimum 1 Ft" });
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
                return Task.FromResult(new Result { Success = "Nem lehetett el�rni az adatb�zist!" });
            }
        } //done
        public override Task<Result> Bid (BidProduct bid, ServerCallContext context)
        {
            if (this.OpenConnection())
            {
                string query = "SELECT COUNT(*) FROM users WHERE session_string='" + bid.Uid + "'";
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
                            return Task.FromResult(new Result { Success = "Saj�t �rura nem lehet aj�nlatot tenni!" });
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
                                return Task.FromResult(new Result { Success = "T�l alacsony aj�nlat!" });
                            }
                        }
                    }
                    else
                    {
                        CloseConnection();
                        return Task.FromResult(new Result { Success = "�ru nem l�tezik!" });
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
                return Task.FromResult(new Result { Success = "Nem lehetett el�rni az adatb�zist!" });
            }
        } //done
        public override Task<Result> Modify(ModifyProduct product, ServerCallContext context) //done
        {
            if (this.OpenConnection())
            {
                string query = "SELECT COUNT(*) FROM users WHERE session_string='" + product.Uid + "'";
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
                            return Task.FromResult(new Result { Success = "M�s �ruj�t nem lehet v�ltoztatni!" });
                        }
                    }
                    else
                    {
                        CloseConnection();
                        return Task.FromResult(new Result { Success = "Nem l�tezik az �ru!" });
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
                return Task.FromResult(new Result { Success = "Nem lehetett el�rni az adatb�zist!" }); //To-do lekezelni kliensen
            }
        }
        public override Task<Result> Delete(DeleteProduct product, ServerCallContext context) //done
        {
            if (this.OpenConnection())
            {
                string query = "SELECT COUNT(*) FROM users WHERE session_string='" + product.Uid + "'";
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
                            return Task.FromResult(new Result { Success = "M�s �ruj�t nem lehet t�r�lni!" });
                        }
                    }
                    else
                    {
                        CloseConnection();
                        return Task.FromResult(new Result { Success = "Nem l�tezik az �ru!" });
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
                return Task.FromResult(new Result { Success = "Nem lehetett el�rni az adatb�zist!" });
            }
        }
        public override Task<Session_Id> Login(User user, ServerCallContext context) //done
        {
            string id = "";
            if (this.OpenConnection())
            {
                string query = "SELECT COUNT(*) FROM users WHERE username='" + user.Name + "' AND password='" + user.Password + "'";
                MySqlCommand cmd = new MySqlCommand(query,connection);
                int count = int.Parse(cmd.ExecuteScalar()+"");
                if(count == 1)
                {
                    //Session string a users-ben
                    id = Guid.NewGuid().ToString();
                    query = "UPDATE users SET session_string='" + id + "'WHERE username='"+user.Name + "'";
                    cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();

                    //session string a sessionstring-ben
                    query = "INSERT INTO sessionlogs (username, session_string) VALUES('" + user.Name + "','" + id + "')";
                    cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();

                    this.CloseConnection();
                    return Task.FromResult(new Session_Id { Id = id });
                }
                else
                {
                    this.CloseConnection();
                    return Task.FromResult(new Session_Id { Id = "N/A" });
                }
            }
            else
            {
                return Task.FromResult(new Session_Id { Id = "Nem lehetett el�rni az adatb�zist!" });
            }

        }
        public override Task<Result> Logout(Session_Id id, ServerCallContext context) //done
        {
            if (this.OpenConnection())
            {
                string query = "SELECT COUNT(*) FROM users WHERE session_string='" + id.Id + "'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int count = int.Parse(cmd.ExecuteScalar() + "");
                if(count == 1)
                {
                    query = "UPDATE users SET session_string='' WHERE session_string='" + id.Id + "'";
                    cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return Task.FromResult(new Result { Success = "Sikeres Kijelentkez�s!" });
                }
                else
                {
                    return Task.FromResult(new Result { Success = "Nincs bejelentkezve!" });
                }
            }
            else
            {
                return Task.FromResult(new Result { Success = "Nem lehetett el�rni az adatb�zist!" });
            }

        }
        //Regisztr�ci�
        public override Task<Result> Register(User user, ServerCallContext context)
        {
            if (OpenConnection())
            {
                if(user.Name == "" || user.Password == "")
                {
                    CloseConnection();
                    return Task.FromResult(new Result { Success = "Hiba! Nincs adat!" });
                }
                string query = "SELECT COUNT(*) FROM users WHERE username='" + user.Name + "'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int count = int.Parse(cmd.ExecuteScalar() + "");
                if(count == 1)
                {
                    CloseConnection();
                    return Task.FromResult(new Result { Success = "A felhaszn�l� m�r l�tezik!" });
                }
                else
                {
                    query = "INSERT INTO users (username,password) VALUES('"+ user.Name+"','"+user.Password + "')";
                    cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    CloseConnection();
                    return Task.FromResult(new Result { Success = "Sikeres regisztr�ci�!" });
                }
            }
            else
            {
                return Task.FromResult(new Result { Success = "Nem lehetett el�rni az adatb�zist!" });
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