using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Dapper;

namespace PayrollV3
{
    public class LeavesRepo
    {
        private static LeavesRepo instance;

        private LeavesRepo() { }

        public static LeavesRepo Instance() 
        {
            if (instance == null)
                instance = new LeavesRepo();
            return instance;
        }

        public Leaves FindByID(int leavesId) 
        {
            Leaves leaves;

            try 
            {
            using(SqlConnection connection= DBConnection.getConnection()) 
                {
                    string query = "select * from Leaves where leaves_id= @id";
                    var objparam = new { id = leavesId };
                    leaves=connection.QueryFirstOrDefault<Leaves>(query,objparam);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return leaves;
        }
        public void UpdateByID(Leaves leaves) //works only in payrollForm 
        {
            try 
            {
                using(SqlConnection connection = DBConnection.getConnection())
                {
                    string query = "update leaves set remaining_SL = @SL, remaining_VL= @VL, emergency_leave = @EL, bereavement_leave_used= @BL where leaves_id = @id ";
                    var objparam = new
                    {
                        SL = leaves.Remaining_SL,
                        VL = leaves.Remaining_VL,
                        EL = leaves.Emergency_leave,
                        BL = leaves.Bereavement_leave_used,
                        id = leaves.Leaves_id,
                    };
                    connection.Execute(query,objparam);
                    MessageBox.Show("leave record updated");
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
    public class Leaves
    {
        public int Leaves_id { get; set; }
        public decimal Accrued_sickleave { get; set; }
        public decimal Accrued_vacationleave { get; set; }
        public decimal Remaining_SL { get; set; }
        public decimal Remaining_VL { get; set; }
        public DateTime DateStarted { get; set; }
        public int Emergency_leave { get; set; }
        public int Bereavement_leave_used { get; set; }

        public override string ToString()
        {
            return $"Leaves_id: {Leaves_id}, " +
                   $"Accrued_sickleave: {Accrued_sickleave}, " +
                   $"Accrued_vacationleave: {Accrued_vacationleave}, " +
                   $"Remaining_SL: {Remaining_SL}, " +
                   $"Remaining_VL: {Remaining_VL}, " +
                   $"DateStarted: {DateStarted}, " +
                   $"Emergency_leave: {Emergency_leave}";
        }

    }
}
