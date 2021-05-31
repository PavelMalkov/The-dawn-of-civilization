/*using System;

public class Pair<L, R>
{
    private L first;
    private R second;
    public Pair(L l, R r)
    {
        this.first = l;
        this.second = r;
    }
    public L getfirst() { return first; }
    public R getsecond() { return second; }
    public void setfirst(L l) { this.first = l; }
    public void setsecond(R r) { this.second = r; }
}*/
/*
public class Part : IEquatable<Part>
{
    public string PartName { get; set; }

    public int PartId { get; set; }

    public override string ToString()
    {
        return "ID: " + PartId + "   Name: " + PartName;
    }
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        Part objAsPart = obj as Part;
        if (objAsPart == null) return false;
        else return Equals(objAsPart);
    }
    public override int GetHashCode()
    {
        return PartId;
    }
    public bool Equals(Part other)
    {
        if (other == null) return false;
        return (this.PartId.Equals(other.PartId));
    }
    // Should also override == and != operators.
}*/