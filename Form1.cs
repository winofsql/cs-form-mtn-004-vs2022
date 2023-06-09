using System.Data.Odbc;
using System.Diagnostics;

namespace cs_form_mtn_004_vs2022
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 確認_Click(object sender, EventArgs e)
        {
            // 必要なクラス
            OdbcConnection myCon = new OdbcConnection();
            OdbcCommand myCommand = new OdbcCommand();

            // 接続文字列の作成
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
                // 接続 
                myCon.Open();
            }
            catch (Exception ex)
            {
                functionExit = true;
                MessageBox.Show($"接続エラー : {ex.Message}");
            }
            // 接続エラーの為
            if (functionExit)
            {
                return;
            }
            // =====================================

            // コマンドオブジェクトを接続に関係付ける 
            myCommand.Connection = myCon;
            // 社員コード存在チェック用の SQL 作成
            string strQuery = @$"select * from 社員マスタ
                                    where 社員コード = '{this.社員コード.Text}'";

            myCommand.CommandText = strQuery;
            Debug.WriteLine($"DBG:{strQuery}");

            OdbcDataReader myReader = myCommand.ExecuteReader();
            bool check = myReader.Read();
            if (check)
            {
                myReader.Close();
                myCon.Close();
                MessageBox.Show($"入力された社員コードは既に登録されています : {this.社員コード.Text}");

                // 再入力が必要なので、フォーカスして選択
                this.社員コード.Focus();
                this.社員コード.SelectAll();
                return;
            }

            // 接続解除
            myCon.Close();

            // 第二会話へ遷移
            this.ヘッド部.Enabled = false;
            this.ボディ部.Enabled = true;

            // 最初に入力必要なフィールドにフォーカスして選択
            this.氏名.Focus();
            this.氏名.SelectAll();

        }

        private void キャンセル_Click(object sender, EventArgs e)
        {
            // 第一会話(初期)へ遷移
            this.ヘッド部.Enabled = true;
            this.ボディ部.Enabled = false;

            // 最初に入力必要なフィールドにフォーカスして選択
            this.社員コード.Focus();
            this.社員コード.SelectAll();

            // キャンセルなので入力したフィールドのクリア
            this.氏名.Clear();
            this.給与.Clear();
            this.生年月日.Value = DateTime.Now;

        }

        private void 更新_Click(object sender, EventArgs e)
        {
            // 必要なクラス
            OdbcConnection myCon = new OdbcConnection();
            OdbcCommand myCommand = new OdbcCommand();

            // 接続文字列の作成
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
                // 接続 
                myCon.Open();
            }
            catch (Exception ex)
            {
                functionExit = true;
                MessageBox.Show($"接続エラー : {ex.Message}");
            }
            // 接続エラーの為
            if (functionExit)
            {
                return;
            }

            // 更新処理

            // コマンドオブジェクトを接続に関係付ける 
            myCommand.Connection = myCon;
            // 社員コード存在チェック用の SQL 作成
            string strQuery = @$"insert into `社員マスタ` (
	`社員コード` 
	,`氏名` 
	,`給与` 
	,`生年月日` 
)
 values(
	'{this.社員コード.Text}'
	,'{this.氏名.Text}'
	,{this.給与.Text}
	,'{this.生年月日.Value}'
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
                MessageBox.Show($"接続エラー : {ex.Message}");
            }
            // 接続エラーの為
            if (functionExit)
            {
                myCon.Close();
                return;
            }


            // 接続解除
            myCon.Close();

            // 第一会話(初期)へ遷移
            this.ヘッド部.Enabled = true;
            this.ボディ部.Enabled = false;

            // 最初に入力必要なフィールドにフォーカスして選択
            this.社員コード.Focus();
            this.社員コード.SelectAll();

            // キャンセルなので入力したフィールドのクリア
            this.社員コード.Clear();
            this.氏名.Clear();
            this.給与.Clear();
            this.生年月日.Value = DateTime.Now;


        }
    }
}
