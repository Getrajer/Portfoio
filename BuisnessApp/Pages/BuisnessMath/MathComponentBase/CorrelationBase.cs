using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuisnessApp.Pages.BuisnessMath.MathComponentBase
{
    public class CorrelationBase:ComponentBase
    {
        //This function will generate random number
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        /// <summary>
        /// This class is used to create Correlation Coefficient Formula object
        /// </summary>
        public class CCF_Data_Obj
        {
            public int ID { get; set; }
            public double X { get; set; }
            public double Y { get; set; }
            public double XY { get; set; }
            public double X_Square { get; set; }
            public double Y_Square { get; set; }
            public CCF_Data_Obj()
            {

            }
        }

        /// <summary>
        /// This List will store Correlation Coefficient Formula objects (CCF_Data_Obj)
        /// </summary>
        protected List<CCF_Data_Obj> CCF_Data = new List<CCF_Data_Obj>();

        protected int CCF_List_Lenght = 0;

        //Checking if custom data is used for Correlation Coefficient Formula(CCF)
        protected bool isCustom_CCF = false;
        //Checking if random generated data is used Correlation Coefficient Formula(CCF)
        protected bool isRandom_CCF = false;
        //Checking if Correlation Coefficient Formula(CCF) is beeing calculated
        protected bool isCalculated_CCF = false;

        //Text for example
        protected string CCF_Text_Example = "Table shows data of weekly maintenance cost(y) to the age. It will calculate correlation between, how machines are old and how maintence cost is beeing affected by it.  ";

        //Variables for custom input
        protected double x_CCF = 0;
        protected double y_CCF = 0;

        //Text for button
        protected string btn_CCF_text = "See example";

        //Total x
        protected double total_x_CCF = 0;
        //Total y
        protected double total_y_CCF = 0;
        //Total xy
        protected double total_xy_CCF = 0;
        //Total x^2
        protected double total_x_square_CCF = 0;
        //Total y^2
        protected double total_y_square_CCF = 0;

        /// <summary>
        /// This will show result of Correlation Coefficient Formula calculation
        /// </summary>
        protected string result_CCF;

        /// <summary>
        /// This will add cutom variable to the List CCF_Data
        /// </summary>
        public void Add_C_CCF()
        {
            isCalculated_CCF = false;
            isCustom_CCF = true;

            CCF_Data_Obj obj = new CCF_Data_Obj();
            obj.ID = CCF_Data.Count;
            obj.X = x_CCF;
            obj.Y = y_CCF;
            obj.XY = obj.X * obj.Y;
            obj.X_Square = Math.Pow(obj.X, 2);
            obj.Y_Square = Math.Pow(obj.Y, 2);

            total_x_CCF += obj.X;
            total_y_CCF += obj.Y;
            total_xy_CCF += obj.XY;
            total_x_square_CCF += obj.X_Square;
            total_y_square_CCF += obj.Y_Square;

            CCF_Data.Add(obj);
        }

        /// <summary>
        /// This will delete last variable from List CCF_Data
        /// </summary>
        public void Delete_CCF()
        {
            isCalculated_CCF = false;

            if (CCF_Data.Count != 0)
            {
                CCF_Data_Obj obj = CCF_Data[CCF_Data.Count - 1];
                CCF_Data.RemoveAt(CCF_Data.Count - 1);

                total_x_CCF -= obj.X;
                total_y_CCF -= obj.Y;
                total_xy_CCF -= obj.XY;
                total_x_square_CCF -= obj.X_Square;
                total_y_square_CCF -= obj.Y_Square;

                for (int i = 0; i < CCF_Data.Count; i++)
                {
                    CCF_Data[i].ID = i;
                }

                if (CCF_Data.Count == 0)
                {
                    isCustom_CCF = false;
                    isRandom_CCF = false;
                    btn_CCF_text = "See example";
                }
            }
        }

        /// <summary>
        /// This will reset Correlation coefficient formula program
        /// </summary>
        public void Reset_CCF()
        {
            isCalculated_CCF = false;
            CCF_Data.Clear();
            total_x_CCF = 0;
            total_y_CCF = 0;
            total_xy_CCF = 0;
            total_x_square_CCF = 0;
            total_y_square_CCF = 0;

            btn_CCF_text = "See example";

            x_CCF = 0;
            y_CCF = 0;

            isRandom_CCF = false;
            isCustom_CCF = false;
        }

        /// <summary>
        /// This will delete CCF object from CCF_Data, by its Id
        /// </summary>
        public void Delete_CCF_ID(int id)
        {
            isCalculated_CCF = false;

            if (CCF_Data.Count != 0)
            {
                CCF_Data_Obj obj = CCF_Data[id];

                CCF_Data.RemoveAt(id);

                total_x_CCF -= obj.X;
                total_y_CCF -= obj.Y;
                total_xy_CCF -= obj.XY;
                total_x_square_CCF -= obj.X_Square;
                total_y_square_CCF -= obj.Y_Square;

                for (int i = 0; i < CCF_Data.Count; i++)
                {
                    CCF_Data[i].ID = i;
                }

                if (CCF_Data.Count == 0)
                {
                    isCustom_CCF = false;
                    isRandom_CCF = false;
                    btn_CCF_text = "See example";
                }
            }
        }

        /// <summary>
        /// This will generate mock data to the List CCF_Data, and can be used as well to reset the data
        /// </summary>
        public void Generate_Data_CCF()
        {
            isCalculated_CCF = false;
            isRandom_CCF = true;
            btn_CCF_text = "Reset Data";

            for (int i = 0; i < 10; i++)
            {


                if (CCF_Data.Count == 0)
                {
                    double random = RandomNumber(1 * 10, 1 * 100);

                    CCF_Data_Obj obj = new CCF_Data_Obj();
                    obj.ID = i;
                    obj.X = 1 * 10;
                    obj.Y = random;
                    obj.XY = obj.X * obj.Y;
                    obj.X_Square = Math.Pow(obj.X, 2);
                    obj.Y_Square = Math.Pow(obj.Y, 2);

                    total_x_CCF += obj.X;
                    total_y_CCF += obj.Y;
                    total_xy_CCF += obj.XY;
                    total_x_square_CCF += obj.X_Square;
                    total_y_square_CCF += obj.Y_Square;

                    CCF_Data.Add(obj);
                }
                else
                {
                    double random = RandomNumber(i * 10, i * 100);

                    CCF_Data_Obj obj = new CCF_Data_Obj();
                    obj.ID = i;
                    obj.X = (i + 1) * 10;
                    obj.Y = random;
                    obj.XY = obj.X * obj.Y;
                    obj.X_Square = Math.Pow(obj.X, 2);
                    obj.Y_Square = Math.Pow(obj.Y, 2);

                    total_x_CCF += obj.X;
                    total_y_CCF += obj.Y;
                    total_xy_CCF += obj.XY;
                    total_x_square_CCF += obj.X_Square;
                    total_y_square_CCF += obj.Y_Square;

                    CCF_Data.Add(obj);
                }
            }
        }

        /// <summary>
        /// This will calculate Correlation for Coefficient Formula
        /// </summary>
        public void Calculate_CCF()
        {
            if (CCF_Data.Count != 0)
            {
                int lenght = CCF_Data.Count;
                double result = ((lenght * total_xy_CCF) - (total_x_CCF * total_y_CCF))
                    / (Math.Sqrt((lenght * total_x_square_CCF) - (Math.Pow(total_x_CCF, 2))) * Math.Sqrt((lenght * total_y_square_CCF) - Math.Pow(total_y_CCF, 2)));

                CCF_List_Lenght = lenght;
                isCalculated_CCF = true;
                result_CCF = result.ToString();
            }
        }


        //////////////////////////////////
        ///



        /// <summary>
        /// This class is used to create Rank Correlation object
        /// </summary>
        public class RC_Data_Obj
        {
            public int ID { get; set; }
            public double X { get; set; }
            public double Y { get; set; }
            public double Rx { get; set; }
            public double Ry { get; set; }
            public double D_Square { get; set; }
            public RC_Data_Obj()
            {

            }
        }

        /// <summary>
        /// This List will store Correlation Coefficient Formula objects (RC_Data_Obj)
        /// </summary>
        protected List<RC_Data_Obj> RC_Data = new List<RC_Data_Obj>();

        protected int RC_List_Lenght = 0;

        //Checking if custom data is used for Rank Correlation(RC)
        protected bool isCustom_RC = false;
        //Checking if random generated data is used (RC)
        protected bool isRandom_RC = false;
        //Checking if Rank Correlation (RC) is beeing calculated
        protected bool isCalculated_RC = false;

        //Text for example
        protected string RC_Text_Example = "This table shows an average rates for rent and utilities in London. Rates(x). Rent(y).";

        //Variables for custom input
        protected double x_RC = 0;
        protected double y_RC = 0;
        protected double rx_RC = 0;
        protected double ry_RC = 0;

        //Text for button
        protected string btn_RC_text = "See example";

        //Total x
        protected double total_x_RC = 0;
        //Total y
        protected double total_y_RC = 0;
        //Total Rank X
        protected double total_RC_Rx = 0;
        //Total Rank y
        protected double total_RC_Ry = 0;
        //Total d^2
        protected double total_d_square_RC = 0;

        /// <summary>
        /// This will show result of Rank Correlation
        /// </summary>
        protected string result_RC;

        /// <summary>
        /// This will add cutom variable to the List RC_Data
        /// </summary>
        public void Add_C_RC()
        {
            isCalculated_RC = false;
            isCustom_RC = true;

            RC_Data_Obj obj = new RC_Data_Obj();
            obj.ID = RC_Data.Count;
            obj.X = x_RC;
            obj.Y = y_RC;
            obj.Rx = rx_RC;
            obj.Ry = ry_RC;
            obj.D_Square = Math.Pow(obj.Rx - obj.Ry, 2);

            total_x_RC += obj.X;
            total_y_RC += obj.Y;
            total_RC_Rx += obj.Rx;
            total_RC_Ry += obj.Ry;
            total_d_square_RC += obj.D_Square;

            RC_Data.Add(obj);
        }

        /// <summary>
        /// This will delete last variable from List RC_Data
        /// </summary>
        public void Delete_RC()
        {
            isCalculated_RC = false;

            if (RC_Data.Count != 0)
            {
                RC_Data_Obj obj = RC_Data[RC_Data.Count - 1];
                RC_Data.RemoveAt(RC_Data.Count - 1);

                total_x_RC -= obj.X;
                total_y_RC -= obj.Y;
                total_RC_Rx -= obj.Rx;
                total_RC_Ry -= obj.Ry;
                total_d_square_RC -= obj.D_Square;

                for (int i = 0; i < RC_Data.Count; i++)
                {
                    RC_Data[i].ID = i;
                }

                if (RC_Data.Count == 0)
                {
                    isCustom_RC = false;
                    isRandom_RC = false;
                    btn_RC_text = "See example";
                }
            }
        }

        /// <summary>
        /// This will reset Rank Correlation program
        /// </summary>
        public void Reset_RC()
        {
            isCalculated_RC = false;
            RC_Data.Clear();
            total_x_RC = 0;
            total_y_RC = 0;
            total_RC_Rx = 0;
            total_RC_Ry = 0;
            total_d_square_RC = 0;

            btn_RC_text = "See example";

            x_RC = 0;
            y_RC = 0;

            isRandom_RC = false;
            isCustom_RC = false;
        }

        /// <summary>
        /// This will delete CCF object from RC_Data, by its Id
        /// </summary>
        public void Delete_RC_ID(int id)
        {
            isCalculated_RC = false;

            if (RC_Data.Count != 0)
            {
                RC_Data_Obj obj = RC_Data[id];

                RC_Data.RemoveAt(id);

                total_x_RC -= obj.X;
                total_y_RC -= obj.Y;
                total_RC_Rx -= obj.Rx;
                total_RC_Ry -= obj.Ry;
                total_d_square_RC -= obj.D_Square;

                for (int i = 0; i < RC_Data.Count; i++)
                {
                    RC_Data[i].ID = i;
                }

                if (RC_Data.Count == 0)
                {
                    isCustom_RC = false;
                    isRandom_RC = false;
                    btn_RC_text = "See example";
                }
            }
        }

        /// <summary>
        /// This will generate mock data to the List RC_Data, and can be used as well to reset the data
        /// </summary>
        public void Generate_Data_RC()
        {
            isCalculated_RC = false;
            isRandom_RC = true;
            btn_RC_text = "Reset Data";

            for (int i = 0; i < 10; i++)
            {
                if (RC_Data.Count == 0)
                {
                    double random = RandomNumber(1 * 10, 1 * 100);

                    RC_Data_Obj obj = new RC_Data_Obj();
                    obj.ID = i;
                    obj.X = 1 * 10;
                    obj.Y = random;
                    obj.Rx = RandomNumber(4, 10);
                    obj.Ry = RandomNumber(4, 15);
                    obj.D_Square = Math.Pow(obj.Rx - obj.Ry, 2);

                    total_x_RC += obj.X;
                    total_y_RC += obj.Y;
                    total_RC_Rx += obj.Rx;
                    total_RC_Ry += obj.Ry;
                    total_d_square_RC += obj.D_Square;

                    RC_Data.Add(obj);
                }
                else
                {
                    double random = RandomNumber(i * 10, i * 100);

                    RC_Data_Obj obj = new RC_Data_Obj();
                    obj.ID = i;
                    obj.X = (i + 1) * 10;
                    obj.Y = random;
                    obj.Rx = RandomNumber(4, 10);
                    obj.Ry = RandomNumber(4, 15);
                    obj.D_Square = Math.Pow(obj.Rx - obj.Ry, 2);

                    total_x_RC += obj.X;
                    total_y_RC += obj.Y;
                    total_RC_Rx += obj.Rx;
                    total_RC_Ry += obj.Ry;
                    total_d_square_RC += obj.D_Square;

                    RC_Data.Add(obj);
                }
            }
        }

        /// <summary>
        /// This will calculate Rank Correlation
        /// </summary>
        public void Calculate_RC()
        {
            if (RC_Data.Count != 0)
            {
                int lenght = RC_Data.Count;

                double result = 1 - ((6 * total_d_square_RC) / (lenght * (Math.Pow(lenght, 2) - 1)));

                RC_List_Lenght = lenght;
                isCalculated_RC = true;
                result_RC = result.ToString();
            }
        }
    }
}
