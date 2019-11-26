using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Databank
{
    public class Cursus
    {
        private int cursusnr;
        private string cursusNaam;
        private int inschrijvingsGeld;

        public int Cursusnr
        {
            get { return cursusnr; }
        }

        public string CursusNaam
        {
            get { return cursusNaam; }
            set { cursusNaam = value; }
        }

        public int InschrijvingsGeld
        {
            get { return inschrijvingsGeld; }
            set { inschrijvingsGeld = value; }
        }
        private Cursus()
        {

        }
        public Cursus(string cursusnaam, int inschrijvingsgeld)
        {
            this.cursusNaam = cursusnaam;
            this.inschrijvingsGeld = inschrijvingsgeld;
        }
        public int InsertCursus()
        {
            SqlParameter[] sp = new SqlParameter[2];
            sp[0] = new SqlParameter();
            sp[0].ParameterName = "@cursusnaam";
            sp[0].Value = cursusNaam;
            sp[1] = new SqlParameter();
            sp[1].ParameterName = "@inschrijvingsgeld";
            sp[1].Value = inschrijvingsGeld;
            cursusnr = DBGeneral.ExecuteSP("CursussenInsert", sp);
            return cursusnr;

        }
        public int UpdateCursus()
        {
            SqlParameter[] sp = new SqlParameter[3];
            sp[0] = new SqlParameter();
            sp[0].ParameterName = "@cursusnr";
            sp[0].Value = cursusnr;
            sp[1] = new SqlParameter();
            sp[1].ParameterName = "@cursusnaam";
            sp[1].Value = cursusNaam;
            sp[2] = new SqlParameter();
            sp[2].ParameterName = "@inschrijvingsgeld";
            sp[2].Value = inschrijvingsGeld;
            return DBGeneral.ExecuteSP("CursussenUpdate", sp);

        }
        public int DeleteCursus()
        {
            SqlParameter[] sp = new SqlParameter[1];
            sp[0] = new SqlParameter();
            sp[0].ParameterName = "@cursusnr";
            sp[0].Value = cursusnr;
            return DBGeneral.ExecuteSP("CursussenDelete", sp);

        }
        public static DataTable GetCursussen(string sorteerveld, Enumeraties.sorteervolgorde sortorder)
        {
            SqlParameter[] sp = new SqlParameter[2];
            sp[0] = new SqlParameter();
            sp[0].ParameterName = "@sorteerVeld";
            sp[0].Value = sorteerveld;
            sp[1] = new SqlParameter();
            sp[1].ParameterName = "@sorteerVolgorde";
            if (sortorder == Enumeraties.sorteervolgorde.ASC)
                sp[1].Value = "ASC";
            else
                sp[1].Value = "DESC";
            return DBGeneral.ExecuteSPWithDataTable("CursussenSelectAll", sp);          

        }
        public static Cursus GetCursusByID(int cursusnr)
        {
            SqlParameter[] sp = new SqlParameter[1];
            sp[0] = new SqlParameter();
            sp[0].ParameterName = "@cursusnr";
            sp[0].Value = cursusnr;
            DataTable dt = DBGeneral.ExecuteSPWithDataTable("CursussenSelectByCursusnr", sp);
            if (dt.Rows.Count > 0)
            {
                Cursus nieuwecursus = new Cursus();
                nieuwecursus.cursusNaam = dt.Rows[0]["cursusnaam"].ToString();
                nieuwecursus.inschrijvingsGeld = int.Parse(dt.Rows[0]["inschrijvingsgeld"].ToString());
                nieuwecursus.cursusnr = cursusnr;
                return nieuwecursus;
            }
            else
                return null;

        }

    }
}
