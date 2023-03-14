using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly2
{
    public class PanelTop : Panel
    {
        PictureBox pictureBox;
        Label labelTextLogo;
        Button buttonSearchGame;
        public Button buttonInventory;
        public Button buttonMarket;
        public Button buttonUser;
        public event EventHandler userHandler;
        public event EventHandler inventoryHandler;
        public PanelTop()
        {

            pictureBox = new PictureBox();
            pictureBox.Image = Properties.Resources.MonopolyLogo;
            pictureBox.Location = new Point(300, 20);
            pictureBox.Size = new Size(64, 64);
            pictureBox.BackColor = Color.AntiqueWhite;

            labelTextLogo = new Label();
            labelTextLogo.Text = "Monopoly One";
            labelTextLogo.Location = new Point(370, 20);
            labelTextLogo.Size = new Size(170, 64);
            labelTextLogo.Font = new Font("", 16);
            labelTextLogo.TextAlign = ContentAlignment.MiddleCenter;

            buttonSearchGame = new Button();
            buttonSearchGame.FlatStyle = FlatStyle.Flat;
            buttonSearchGame.FlatAppearance.BorderSize = 0;
            buttonSearchGame.BackColor = Color.GreenYellow;
            buttonSearchGame.Location = new Point(570, 20);
            buttonSearchGame.Size = new Size(150, 64);
            buttonSearchGame.ForeColor = Color.White;
            buttonSearchGame.Text = "Поиск игр";
            buttonSearchGame.Font = new Font("", 16);

            buttonMarket = new Button();
            buttonMarket.FlatStyle = FlatStyle.Flat;
            buttonMarket.FlatAppearance.BorderSize = 0;
            buttonMarket.Location = new Point(740, 20);
            buttonMarket.Size = new Size(150, 64);
            buttonMarket.Text = "Маркет";
            buttonMarket.Font = new Font("", 16);

            buttonInventory = new Button();
            buttonInventory.FlatStyle = FlatStyle.Flat;
            buttonInventory.FlatAppearance.BorderSize = 0;
            buttonInventory.Location = new Point(900, 20);
            buttonInventory.Size = new Size(150, 64);
            buttonInventory.Text = "Инвентарь";
            buttonInventory.Font = new Font("", 16);
            buttonInventory.Visible = false;
            buttonInventory.Click += ButtonInventory_Click;

            buttonUser = new Button();
            buttonUser.FlatStyle = FlatStyle.Flat;
            buttonUser.FlatAppearance.BorderSize = 0;
            buttonUser.Location = new Point(1540, 20);
            buttonUser.Size = new Size(130, 64);
            buttonUser.Text = "Войти";
            buttonUser.Font = new Font("", 16);
            buttonUser.BackColor = Color.GreenYellow;
            buttonUser.ForeColor = Color.White;
            buttonUser.Click += ButtonUser_Click;

            this.Dock = DockStyle.Top;
            this.BackColor = Color.White;
            Controls.AddRange(new Control[] { pictureBox, labelTextLogo, buttonSearchGame, buttonMarket, buttonUser,buttonInventory });

        }

        private void ButtonInventory_Click(object? sender, EventArgs e)
        {
            inventoryHandler.Invoke(this, EventArgs.Empty);
        }

        private void ButtonUser_Click(object? sender, EventArgs e)
        {
            userHandler?.Invoke(this,EventArgs.Empty);
        }
    }
        public class Player
        {
            public int id;
            public string name;
            public string password;
            public double balance;
            public int amountCards;
            public DataTable inventory;
            public List<PanelCardInInventory> panelCardInInventories;
            public Player(int id, string name, string password,double balance,int amountCards,DataTable dataTable) 
            {
                this.id = id;
                this.name = name;
                this.password = password;
                this.balance = balance;
                this.amountCards = amountCards;
                inventory=dataTable;
                for(int i=0;i<dataTable.Rows.Count;i++)
                {
                panelCardInInventories.Add(new PanelCardInInventory(new Card(int.Parse(dataTable.Rows[i].Table.Columns["Id"].ToString()),
                        dataTable.Rows[i].Table.Columns["Name"].ToString(), double.Parse(dataTable.Rows[i].Table.Columns["Price"].ToString()), 
                        dataTable.Rows[i].Table.Columns["Mono"].ToString(), dataTable.Rows[i].Table.Columns["Class"].ToString(),
                        (dataTable.Rows[i].Table.Columns["Image"].ToString()))));
                }
             }
        public Player() { }
            public Player(string name, string password, double balance, int amountCards)
            {
            this.name = name;
            this.password = password;
            this.balance = balance;
            this.amountCards = amountCards;
            }
        }
        public class Card
        {
            public int id;
            public string name;
            public double price;
            public string monoCard;
            public string classCard;
            public Image image;
            public Card(int id, string name, double price,string monoCard,string classCard,string stringImage)
            {
                this.id = id;
                this.name = name;
                this.price = price;
                this.monoCard=monoCard;
                this.classCard = classCard;
                image = Image.FromFile(stringImage);
            }
        }
        //public class Market
        //{
        //    public int id;
        //    public int idCard;
        //    public int countCard;
        //    public double priceCard;
        //    public Market(int id, int idCard, int countCard, double priceCard)
        //    {
        //        this.id = id;
        //        this.idCard = idCard;
        //        this.countCard = countCard;
        //        this.priceCard = priceCard;
        //    }
        //}
        public class Sale
        {
            public int id;
            public int idCard;
            public int countCard;
            public double priceCard;
            public DateTime dateTime;
            public Sale(int id, int idCard, int countCard, double priceCard, DateTime dateTime)
            {
                this.id = id;
                this.idCard = idCard;
                this.countCard = countCard;
                this.priceCard = priceCard;
                this.dateTime = dateTime;
        }
    }
    public class PanelCardInInventory:Panel
    {
        Card card;
        PictureBox pictureBox;
        Label label;
        public PanelCardInInventory(Card card)
        {
            this.card = card;
            this.BackColor = Color.White;

            pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.BackColor = Color.White;

            label = new Label();
            label.Dock = DockStyle.Bottom;
            switch (card.classCard)
            {
                case "Обыкновенный": label.BackColor=Color.AliceBlue; break;
                case "Стандартный":label.BackColor = Color.Blue; break;
                case "Особенный": label.BackColor = Color.Violet;break;
                case "Высочайший": label.BackColor = Color.IndianRed;break;
                case "Эксклюзивный": label.BackColor = Color.Gold;break;
            }
            Controls.AddRange(new Control[] { pictureBox, label });
        }
    }

}
