using System.Data.SqlClient;

public class Connection
{
    readonly SqlConnection _con;

	public Connection()
	{
		_con = new SqlConnection();
	}

	public void OpenConnection()
	{
		_con.ConnectionString = "Data Source=horus;Initial Catalog=myDBName;Persist Security Info=True;User ID=myDBUser;Password=myDBPassword";
		_con.Open();
        return _con;
	}


	public void CloseConnection()
	{
		_con.Close();
        _con.Dispose();
	}

}