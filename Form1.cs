using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace Monopoly2
{
    public partial class Form1 : Form
    {
        public static List<Player> players;
        public bool enter;
        List<Card> cards;
        List<Sale> sales;
        string selectCards = "Select * from [Players]";
        string stringConnection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Monopoly2;";
        string stringConnectionMonopolyInfoForPlayers = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MonopolyInfoForPlayersDataBase;";
        SqlConnection sqlConnection;
        SqlCommand sqlCommand;
        SqlDataReader sqlDataReader;
        TextBox textBoxName;
        TextBox textBoxPassword;
        TextBox textBoxPasswordRegistration;
        TextBox textBoxPassword2Registration;
        TextBox textBoxNameRegistration;

        Panel panelStart;
        Panel panelEnter;
        Panel panelRegistration;
        PanelTop panelTop;
        Panel panelSearchGame;
        Panel panelProposal;
        Panel panelInventory;
        public Form1()
        {
            InitializeComponent();
            Size resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
            this.Size = resolution;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Load += Form1_Load;
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.FormClosed += Form1_FormClosed;
        }

        private void Form1_FormClosed(object? sender, FormClosedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlConnection.Close();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            Button buttonLeft = new Button();
            buttonLeft.Dock = DockStyle.Left;
            buttonLeft.FlatStyle = FlatStyle.Flat;
            buttonLeft.FlatAppearance.BorderSize = 0;
            buttonLeft.Image = Properties.Resources.arrow_left_White;

            Label labelMiddle= new Label();
            labelMiddle.Text = "12312313231";
            labelMiddle.Dock = DockStyle.Fill;
            labelMiddle.Font = new Font("", 18);

            Button buttonRight = new Button();
            buttonRight.Dock = DockStyle.Right;
            buttonRight.FlatStyle = FlatStyle.Flat;
            buttonRight.FlatAppearance.BorderSize = 0;
            buttonRight.Image = Properties.Resources.arrow_right_White;

            panelInventory = new Panel();
            panelInventory.Dock = DockStyle.Fill;
            panelInventory.BackColor = Color.FromArgb(240, 240, 240);

            panelProposal = new Panel();
            panelProposal.Size = new Size(600, 220);
            panelProposal.Location = new Point(1000, 200);
            panelProposal.Controls.AddRange(new Control[] { labelMiddle,buttonRight, buttonLeft});

            players = new List<Player>();
            panelSearchGame=new Panel();
            panelSearchGame.Visible= false;
            panelSearchGame.BackColor = Color.Red;
            panelSearchGame.Dock=DockStyle.Fill;
            panelSearchGame.BackColor = Color.FromArgb(240, 240, 240);
            panelStart = new Panel();
            panelStart.Dock=DockStyle.Fill;
            try
            {
                sqlConnection = new SqlConnection(stringConnection);
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = selectCards;
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();
                int i = 0;
                while (sqlDataReader.Read())
                {
                    players.Add(new Player());
                    players[i].id = int.Parse(sqlDataReader["Id"].ToString());
                    players[i].name = sqlDataReader["Name"].ToString();
                    players[i].password = sqlDataReader["Password"].ToString();
                    players[i].balance = double.Parse(sqlDataReader["Balance"].ToString());
                    players[i].amountCards = int.Parse(sqlDataReader["AmountCards"].ToString());
                    i++;
                }
                sqlDataReader.Close();
                panelTop = new PanelTop();


                panelEnter = new Panel();
                panelEnter.BackColor = Color.White;
                panelEnter.Location = new Point(500, 200);
                panelEnter.Size = new Size(400, 500);

                Label labelInfoEnter = new Label();
                labelInfoEnter.Text = "Авторизуйтесь при помощи логина и пароля от вашего аккаунта на Monopoly.";
                labelInfoEnter.Location = new Point(20, 20);
                labelInfoEnter.Font = new Font("", 18);
                labelInfoEnter.Size = new Size(250,370);
                labelInfoEnter.ForeColor= Color.Gray;
    
                Button buttonEnter = new Button();
                buttonEnter.FlatStyle = FlatStyle.Flat;
                buttonEnter.FlatAppearance.BorderSize = 0;
                buttonEnter.BackColor= Color.SpringGreen;
                buttonEnter.ForeColor= Color.White;
                buttonEnter.Font = new Font("", 14);
                buttonEnter.Text = "Войти";
                buttonEnter.Click += ButtonEnter_Click;
                buttonEnter.Size = new Size(100, 55);
                buttonEnter.Location = new Point(20, 420);

                textBoxName = new TextBox();
                textBoxName.Location = new Point(20, 270);
                textBoxName.BorderStyle = BorderStyle.None;
                textBoxName.BackColor = Color.AliceBlue;
                textBoxName.Font = new Font("", 20);
                textBoxName.Size = new Size(250, 100);

                textBoxPassword = new TextBox();
                textBoxPassword.Font = new Font("", 20);
                textBoxPassword.BorderStyle= BorderStyle.None;
                textBoxPassword.BackColor = Color.AliceBlue;
                textBoxPassword.Size = new Size(250, 100);
                textBoxPassword.Location = new Point(20, 350);

                Label labelName = new Label();
                labelName.Location = new Point(20, 220);
                labelName.Text = "Логин";
                labelName.Font = new Font("", 18);
                labelName.ForeColor = Color.Gray;

                Label labelPassword = new Label();
                labelPassword.Location = new Point(20, 300);
                labelPassword.Size = new Size(100, 50);
                labelPassword.Text = "Пароль";
                labelPassword.Font = new Font("", 18);
                labelPassword.ForeColor= Color.Gray;

                panelRegistration = new Panel();
                panelRegistration.BackColor = Color.White;
                panelRegistration.Location = new Point(905, 200);
                panelRegistration.Size = new Size(400, 500);

                Button buttonRegistration = new Button();
                buttonRegistration.Font = new Font("", 14);
                buttonRegistration.FlatStyle = FlatStyle.Flat;
                buttonRegistration.FlatAppearance.BorderSize = 0;
                buttonRegistration.BackColor = Color.SpringGreen;
                buttonRegistration.ForeColor = Color.White;
                buttonRegistration.Location = new Point(20, 400);
                buttonRegistration.Text = "Зарегистрироваться";
                buttonRegistration.BackColor = Color.SpringGreen;
                buttonRegistration.Click += ButtonRegistration_Click;
                buttonRegistration.Size = new Size(100, 55);

                textBoxNameRegistration = new TextBox();
                textBoxNameRegistration.Location=new Point(20, 230);
                textBoxNameRegistration.BorderStyle = BorderStyle.None;
                textBoxNameRegistration.BackColor = Color.AliceBlue;
                textBoxNameRegistration.Font = new Font("", 20);
                textBoxNameRegistration.Size = new Size(250, 100);

                Label labelNameRegistration = new Label();
                labelNameRegistration.Location = new Point(20, 180);
                labelNameRegistration.Text = "Логин";
                labelNameRegistration.Font = new Font("", 18);
                labelNameRegistration.ForeColor = Color.Gray;
                labelNameRegistration.Size = new Size(100, 30);

                Label labelPasswordRegistration = new Label();
                labelPasswordRegistration.Location = new Point(20, 280);
                labelPasswordRegistration.Text = "Пароль";
                labelPasswordRegistration.Font = new Font("", 18);
                labelPasswordRegistration.ForeColor = Color.Gray;
                labelPasswordRegistration.Size = new Size(100, 30);

                Label labelPassword2Registration = new Label();
                labelPassword2Registration.Location = new Point(20, 360);
                labelPassword2Registration.Text = "Повторите пароль";
                labelPassword2Registration.Font = new Font("", 18);
                labelPassword2Registration.ForeColor = Color.Gray;
                labelPassword2Registration.Size = new Size(300, 30);

                textBoxPasswordRegistration = new TextBox();
                textBoxPasswordRegistration.Location = new Point(20, 320);
                textBoxPasswordRegistration.BorderStyle = BorderStyle.None;
                textBoxPasswordRegistration.BackColor = Color.AliceBlue;
                textBoxPasswordRegistration.Font = new Font("", 20);
                textBoxPasswordRegistration.Size = new Size(250, 100);

                textBoxPassword2Registration = new TextBox();
                textBoxPassword2Registration.Location = new Point(20, 400);
                textBoxPassword2Registration.BorderStyle = BorderStyle.None;
                textBoxPassword2Registration.BackColor = Color.AliceBlue;
                textBoxPassword2Registration.Font = new Font("", 20);
                textBoxPassword2Registration.Size = new Size(250, 100);

                panelRegistration.Controls.AddRange(new Control[] { textBoxNameRegistration,labelNameRegistration,labelPasswordRegistration,textBoxPasswordRegistration,labelPassword2Registration,textBoxPassword2Registration,buttonRegistration });
                panelEnter.Controls.AddRange(new Control[] { buttonEnter, textBoxName, labelName, labelPassword, textBoxPassword, labelInfoEnter });
                panelStart.Controls.AddRange(new Control[] { panelEnter, panelRegistration, panelTop });

                Controls.AddRange(new Control[] { panelStart,panelSearchGame,panelInventory });
                panelTop.buttonMarket.Click += ButtonMarket_Click;
                panelTop.userHandler += PanelTop_userHandler;
                panelTop.inventoryHandler += PanelTop_inventoryHandler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonRegistration_Click(object? sender, EventArgs e)
        {
            if(textBoxPasswordRegistration.Text==textBoxPassword2Registration.Text)
            {
                players.Add(new Player(textBoxNameRegistration.Text,textBoxPassword2Registration.Text,0,0));
                panelStart.Visible = false;
                panelSearchGame.Visible = true;
                panelSearchGame.Controls.AddRange(new Control[] { panelTop, panelProposal });
                panelTop.buttonUser.Text = textBoxNameRegistration.Text;
                panelTop.buttonInventory.Visible = true;
                enter = true;
                for (int i=0;i<players.Count;i++)
                {
                    if (players[i].name!=textBoxNameRegistration.Text)
                    {
                        sqlCommand.CommandText = $"CREATE TABLE [{players[i].id}]()";
                        break;
                    }
                }
            }
        }

        private void PanelTop_inventoryHandler(object? sender, EventArgs e)
        {
            if (enter == true)
            {
                panelInventory.Visible = true;
                panelInventory.BackColor = Color.Khaki;
                panelSearchGame.Visible = false;
                panelProposal.Visible = false;
                panelInventory.Controls.Add(panelTop);
            }
        }

        private void PanelTop_userHandler(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonMarket_Click(object? sender, EventArgs e)
        {
            
        }

        private void ButtonEnter_Click(object? sender, EventArgs e)
        {
            for(int i=0;i<players.Count;i++) 
            {
                if (players[i].name == textBoxName.Text && players[i].password==textBoxPassword.Text)
                {
                    panelStart.Visible = false;
                    panelSearchGame.Visible= true;
                    panelSearchGame.Controls.AddRange(new Control[] { panelTop,panelProposal });
                    panelTop.buttonUser.Text = players[i].name;
                    panelTop.buttonInventory.Visible = true;
                    enter= true;
                    try
                    {
                        SqlConnection sqlConnection1 = new SqlConnection(stringConnectionMonopolyInfoForPlayers);
                        sqlConnection1.Open();
                        sqlCommand.Connection = sqlConnection1;
                        sqlCommand.CommandText = $"select * from [{players[i].id}]";
                        sqlDataReader = sqlCommand.ExecuteReader();
                        while (sqlDataReader.Read())
                        {
                            for (int j = 0; j < sqlDataReader.FieldCount; j++)
                            {
                                players[i].inventory.Columns.Add(sqlDataReader.GetName(i));
                            }
                            DataRow dataRow = players[i].inventory.NewRow();
                            for (int j = 0; j < sqlDataReader.FieldCount; j++)
                            {
                                dataRow[j] = sqlDataReader[j];
                            }
                            players[i].inventory.Rows.Add(dataRow);
                        }
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message); }
                    finally
                    {
                        if(sqlDataReader!=null)
                        {
                            sqlDataReader.Close();
                        }
                        if(sqlConnection!=null)
                        {
                            sqlConnection.Close();
                        }
                    }
                    break;
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.");
                    break;
                }
            }
        }
    }
}