using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace FPSServer.db;

public class DbManager {
	public static MySqlConnection mysql;
	
	public static bool Connect(string db, string ip, int port, string user, string pw)
	{

		mysql = new MySqlConnection();

		string s = string.Format("Database={0};Data Source={1}; port={2};User Id={3}; Password={4}", 
			               db, ip, port, user, pw);
		mysql.ConnectionString = s;

		try
		{
			mysql.Open();
			Console.WriteLine("[数据库]connect succ ");

			return true;
		}
		catch (Exception e)
		{
			Console.WriteLine("[数据库]connect fail, " + e.Message);
			return false;
		}
	}


	private static void CheckAndReconnect(){
		try{
			if(mysql.Ping()){
				return;
			}
			mysql.Close();
			mysql.Open();
			Console.WriteLine("[数据库] Reconnect!");
		}
		catch(Exception e){
			Console.WriteLine("[数据库] CheckAndReconnect fail " + e.Message);
		}
		
	}


	private static bool IsSafeString(string str)
	{
		return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
	}

	
	public static bool IsAccountExist(string id)
	{
		CheckAndReconnect();

		if (!DbManager.IsSafeString(id)){
			return false;
		}

		string s = string.Format("select * from user where account='{0}';", id);  

		try 
		{
			MySqlCommand cmd = new MySqlCommand (s, mysql); 
			MySqlDataReader dataReader = cmd.ExecuteReader (); 
			bool hasRows = dataReader.HasRows;
			dataReader.Close();
			return !hasRows;
		}
		catch(Exception e)
		{
			Console.WriteLine("[数据库] IsSafeString err, " + e.Message);
			return false;
		}
	}


	public static bool Register(string id, string pw)
	{
		CheckAndReconnect();

		if(!DbManager.IsSafeString(id)){
			Console.WriteLine("[数据库] Register fail, id not safe");
			return false;
		}
		if(!DbManager.IsSafeString(pw)){
			Console.WriteLine("[数据库] Register fail, pw not safe");
			return false;
		}

		if (!IsAccountExist(id)) 
		{
			Console.WriteLine("[数据库] Register fail, id exist");
			return false;
		}

		string sql = string.Format("insert into user set account ='{0}' ,password ='{1}';", id, pw);
		try
		{
			MySqlCommand cmd = new MySqlCommand(sql, mysql);
			cmd.ExecuteNonQuery();
			return true;
		}
		catch(Exception e)
		{
			Console.WriteLine("[数据库] Register fail " + e.Message);
			return false;
		}
	}
	
	public static bool CheckPassword(string id, string pw)
	{
		CheckAndReconnect();

		if(!DbManager.IsSafeString(id)){
			Console.WriteLine("[数据库] CheckPassword fail, id not safe");
			return false;
		}
		if(!DbManager.IsSafeString(pw)){
			Console.WriteLine("[数据库] CheckPassword fail, pw not safe");
			return false;
		}
		
		string sql = string.Format("select * from user where account='{0}' and password='{1}';", id, pw);  

		try 
		{
			MySqlCommand cmd = new MySqlCommand (sql, mysql);  
			MySqlDataReader dataReader = cmd.ExecuteReader();
			bool hasRows = dataReader.HasRows;
			dataReader.Close();
			return hasRows;
		}
		catch(Exception e)
		{
			Console.WriteLine("[数据库] CheckPassword err, " + e.Message);
			return false;
		}
	}
	
}