using bank;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace library
{
    public partial class FormNewAccount : Form
    {
        private readonly FormAccounts _parent;
        public string? id, clientID, accountNumber, accountType, balance;

        public FormNewAccount(FormAccounts parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        public void UpdateInfo()
        {
            textBox5.Text = clientID;
            textBox4.Text = accountNumber;
            textBox1.Text = accountType;
            textBox2.Text = balance;
            label1.Text = "Редагування рахунку";
            btnAdd.Text = "Оновити";
        }

        public void DefaultInfo()
        {
            Operations.Clear(this);
            label1.Text = "Додавання рахунку";
            btnAdd.Text = "Зберегти";
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Account a = new Account(textBox5.Text.Trim(), textBox4.Text.Trim(), textBox1.Text.Trim(), textBox2.Text.Trim());
            string insert = "INSERT INTO Accounts (ClientID, AccountNumber, AccountType, Balance) " +
                "VALUES (@ClientID, @AccountNumber, @AccountType, @Balance);";
            string update = "UPDATE Accounts SET ClientID = @ClientID, AccountNumber = @AccountNumber, AccountType = @AccountType, " +
                "Balance = @Balance WHERE AccountID = @AccountID;";
            if (btnAdd.Text == "Зберегти")
            {
                Operations.ManageAccount(a, insert);
            }
            if (btnAdd.Text == "Оновити")
            {
                Operations.ManageAccount(a, update, id);
            }
            Operations.Clear(this);
            _parent.Display();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
