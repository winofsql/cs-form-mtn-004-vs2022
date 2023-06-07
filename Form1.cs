using System.Data.Odbc;
using System.Diagnostics;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void �m�F_Click(object sender, EventArgs e)
        {
            // �K�v�ȃN���X
            OdbcConnection myCon = new OdbcConnection();
            OdbcCommand myCommand = new OdbcCommand();

            // �ڑ�������̍쐬
            string server = "localhost";
            string database = "lightbox";
            string user = "root";
            string pass = "";
            string strCon = $"Driver={{MySQL ODBC 8.0 Unicode Driver}};SERVER={server};DATABASE={database};UID={user};PWD={pass}";
            Debug.WriteLine($"DBG:{strCon}");

            myCon.ConnectionString = strCon;

            bool functionExit = false;
            try
            {
                // �ڑ� 
                myCon.Open();
            }
            catch (Exception ex)
            {
                functionExit = true;
                MessageBox.Show($"�ڑ��G���[ : {ex.Message}");
            }
            // �ڑ��G���[�̈�
            if (functionExit)
            {
                return;
            }
            // =====================================

            // �R�}���h�I�u�W�F�N�g��ڑ��Ɋ֌W�t���� 
            myCommand.Connection = myCon;
            // �Ј��R�[�h���݃`�F�b�N�p�� SQL �쐬
            string strQuery = @$"select * from �Ј��}�X�^
                                    where �Ј��R�[�h = '{this.�Ј��R�[�h.Text}'";

            myCommand.CommandText = strQuery;
            Debug.WriteLine($"DBG:{strQuery}");

            OdbcDataReader myReader = myCommand.ExecuteReader();
            bool check = myReader.Read();
            if (check)
            {
                myReader.Close();
                myCon.Close();
                MessageBox.Show($"���͂��ꂽ�Ј��R�[�h�͊��ɓo�^����Ă��܂� : {this.�Ј��R�[�h.Text}");

                // �ē��͂��K�v�Ȃ̂ŁA�t�H�[�J�X���đI��
                this.�Ј��R�[�h.Focus();
                this.�Ј��R�[�h.SelectAll();
                return;
            }

            // �ڑ�����
            myCon.Close();

            // ����b�֑J��
            this.�w�b�h��.Enabled = false;
            this.�{�f�B��.Enabled = true;

            // �ŏ��ɓ��͕K�v�ȃt�B�[���h�Ƀt�H�[�J�X���đI��
            this.����.Focus();
            this.����.SelectAll();

        }

        private void �L�����Z��_Click(object sender, EventArgs e)
        {
            // ����b(����)�֑J��
            this.�w�b�h��.Enabled = true;
            this.�{�f�B��.Enabled = false;

            // �ŏ��ɓ��͕K�v�ȃt�B�[���h�Ƀt�H�[�J�X���đI��
            this.�Ј��R�[�h.Focus();
            this.�Ј��R�[�h.SelectAll();

            // �L�����Z���Ȃ̂œ��͂����t�B�[���h�̃N���A
            this.����.Clear();
            this.���^.Clear();
            this.���N����.Value = DateTime.Now;

        }

        private void �X�V_Click(object sender, EventArgs e)
        {
            // �K�v�ȃN���X
            OdbcConnection myCon = new OdbcConnection();
            OdbcCommand myCommand = new OdbcCommand();

            // �ڑ�������̍쐬
            string server = "localhost";
            string database = "lightbox";
            string user = "root";
            string pass = "";
            string strCon = $"Driver={{MySQL ODBC 8.0 Unicode Driver}};SERVER={server};DATABASE={database};UID={user};PWD={pass}";
            Debug.WriteLine($"DBG:{strCon}");

            myCon.ConnectionString = strCon;

            bool functionExit = false;
            try
            {
                // �ڑ� 
                myCon.Open();
            }
            catch (Exception ex)
            {
                functionExit = true;
                MessageBox.Show($"�ڑ��G���[ : {ex.Message}");
            }
            // �ڑ��G���[�̈�
            if (functionExit)
            {
                return;
            }

            // �X�V����

            // �R�}���h�I�u�W�F�N�g��ڑ��Ɋ֌W�t���� 
            myCommand.Connection = myCon;
            // �Ј��R�[�h���݃`�F�b�N�p�� SQL �쐬
            string strQuery = @$"insert into `�Ј��}�X�^` (
	`�Ј��R�[�h` 
	,`����` 
	,`���^` 
	,`���N����` 
)
 values(
	'{this.�Ј��R�[�h.Text}'
	,'{this.����.Text}'
	,{this.���^.Text}
	,'{this.���N����.Value}'
)";

            myCommand.CommandText = strQuery;
            Debug.WriteLine($"DBG:{strQuery}");


            functionExit = false;
            try
            {
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                functionExit = true;
                MessageBox.Show($"�ڑ��G���[ : {ex.Message}");
            }
            // �ڑ��G���[�̈�
            if (functionExit)
            {
                myCon.Close();
                return;
            }


            // �ڑ�����
            myCon.Close();

            // ����b(����)�֑J��
            this.�w�b�h��.Enabled = true;
            this.�{�f�B��.Enabled = false;

            // �ŏ��ɓ��͕K�v�ȃt�B�[���h�Ƀt�H�[�J�X���đI��
            this.�Ј��R�[�h.Focus();
            this.�Ј��R�[�h.SelectAll();

            // �L�����Z���Ȃ̂œ��͂����t�B�[���h�̃N���A
            this.�Ј��R�[�h.Clear();
            this.����.Clear();
            this.���^.Clear();
            this.���N����.Value = DateTime.Now;


        }
    }
}