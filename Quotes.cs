internal class Quotes
{
    private string _id;

    public string Description { get; set; }

    public double Value { get; set; }

    public Quotes(string d, double v)
    {
        var temp = Guid.NewGuid();
        _id = temp.ToString().Substring(0, 8);

        Description = d;
        Value = v;
    }
    public Quotes (string id, string d, double v)
    {
        this._id = id;
        this.Description = d;
        this.Value = v;
    }

    public override string ToString()
    {
        return @"ID: " + this._id + "\nDescrição: " + this.Description + "\nValor: R$" + this.Value;
    }
    public string SaveToFile()
    {
        return this._id + "," + this.Description + "," + this.Value;
    }

    public string GetID(){
        return this._id;
    }
}