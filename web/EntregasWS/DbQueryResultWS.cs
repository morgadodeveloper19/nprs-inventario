public class DbQueryResultWS
{
    public bool Success
    {
        get;
        set;
    }
    public string ErrorMessage
    {
        get;
        set;
    }

    private int _id = 1;
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

}