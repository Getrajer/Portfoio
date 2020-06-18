using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuisnessApp.Pages.BuisnessMath.MathComponentBase
{
    public class InterestBase:ComponentBase
    {
        ////
        ///Simple Interest Accured Ammount Formula
        ////

        /// <summary>
        /// This class is used to create ACCURED AMMOUNT FORMULA OBJECT for Simple Interest
        /// </summary>
        public class AAF_SI_Object
        {
            public int Id { get; set; }
            public double Value { get; set; }
            public int Time { get; set; }

            public AAF_SI_Object()
            {
            }
        }

        //Declaring list for AAF_SI_Objects
        protected List<AAF_SI_Object> AAF_SI_Data = new List<AAF_SI_Object>();

        //Declaring variable for interest (%)
        protected double AAF_SI_Interest;

        //Declaring variable for First Value input
        protected double AAF_SI_Value;

        //Declaring variable for preroid of time to count interest
        protected double AAF_SI_Time;

        //Declaring string for name of peroid of time
        protected string AAF_SI_T_Name;

        //Variables for the result
        protected double AAF_SI_Diffrence = 0;

        //Bool which checks if Calc Interest is Calculated
        protected bool AAF_SI_IsCalculated = false;



        /// <summary>
        /// This function will hide result text when data is beeing changed
        /// </summary>
        public void AAF_SI_Focus()
        {
            AAF_SI_IsCalculated = false;
        }

        /// <summary>
        /// This function will calculate interest for Simple Interest with use of Accured Ammount Formula
        /// </summary>
        public void CalcInterest_AAF_SI()
        {
            AAF_SI_Data.Clear();
            AAF_SI_Diffrence = 0;
            if (AAF_SI_Time != 0)
                if (AAF_SI_Interest != 0)
                    if (AAF_SI_Value != 0)
                    {
                        AAF_SI_IsCalculated = true;
                        for (int i = 1; i <= AAF_SI_Time; i++)
                        {
                            if (AAF_SI_Data.Count == 0)
                            {
                                AAF_SI_Object obj = new AAF_SI_Object();
                                obj.Value = Math.Round(AAF_SI_Value * (1 + (AAF_SI_Interest / 100)), 3);
                                obj.Time = i;
                                obj.Id = i;
                                AAF_SI_Data.Add(obj);
                            }
                            else
                            {
                                AAF_SI_Object temp = AAF_SI_Data[i - 2];
                                AAF_SI_Object obj = new AAF_SI_Object();
                                obj.Value = Math.Round(temp.Value + (AAF_SI_Value * (1 + (AAF_SI_Interest / 100)) - AAF_SI_Value), 3);
                                obj.Time = i;
                                obj.Id = i;
                                AAF_SI_Data.Add(obj);
                            }
                        }
                        AAF_SI_Diffrence += Math.Round(AAF_SI_Data[AAF_SI_Data.Count - 1].Value - AAF_SI_Value, 3);

                    }
        }


        ////
        ///Compound Interest Accured Ammount Formula
        ////


        /// <summary>
        /// This class is used to create ACCURED AMMOUNT FORMULA OBJECT for Compound Interest
        /// </summary>
        public class AAF_CI_Object
        {
            public int Id { get; set; }
            public double Value { get; set; }
            public double Interest_Value { get; set; }
            public int Time { get; set; }

            public AAF_CI_Object()
            {
            }
        }

        //Declaring list for AAF_CI_Objects
        protected List<AAF_CI_Object> AAF_CI_Data = new List<AAF_CI_Object>();

        //Declaring variable for interest (%)
        protected double AAF_CI_Interest;

        //Declaring variable for First Value input
        protected double AAF_CI_Value;

        //Declaring variable for preroid of time to count interest
        protected double AAF_CI_Time;

        //Declaring string for name of peroid of time
        protected string AAF_CI_T_Name;

        //Variables for the result
        protected double AAF_CI_Diffrence = 0;

        //Bool which checks if Calc Interest is Calculated
        protected bool AAF_CI_IsCalculated = false;

        /// <summary>
        /// This function will hide result text when data is beeing changed
        /// </summary>
        public void AAF_CI_Focus()
        {
            AAF_CI_IsCalculated = false;
        }

        /// <summary>
        /// This function will calculate interest for Compound Interest with use of Accured Ammount Formula
        /// </summary>
        public void CalcInterest_AAF_CI()
        {
            AAF_CI_Data.Clear();
            AAF_CI_Diffrence = 0;
            if (AAF_CI_Time != 0)
                if (AAF_CI_Interest != 0)
                    if (AAF_CI_Value != 0)
                    {
                        AAF_CI_IsCalculated = true;
                        //Calculate interest
                        for (int i = 1; i <= AAF_CI_Time; i++)
                        {
                            AAF_CI_Object obj = new AAF_CI_Object();
                            double interest = AAF_CI_Interest;

                            obj.Id = i;
                            obj.Time = i;
                            obj.Value = Math.Round(AAF_CI_Value * Math.Pow((interest / 100) + 1, i), 3);
                            obj.Interest_Value = Math.Pow((interest / 100), i);

                            AAF_CI_Data.Add(obj);
                        }

                        //Calc Diffrence between actual and the old number
                        AAF_CI_Diffrence += Math.Round(AAF_CI_Data[AAF_CI_Data.Count - 1].Value - AAF_CI_Value, 3);
                    }
        }
    }
}
