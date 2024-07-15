using bank;
using library;
using System;
using System.Windows.Forms;

namespace bank
{
    public partial class FormAccounts : Form
    {
        private readonly Main _parent;
        FormNewAccount accountForm;

        public string sql = "SELECT AccountID, ClientID, AccountNumber, AccountType, Balance FROM Accounts;";

        public FormAccounts(Main parent)
        {
            InitializeComponent();
            _parent = parent;
            accountForm = new FormNewAccount(this);
        }

        public void Display()
        {
            Operations.DisplayAndSearch(sql, dataGridView1);
        }

        private void FormAccounts_Load(object sender, EventArgs e)
        {
            Display();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchSql = "SELECT AccountID, ClientID, AccountNumber, AccountType, Balance FROM Accounts " +
                "WHERE AccountNumber LIKE '%" + txtSearch.Text + "%';";
            Operations.DisplayAndSearch(searchSql, dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                accountForm.id = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                accountForm.clientID = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                accountForm.accountNumber = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                accountForm.accountType = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                accountForm.balance = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                accountForm.UpdateInfo();
                accountForm.ShowDialog();
                return;
            }
            if (e.ColumnIndex == 1)
            {
                if (MessageBox.Show("Дійсно видалити?", "Інформація", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Operations.Delete(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(), "AccountID", "Accounts");
                    Display();
                }
                return;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            accountForm.DefaultInfo();
            accountForm.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
