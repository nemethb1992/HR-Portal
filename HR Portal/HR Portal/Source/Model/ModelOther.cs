
namespace HR_Portal.Source.Model
{

    public class ModelNyelv
    {
        public int id { get; set; }
        public string nyelv { get; set; }
    }

    public class ModelErtesulesek
    {
        public int id { get; set; }
        public string ertesules_megnevezes { get; set; }
    }

    public class ModelNem
    {
        public int id { get; set; }
        public string nem { get; set; }
    }

    public class ModelMunkakor
    {
        public int id { get; set; }
        public string munkakor { get; set; }
    }

    public class ModelStatusz
    {
        public int id { get; set; }
        public string allapot { get; set; }
    }

    public class ModelPc
    {
        public int id { get; set; }
        public string megnevezes_pc { get; set; }
    }

    public class ModelVegzettseg
    {
        public int id { get; set; }
        public string megnevezes_vegzettseg { get; set; }
    }

    public class ModelErtesitendok
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    }

    public class ModelHr
    {
        public int id { get; set; }
        public string name { get; set; }
        public int kategoria { get; set; }
        public int jogosultsag { get; set; }
        public int validitas { get; set; }
    }

    public class ModelComment
    {
        public int id { get; set; }
        public int jelolt_id { get; set; }
        public int projekt_id { get; set; }
        public int hr_id { get; set; }
        public string hr_nev { get; set; }
        public string megjegyzes { get; set; }
        public string datum { get; set; }
        public int ertekeles { get; set; }
    }

    public class ModelJeloltFile
    {
        public string fajlnev { get; set; }
        public string path { get; set; }
    }

    public class ModelKompetenciak
    {
        public int id { get; set; }
        public string kompetencia_megnevezes { get; set; }
    }

    public class ModelKoltsegek
    {
        public int id { get; set; }
        public int osszeg { get; set; }
        public string koltseg_megnevezes { get; set; }
    }
    public class ModelId
    {
        public int id { get; set; }
    }
}
