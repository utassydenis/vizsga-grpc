using Grpc.Net.Client;
using VizsgaGRPC;
using GRPCClient;
using Grpc.Core;
namespace GRPCClient
{
    public partial class Form1 : Form
    {
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5164");
        Vizsga.VizsgaClient client;
        string uid = "N/A";
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += logoutBtn_Click;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
                client = new Vizsga.VizsgaClient(channel);
        }
        private async void listBtn_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                using (var call = client.List(new Empty { }))
                {
                    while (await call.ResponseStream.MoveNext())
                    {
                        dataGridView1.AllowUserToAddRows = false;
                        dataGridView1.ColumnCount = 4;
                        dataGridView1.Columns[0].Name = "Product Code";
                        dataGridView1.Columns[1].Name = "Product Name";
                        dataGridView1.Columns[2].Name = "Current Price";
                        dataGridView1.Columns[3].Name = "Seller";
                        Product products = call.ResponseStream.Current;
                        string[] row = new string[] { products.Code, products.Name, products.CurPrice.ToString(), products.Username };
                        dataGridView1.Rows.Add(row);
                    }
                }
            }
            catch (RpcException x)
            {
                resultLbl.Text = "RPC Szerver nem elérhetõ!";
            }
            
        }
        private void addBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Int32.TryParse(priceTxtBx.Text, out int tmp))
                {
                    Data data = new Data();
                    data.Code = codeTxtBx.Text;
                    data.Name = nameTxtBx.Text;
                    data.Price = int.Parse(priceTxtBx.Text);
                    data.Uid = uid;
                    Result res = client.Add(data);
                    resultLbl.Text = res.ToString();
                }
                else
                {
                    resultLbl.Text = "Hibás ár. Minimum 1Ft";
                }
            }
            catch (RpcException x)
            {
                resultLbl.Text = "RPC Szerver nem elérhetõ!";
            }
        }
        private void bidBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Int32.TryParse(priceTxtBx.Text, out int tmp) && dataGridView1.SelectedRows.Count <= 0)
                {
                    BidProduct data = new BidProduct();
                    data.Code = codeTxtBx.Text;
                    data.Price = int.Parse(priceTxtBx.Text);
                    data.Uid = uid;
                    Result res = client.Bid(data);
                    resultLbl.Text = res.ToString();
                }
                else if (Int32.TryParse(priceTxtBx.Text, out tmp) && dataGridView1.SelectedRows.Count > 0)
                {
                    BidProduct data = new BidProduct();
                    data.Code = dataGridView1.SelectedCells[0].Value.ToString();
                    data.Price = int.Parse(priceTxtBx.Text);
                    data.Uid = uid;
                    Result res = client.Bid(data);
                    resultLbl.Text = res.ToString();
                }
                else
                {
                    resultLbl.Text = "Hibás ár. Minimum 1Ft.";
                }
            }
            catch (RpcException x)
            {
                resultLbl.Text = "RPC Szerver nem elérhetõ!";
            }
        }
        private void modifyBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Int32.TryParse(priceTxtBx.Text, out int tmp))
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        ModifyProduct m = new ModifyProduct();
                        m.Code = dataGridView1.SelectedCells[0].Value.ToString();
                        m.Price = int.Parse(priceTxtBx.Text);
                        m.Uid = uid;
                        Result res = client.Modify(m);
                        resultLbl.Text = res.ToString();
                    }
                    else
                    {
                        ModifyProduct m = new ModifyProduct();
                        m.Code = codeTxtBx.Text;
                        m.Price = int.Parse(priceTxtBx.Text);
                        m.Uid = uid;
                        Result res = client.Modify(m);
                        resultLbl.Text = res.ToString();
                    }
                }
                else
                {
                    resultLbl.Text = "Hibás ár. Minimum 1Ft";
                }
            }
            catch (RpcException x)
            {
                resultLbl.Text = "RPC Szerver nem elérhetõ!";
            }

        }
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DeleteProduct d = new DeleteProduct();
                    d.Code = dataGridView1.SelectedCells[0].Value.ToString();
                    d.Uid = uid;
                    Result res = client.Delete(d);
                    resultLbl.Text = res.ToString();
                }
                else
                {
                    DeleteProduct d = new DeleteProduct();
                    d.Code = codeTxtBx.Text;
                    d.Uid = uid;
                    Result res = client.Delete(d);
                    resultLbl.Text = res.ToString();
                }
            }
            catch (RpcException x)
            {
                resultLbl.Text = "RPC Szerver nem elérhetõ!";
            }


        }
        private void loginBtn_Click(object sender, EventArgs e)
        {
            if ((userTxtBx.Text != "" || passTxtBx.Text != "" ) && uid == "N/A")
            {
                try
                {
                    User user = new User();
                    user.Name = userTxtBx.Text;
                    user.Password = passTxtBx.Text;
                    Session_Id tempuid = client.Login(user);
                    connStringLbl.Text = tempuid.ToString();
                    if (tempuid.ToString().Contains("N/A"))
                    {
                        resultLbl.Text = "Bejelentkezés sikertelen";
                    }
                    else
                    {
                        string temp = tempuid.ToString();
                        uid = temp.Substring(9, 36);
                    }
                }
                catch (RpcException x)
                {
                    resultLbl.Text = "RPC Szerver nem elérhetõ!";
                }

            }
        }
        private void logoutBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Session_Id s = new Session_Id();
                s.Id = uid;
                Result res = client.Logout(s);
                resultLbl.Text = res.ToString();
                connStringLbl.Text = res.ToString();
                uid = "N/A";
            }
            catch (RpcException x)
            {
                resultLbl.Text = "RPC Szerver nem elérhetõ!";
            }
        }
        private void registerBtn_Click(object sender, EventArgs e)
        {
            try
            {
                    User user = new User();
                    user.Name = registerUserTxtBx.Text;
                    user.Password = registerPswrdTxtBx.Text;
                    Result res = client.Register(user);
                    resultLbl.Text = res.ToString();
            }
            catch (RpcException x)
            {
                resultLbl.Text = "RPC Szerver nem elérhetõ!";
            }
        }
    }
}